using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    public int weight = 1;
    public float health;
    public float maxHealth;
    [SerializeField]
    protected Animator animator;
    protected AudioSource audioSource;
    public AudioClip audioClip;
    public float speed;
    protected float tempSpeed;
    protected GameObject player;
    [SerializeField]
    int xForce = 2, yForce = 2;
    [SerializeField]
    protected int aggressionRange = 20;
    protected bool aggroed = false;
    public int damage;
    protected float range;
    protected CircleCollider2D attackCol;
    protected bool attacking = false;
    public int cooldown;



    void Awake()
    {
        animator = GetComponent<Animator>();
        health = maxHealth;
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClip;
        tempSpeed = speed;
        player = GameObject.Find("Player");
        attackCol = GetComponent<CircleCollider2D>();
        attackCol.radius = range;
    }

    private void Update()
    {
        Vector3 myPos = transform.position;
        Vector3 playerPos = GameObject.Find("Player").transform.position;

        if (Vector2.Distance(myPos, playerPos) <= range && !animator.GetBool("Attacking"))
        {
            Attack();
        }

        if (gameObject.GetComponent<Animator>().GetBool("Dead"))
        {
            Destroy(this);
        }
    }

    public void DealDamage(int damage)
    {
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
        if (health <= 0)
        {
            //Debug.Log("Dead");
            animator.SetBool("Dead", true);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    protected void DeadClear()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        //Destroy(gameObject);
        foreach (var comp in GetComponents<Component>())
        {
            if (comp != GetComponent<Animator>())
            {
                Destroy(comp);
            }
        }
    }

    protected void HurtDone()
    {
        animator.SetBool("Hurt", false);
        animator.SetBool("Invincible", false);
        CheckDeath();

    }

    protected void EnemyAttackDone()
    {
        GetComponent<Animator>().SetBool("Attacking", false);
        /*StartCoroutine(AttackCooldown());*/
    }

    protected void OnHitBecomeInvicible()
    {
        animator.SetBool("Invincible", true);
    }

    protected void ImmobileOnAttack()
    {
        speed = 0;
    }

    protected void Attack()
    {
        if (!GetComponent<Animator>().GetBool("Attacking"))
        {
            Debug.Log("Attacking");
            animator.SetBool("Attacking", true);
            GameObject.Find("Player").GetComponent<PlayerRecieveDamage>().DealDamage(damage);
            //StartCoroutine(AttackCooldown());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerRecieveDamage>(out PlayerRecieveDamage player))
        {
            Attack();
        }
    }

    protected IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(cooldown + animator.GetCurrentAnimatorClipInfo(0).Length);
        if (Vector2.Distance(transform.position, GameObject.Find("Player").transform.position) <= range || Vector2.Distance(transform.position, GameObject.Find("Player").transform.position) == 0)
        {
            Attack();
        }
        else
        {
            GetComponent<Animator>().SetBool("Attacking", false);
            Debug.Log(Vector2.Distance(transform.position, GameObject.Find("Player").transform.position));
        }
    }
}
