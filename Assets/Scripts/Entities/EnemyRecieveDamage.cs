using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRecieveDamage : MonoBehaviour
{
    public float health;
    public float maxHealth;
    protected Animator animator;
    protected AudioSource audioSource;
    public AudioClip audioClip;
    

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        health = maxHealth;
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClip;
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
        if(health > maxHealth)
        {
            health = maxHealth;
        }

    }

    protected void CheckDeath()
    {
        if (health <= 0){
            Debug.Log("Dead");
            animator.SetBool("Dead", true);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    protected void DeadClear()
    {
        //gameObject.GetComponent<Collider2DBox>.enabled = false;
        //Destroy(gameObject);
    }

    protected void HurtDone()
    {
        animator.SetBool("Hurt", false);
        animator.SetBool("Invincible", false);
        CheckDeath() ;
        
    }

    protected void AttackDone()
    {
        animator.SetBool("Attacking", false);
    }

    protected void OnHitBecomeInvicible()
    {
        animator.SetBool("Invincible", true);
    }


}
