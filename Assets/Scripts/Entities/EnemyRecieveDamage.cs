using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRecieveDamage : MonoBehaviour
{
    public float health;
    public float maxHealth;
    private Animator animator;
    private AudioSource audioSource;
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

    private void CheckOverheal()
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

    private void DeadClear()
    {
        Destroy(gameObject);
    }

    private void HurtDone()
    {
        animator.SetBool("Hurt", false);
        animator.SetBool("Invincible", false);
        CheckDeath() ;
        
    }

    private void AttackDone()
    {
        animator.SetBool("Attacking", false);
    }

    private void OnHitBecomeInvicible() {
        animator.SetBool("Invincible", true);
    }

    private void MetalOnFleshHit()
    {
        audioSource.Play();
    }
}
