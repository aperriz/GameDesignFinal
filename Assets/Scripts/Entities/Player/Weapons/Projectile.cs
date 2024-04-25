using System;
using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage;
    Rigidbody2D rb;
    [SerializeField]
    public int speed = 10;
    Collider2D col;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();

        col.enabled = false;

        SetStraightVelocity();

        StartCoroutine(ProjectileBuffer());

        StartCoroutine(DestroyProjectile());
    }

    private void SetStraightVelocity()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.name != "Player" && collision.GetType() != typeof(CircleCollider2D) && collision.GetComponent<PlayerItem>() == null)
        {
            EnemyRecieveDamage enemy = collision.GetComponent<EnemyRecieveDamage>();
            if (enemy != null)
            {
                enemy.DealDamage(damage);
            }
            else if(collision.GetComponent<Boss1RecieveDamage>() != null)
            {
                collision.GetComponent<Boss1RecieveDamage>().DealDamage(damage);
            }
            Destroy(gameObject);
        }
        
    }

    private IEnumerator DestroyProjectile()
    {
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }

    private IEnumerator ProjectileBuffer()
    {
        yield return new WaitForSeconds(0.07f);
        col.enabled = true;
    }
}
