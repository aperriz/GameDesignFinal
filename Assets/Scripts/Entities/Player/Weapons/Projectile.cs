using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage;
    Rigidbody2D rb;
    [SerializeField]
    public int speed = 10;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        SetStraightVelocity();

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
            Destroy(gameObject);
        }
        
    }

    private IEnumerator DestroyProjectile()
    {
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }
}
