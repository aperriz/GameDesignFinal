using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class WeaponControlScript : MonoBehaviour
{
    public float speed;
    private Vector2 direction;
    private Animator animator;
    private SpriteRenderer renderer;
    public int damage;

    private void Update()
    {
        if (enabled)
        {
            transform.position = GameObject.Find("Player").transform.position;
        }

    }

    public void ResetAttack()
    {
        GameObject.Find("Player").GetComponent<Animator>().SetBool("Attacking", false);
        Destroy(this.gameObject);
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       Debug.Log(collision.name);
       if(collision.name != "Player" && collision.GetType() != typeof(CircleCollider2D))
        {
            if (collision.GetComponent<EnemyRecieveDamage>() != null)
            {
                collision.GetComponent<EnemyRecieveDamage>().DealDamage(damage);
            }
        }
    }
}
