using System;
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
    [SerializeField]
    private PlayerExtraStats playerExtraStats;

    public int extraDamage = 0;
    [HideInInspector]
    public int baseDamage = 5;
    [SerializeField]
    AudioClip swordSound, bowSound, staffSound;
    public bool moved = false, paused = false;

    Animator animator;
    SpriteRenderer renderer;

    [SerializeField]
    GameObject pauseMenu, uiObject;

    [SerializeField]
    private InputActionReference movement, attack, escape;

    [SerializeField]
    private GameObject[] possibleWeapons;

    private void Awake()
    {
        agentMover = GetComponent<AgentMover>();
        animator = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
        DontDestroyOnLoad(gameObject);
    }

    private void OnLevelWasLoaded(int level)
    {
        moved = false;
    }

    private void Start()
    {
        moved = false;
    }

    private void Update()
    {
        if (!paused)
        {
            if (attack.action.IsPressed())
            {
                OnAttack();
            }

            Move();
        }

        if (escape.action.triggered)
        {
            OnEscape();
        }
    }

    private void OnEscape()
    {
        TogglePauseMenu();
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
            moved = true;
        }
        else if (movementInput.x < 0)
        {
            renderer.flipX = true;
            animator.SetBool("Flipped", true);
            moved = true;
        }
        agentMover.MovementInput = movementInput;
    }

    public void OnAttack()
    {
        if (!moved)
        {
            moved = true;
        }
        GameObject weaponObject;
        switch (playerExtraStats.weaponType)
        {
            case "Sword":
                weaponObject = possibleWeapons[0];
                baseDamage = 5;
                break;
            case "Bow":
                weaponObject = possibleWeapons[1];
                baseDamage = 3;
                break;
            case "Staff":
                weaponObject = possibleWeapons[2];
                baseDamage = 8;
                break;
            default:
                Debug.LogWarning("Invalid weapon type");
                return;
        }

        ProjectileWeapon pTest;
        MeleeWeapon wTest;

        if(weaponObject != null)
        {
            if (weaponObject.TryGetComponent<ProjectileWeapon>(out pTest))
            {
                weaponObject.GetComponent<ProjectileWeapon>().projectileDamage = baseDamage + extraDamage;
            }
            else if (weaponObject.TryGetComponent<MeleeWeapon>(out wTest))
            {
                weaponObject.GetComponent<MeleeWeapon>().damage = baseDamage + extraDamage;
                //Debug.Log(weaponObject.GetComponent<MeleeWeapon>().damage);
            }
        }

        if (!animator.GetBool("Attacking"))
        {
            animator.SetBool("Attacking", true);
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 myPos = transform.position;
            Vector2 direction = (mousePos - myPos).normalized;
            GameObject attackObject = Instantiate(weaponObject, GameObject.Find("Player").transform);
            attackObject.transform.position = myPos;
            attackObject.SetActive(true);
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            attackObject.SetActive(true);
            //Debug.Log(angle);

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

            AudioSource audioSource = GetComponent<AudioSource>();
            switch (playerExtraStats.weaponType)
            {
                case "Sword":
                    audioSource.clip = swordSound;
                    break;
                case "Bow":
                    audioSource.clip = bowSound;
                    break;
                case "Staff":
                    audioSource.clip = staffSound;
                    break;
            }
            audioSource.Play();
        }
    }


    public void TogglePauseMenu()
    {
        if (!paused)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
            uiObject.SetActive(false);
            paused = true;
            
        }
        else
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
            uiObject.SetActive(true);
            paused = false;
        }
    }

    public void MainMenu()
    {
        Debug.Log("Main menu");
        Application.Quit();
    }
}
    