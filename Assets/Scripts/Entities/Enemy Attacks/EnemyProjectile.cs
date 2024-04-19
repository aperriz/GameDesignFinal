using System.Collections;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public int damage;
    Rigidbody2D rb;
    [SerializeField]
    public int speed = 10;
    [SerializeField]
    Animator animator;
    [SerializeField]
    bool hitEffect = false;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        SetStraightVelocity();

        StartCoroutine(DestroyProjectileTimer());
    }

    private void SetStraightVelocity()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.GetComponent<EnemyRecieveDamage>() == null)
        {
            PlayerRecieveDamage player = collision.GetComponent<PlayerRecieveDamage>();
            if (player != null)
            {
                player.DealDamage(damage);
            }
            rb.velocity = Vector2.zero;
            DestroyProjectileSequence();
        }
        
    }

    private IEnumerator DestroyProjectileTimer()
    {
        yield return new WaitForSeconds(10);
        DestroyProjectileSequence();
    }

    private void DestroyProjectileSequence()
    {
        if(hitEffect)
        {
            animator.enabled = true;
        }
        else
        {
            DestroyProjectile();
        }
    }

    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }
    
}
