using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class EnemyRecieveDamage : MonoBehaviour
{
    public float health;
    public float maxHealth;
    protected Animator animator;
    protected AudioSource audioSource;
    public AudioClip audioClip;
    public float speed;
    protected float tempSpeed;
    GameObject player;
    [SerializeField]
    public   int weight = 1;
    [SerializeField]
    int xForce = 2, yForce = 2;
    public GameObject healthBar;
    public Slider healthBarSlider;
    [SerializeField]
    public int aggressionRange = 20;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        health = maxHealth;
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClip;
        tempSpeed = speed;
        player = GameObject.Find("Player");
    }

    private void Awake()
    {
        health = maxHealth;

    }

    public void DealDamage(int damage)
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClip;
        healthBarSlider.enabled = true;
        healthBar.SetActive(true);

        healthBarSlider.value = CalculateHealthPercent();

        if (!animator.GetBool("Invincible"))
        {
            audioSource.Play();
            health -= damage;
            animator.SetBool("Hurt", true);
            animator.SetBool("Invincible", true);
            CheckDeath();
        }
    }

    protected void CheckOverheal()
    {
        if (health > maxHealth)
        {
            health = maxHealth;
        }

    }

    protected void CheckDeath()
    {
        healthBarSlider.value = CalculateHealthPercent();
        if (health <= 0)
        {
            Debug.Log("Dead");
            animator.SetBool("Dead", true);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            Destroy(transform.parent.GetComponent<AIDestinationSetter>());
            Destroy(transform.parent.GetComponent<AIPath>());

        }
    }

    protected void DeadClear()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        //Destroy(gameObject);
        foreach(var comp in GetComponents<Component>())
        {
            if(comp != GetComponent<Animator>() && comp != GetComponent<Transform>() && comp != GetComponent<SpriteRenderer>())
            {
                Destroy(comp);
            }
        }
        Destroy(healthBar);
        Destroy(transform.parent.gameObject);
        Destroy(gameObject);
    }

    protected void HurtDone()
    {
        animator.SetBool("Hurt", false);
        animator.SetBool("Invincible", false);
        CheckDeath();

    }

    protected void AttackDone()
    {
        animator.SetBool("Attacking", false);
        StartCoroutine(FreezeAttack());
    }

    protected void OnHitBecomeInvicible()
    {
        animator.SetBool("Invincible", true);
    }

    protected void ImmobileOnAttack()
    {
        speed = 0;
    }

    protected IEnumerator FreezeAttack()
    {
        yield return new WaitForSeconds(1);
        speed = tempSpeed;
    }

    private float CalculateHealthPercent()
    {
        return health / maxHealth;
    }

    private IEnumerator DestroyEnemy()
    {
        yield return new WaitForSeconds(5);
        
    }
}
