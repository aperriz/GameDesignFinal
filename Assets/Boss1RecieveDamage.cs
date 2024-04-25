using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Boss1RecieveDamage : MonoBehaviour
{
    public float health;
    public float maxHealth;
    [SerializeField]
    protected Animator animator;
    protected AudioSource audioSource;
    public AudioClip audioClip;
    GameObject player;
    public GameObject healthBar;
    public Slider healthBarSlider;
    [SerializeField]
    Phase1 phaseSwitch;
    bool halfHealth = false;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClip;
        player = GameObject.Find("Player");
    }

    private void Awake()
    {
        health = maxHealth;

    }

    public void DealDamage(int damage)
    {

        if (!animator.GetBool("Invincible") && !animator.GetBool("Hurt"))
        {
            /*if(audioSource == null)
            {
                audioSource = GetComponent<AudioSource>();
            }
            audioSource.clip = audioClip;*/
            healthBarSlider.enabled = true;
            healthBar.SetActive(true);
            //Debug.Log("Ow");

            healthBarSlider.value = CalculateHealthPercent();

            //Debug.Log("Dealing damage");
            /*audioSource.Play();*/
            health -= damage;
            animator.SetBool("Hurt", true);
            animator.SetBool("Invincible", true);
            CheckDeath();
        }
    }

    public void DealSpellDamage(int damage)
    {

        if (!animator.GetBool("Invincible"))
        {

            /*if (audioSource == null)
            {
                audioSource = GetComponent<AudioSource>();
            }
            audioSource.clip = audioClip;*/
            healthBarSlider.enabled = true;
            healthBar.SetActive(true);
            Debug.Log("Ow");

            healthBarSlider.value = CalculateHealthPercent();

            Debug.Log("Dealing damage");
            /* audioSource.Play();*/
            health -= damage;
            animator.SetBool("Hurt", true);

        }
    }

    protected void CheckDeath()
    {
        healthBarSlider.value = CalculateHealthPercent();
        if(CalculateHealthPercent() <= .5 && !halfHealth)
        {
            halfHealth = true;
            Phase1 p1 = GetComponent<Phase1>();
            p1.atk1cd = Math.Ceiling(p1.atk1cd / 1.5);
            p1.atk2cd = Math.Ceiling(p1.atk2cd / 1.5);

        }

        if (health <= 0)
        {
            Debug.Log("Dead");
            animator.SetBool("Dead", true);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;

        }
    }

    protected void HurtDone()
    {
        animator.SetBool("Hurt", false);
        animator.SetBool("Invincible", false);
        CheckDeath();

    }
    protected void OnHitBecomeInvicible()
    {
        animator.SetBool("Invincible", true);
    }

    private float CalculateHealthPercent()
    {
        return (float)health / (float)maxHealth;
    }

}
