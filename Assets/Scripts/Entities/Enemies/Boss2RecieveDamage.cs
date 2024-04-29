using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Boss2RecieveDamage : MonoBehaviour
{
    public float health;
    public float maxHealth;
    [SerializeField]
    protected Animator animator;
    protected AudioSource audioSource;
    public AudioClip audioClip;
    GameObject player;
    GameObject healthBar;
    Slider healthBarSlider;
    [SerializeField]
    Phase2 phaseSwitch;
    bool halfHealth = false;
    [SerializeField]
    AudioClip deathSound;
    [SerializeField]
    UnityEvent onDeath;
    PlayerMovement pInput;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClip;
        player = GameObject.Find("Player");
        healthBar = player.transform.GetChild(1).GetChild(1).gameObject;
        healthBarSlider = healthBar.GetComponent<Slider>();
        healthBar.GetComponentInChildren<TextMeshProUGUI>().text = "Soul of the Phoenix";
    }

    private void Awake()
    {
        health = maxHealth;

    }

    public void DealDamage(int damage)
    {

        if (!animator.GetBool("Invincible") && !animator.GetBool("Hurt"))
        {
            /*if (audioSource == null)
            {
                audioSource = GetComponent<AudioSource>();
            }
            audioSource.clip = audioClip;*/
            audioSource.Play();
            healthBarSlider.enabled = true;
            healthBar.SetActive(true);
            //Debug.Log("Ow");

            health -= damage;

            animator.SetFloat("Health", CalculateHealthPercent());

            healthBarSlider.value = CalculateHealthPercent();

            //Debug.Log("Dealing damage");
            /*audioSource.Play();*/
            
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
        

        if (health <= 0)
        {
            pInput = player.GetComponent<PlayerMovement>();
            pInput.paused = true;
            //Time.timeScale = 0;
            Debug.Log("Dead");
            animator.SetBool("Dead", true);
            if (gameObject.GetComponent<BoxCollider2D>() != null)
            {
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }
            audioSource.clip = deathSound;
            audioSource.Play();

            onDeath?.Invoke();

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

    public void DeathSequence()
    {
        pInput.enabled = true;
        Time.timeScale = 1;
        SceneManager.LoadScene("Boss Phase 2", LoadSceneMode.Single);
    }

    public void StartTransition()
    {
        animator.SetBool("Invincible", true);
    }

    public void EndTransition()
    {
        animator.SetBool("Invincible", false);
    }
}
