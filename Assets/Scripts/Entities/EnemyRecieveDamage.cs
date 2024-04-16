using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

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
    int xForce = 2, yForce = 2;

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        health = maxHealth;
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClip;
        tempSpeed = speed;
        player = GameObject.Find("Player");
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
            Debug.Log("Dead");
            animator.SetBool("Dead", true);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    protected void DeadClear()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        //Destroy(gameObject);
        foreach(var comp in GetComponents<Component>())
        {
            if(comp != GetComponent<Animator>())
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
}
