using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public int damage;
    Rigidbody2D rb;
    [SerializeField]
    public int speed = 10;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        SetStraightVelocity();

        StartCoroutine(DestroyArrow());
    }

    private void SetStraightVelocity()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.name != "Player")
        {
            EnemyRecieveDamage enemy = collision.GetComponent<EnemyRecieveDamage>();
            if (enemy != null)
            {
                enemy.DealDamage(damage);
            }

            Destroy(gameObject);
        }
    }

    private IEnumerator DestroyArrow()
    {
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }
}
