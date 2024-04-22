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
    [SerializeField]
    protected Animator animator;
    protected AudioSource audioSource;
    public AudioClip audioClip;
    public float speed;
    protected float tempSpeed;
    GameObject player;
    [SerializeField]
    public int weight = 1;
    [SerializeField]
    int xForce = 2, yForce = 2;
    public GameObject healthBar;
    public Slider healthBarSlider;
    [SerializeField]
    public int aggressionRange = 20;
    public AIPath ai = null;

    // Start is called before the first frame update
    void Start()
    {
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
        if(ai != null)
        {
            if (ai.enabled == false)
            {
                ai.enabled = true;
            }
        }

        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClip;
        healthBarSlider.enabled = true;
        healthBar.SetActive(true);
        Debug.Log("Ow");

        healthBarSlider.value = CalculateHealthPercent();

        if (!animator.GetBool("Invincible"))
        {
            Debug.Log("Dealing damage");
            audioSource.Play();
            health -= damage;
            animator.SetBool("Hurt", true);
            animator.SetBool("Invincible", true);
            CheckDeath();
        }
    }

    public void DealSpellDamage(int damage)
    {
        if (ai != null)
        {
            if (ai.enabled == false)
            {
                ai.enabled = true;
            }
        }

        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClip;
        healthBarSlider.enabled = true;
        healthBar.SetActive(true);
        Debug.Log("Ow");

        healthBarSlider.value = CalculateHealthPercent();

        if (!animator.GetBool("Invincible"))
        {
            Debug.Log("Dealing damage");
            audioSource.Play();
            health -= damage;
            animator.SetBool("Hurt", true);
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

    public void DeadClear()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        //Destroy(gameObject);
        foreach(var comp in GetComponents<Component>())
        {
            if(comp != animator && comp != GetComponent<Transform>() && comp != GetComponent<SpriteRenderer>())
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
    protected void OnHitBecomeInvicible()
    {
        animator.SetBool("Invincible", true);
    }

    private float CalculateHealthPercent()
    {
        return (float)health / (float)maxHealth;
    }

}
