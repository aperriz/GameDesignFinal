using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    AgentMover agentMover;
    private Vector2 movementInput;
    private Button attackInput, escapeInput;

    bool moveKeyHeld;

    Animator animator;
    new SpriteRenderer renderer;

    [SerializeField]
    GameObject weaponObject;

    [SerializeField]
    private InputActionReference movement, attack, escape;

    [SerializeField]
    float speed = 0.1f;

    private void Awake()
    {
        agentMover = GetComponent<AgentMover>();
        animator = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (attack.action.IsPressed())
        {
            OnAttack();
        }

        if (escape.action.IsPressed())
        {
            OnEscape();
        }

        Move();
    }

    private void OnEscape()
    {
        Debug.Log("Exit");
        Application.Quit();
    }

    private void Move()
    {
        movementInput = movement.action.ReadValue<Vector2>();
        animator.SetFloat("xDir", movementInput.x);
        animator.SetFloat("yDir", movementInput.y);
        if(movementInput.x > 0)
        {
            renderer.flipX = false;
            animator.SetBool("Flipped", false);
        }
        else if (movementInput.x < 0)
        {
            renderer.flipX = true;
            animator.SetBool("Flipped", true);
        }
        agentMover.MovementInput = movementInput;
    }

    public void OnAttack()
    {
        if (!animator.GetBool("Attacking"))
        {
            animator.SetBool("Attacking", true);
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 myPos = transform.position;
            Vector2 direction = (mousePos - myPos).normalized;
            GameObject attackObject = Instantiate(weaponObject, transform.position, Quaternion.identity, GameObject.Find("Player").transform);
            attackObject.SetActive(true);
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Debug.Log(angle);

            if (angle > 90 || angle < -90)
            {
                animator.SetBool("Flipped", true);
                GetComponent<SpriteRenderer>().flipX = true;
                attackObject.GetComponent<SpriteRenderer>().flipY = true;
                //attackObject.GetComponent<SpriteRenderer>().flipX = true;
                attackObject.transform.rotation = Quaternion.Euler(0, 0, angle);
            }
            else
            {
                GetComponent<SpriteRenderer>().flipX = false;
                attackObject.transform.rotation = Quaternion.Euler(0, 0, angle);
            }


        }
    }
}
