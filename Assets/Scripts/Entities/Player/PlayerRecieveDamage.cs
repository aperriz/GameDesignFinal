using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRecieveDamage : MonoBehaviour
{
    public float health;
    public float maxHealth;
    private Animator animator;
    private AudioSource audioSource;
    public AudioClip audioClip;
    public AudioClip[] hurtSounds;
    public GameObject healthBar;
    public Slider healthBarSlider;
    public Text healthText;

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

            int audioClipChoice = Random.Range(0, hurtSounds.Length - 1);
            gameObject.GetComponent<AudioSource>().clip = hurtSounds[audioClipChoice];
            gameObject.GetComponent<AudioSource>().Play();

            CheckDeath();
            healthBarSlider.value = health / maxHealth;
            healthText.text = health.ToString() + "/" + maxHealth.ToString();
        }
    }
    public void HealPlayer(int heal)
    {
        health += heal;
        CheckOverheal();
        healthBarSlider.value = health / maxHealth;
        healthText.text = health.ToString() + "/" + maxHealth.ToString();
    }

    private void CheckOverheal()
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

    private void HurtDone()
    {
        animator.SetBool("Hurt", false);
        animator.SetBool("Invincible", false);
        CheckDeath();

    }

    private void OnHitBecomeInvicible()
    {
        animator.SetBool("Invincible", true);
    }
}

