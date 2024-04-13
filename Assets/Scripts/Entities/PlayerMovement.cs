using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour, Controls.IPlayerActions
{
    public float speed;
    private Vector2 direction;
    private Animator animator;
    private SpriteRenderer renderer;
    public GameObject weaponObject;
    private Controls controls;
    [SerializeField] private bool moveKeyHeld;

    private void Awake()
    {
        controls = new Controls();
    }
    private void Start()
    {
        animator = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        TakeInput();
        if(moveKeyHeld)
        {
            //SetDirection();
            Move();
        }
    }

    private void SetDirection()
    {
        animator.SetFloat("xDir", controls.Player.Movement.ReadValue<Vector2>().x);
        animator.SetFloat("yDir", controls.Player.Movement.ReadValue<Vector2>().y);

        if (controls.Player.Movement.ReadValue<Vector2>().x > 0)
        {
            animator.SetBool("Flipped", false);
        }
        else
        {
            animator.SetBool("Flipped", true);
        }
    }

    private void OnEnable()
    {
        controls.Player.SetCallbacks(this);
        controls.Player.Enable();
    }

    private void OnDisable()
    {
        controls.Player.SetCallbacks(this);
        controls.Player.Disable();
    }

    void Controls.IPlayerActions.OnMovement(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            moveKeyHeld = true;
        }
        else if (ctx.canceled)
        {
            moveKeyHeld= false;
            animator.SetFloat("xDir", 0);
            animator.SetFloat("yDir", 0);
        }
    }

    void Controls.IPlayerActions.OnExit(InputAction.CallbackContext ctx)
    {
        Debug.Log("Exit");
        //Application.Quit();
    }

    private void Move()
    {
        Vector2 dir = controls.Player.Movement.ReadValue<Vector2>();
        Vector3 futurePos = transform.position + (Vector3)dir*speed;

        /*if (IsValidPosition(futurePos))
        {
            transform.position += (Vector3)dir*speed;
        }
        else
        {
            Debug.Log("Invalid pos");
        }*/

        SetDirection();
    }

    private async void TakeInput()
    {

        if (Input.GetKey(KeyCode.Space))
        {
            if (!animator.GetBool("Attacking"))
            {
                animator.SetBool("Attacking", true);
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 myPos = transform.position;
                Vector2 direction = (mousePos - myPos).normalized;
                GameObject attackObject = Instantiate(weaponObject, transform.position, Quaternion.identity);
                attackObject.SetActive(true);
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Debug.Log(angle);

                if (angle > 90 || angle < -90)
                {
                    animator.SetBool("Flipped", true);
                    renderer.flipX = true;
                    attackObject.GetComponent<SpriteRenderer>().flipY = true;
                    //attackObject.GetComponent<SpriteRenderer>().flipX = true;
                    attackObject.transform.rotation = Quaternion.Euler(0, 0, angle);
                }
                else
                {
                    renderer.flipX = false;
                    attackObject.transform.rotation = Quaternion.Euler(0, 0, angle);
                }


            }
        }

    }

    private void SetAnimatorMovement(Vector2 direction)
    {
        animator.SetFloat("yDir", direction.y);
        animator.SetFloat("xDir", direction.x);
    }

    private void ResetAttack()
    {
        animator.SetBool("Attacking", false);

    }

}
