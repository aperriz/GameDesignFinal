using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRecieveDamage : MonoBehaviour
{
    public float health;
    public float maxHealth;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private AudioSource audioSource;
    public AudioClip audioClip;
    public AudioClip[] hurtSounds;
    public AudioClip defenseSound;
    [SerializeField]
    public GameObject healthBar;
    public Slider healthBarSlider;
    public Text healthText;
    [SerializeField]
    private PlayerExtraStats playerExtraStats;

    // Start is called before the first frame update
    void Awake()
    {
        health = maxHealth;
        audioSource.clip = audioClip;
    }

    public void DealDamage(int damage)
    {
        
        if (!animator.GetBool("Invincible"))
        {
            audioSource.Play();
            
            if(playerExtraStats.defense > 0)
            {
                playerExtraStats.UpdateDefense(-1);

                animator.SetBool("Invincible", true);

                audioSource.clip = defenseSound;
                audioSource.Play();

                StartCoroutine(DefenseCooldown());
            }
            else
            {
                animator.SetBool("Hurt", true);
                animator.SetBool("Invincible", true);

                int audioClipChoice = Random.Range(0, hurtSounds.Length - 1);
                audioSource.clip = hurtSounds[audioClipChoice];
                audioSource.Play();

                CheckDeath();
                healthBarSlider.value = health / maxHealth;
                healthText.text = health.ToString() + "/" + maxHealth.ToString();

                health -= damage;
            }

        }
    }

    IEnumerator DefenseCooldown()
    {
        yield return new WaitForSeconds(1);

        animator.SetBool("Invincible", false);
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
        healthBarSlider.value = health / maxHealth;
        healthText.text = health.ToString() + "/" + maxHealth.ToString();

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

