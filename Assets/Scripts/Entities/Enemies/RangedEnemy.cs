using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    public int damage;
    [SerializeField]
    protected float range = 0.98f;
    protected CircleCollider2D attackCol;
    protected bool attacking = false;
    public float cooldown;
    [SerializeField]
    private Animator animator;
    GameObject player;
    bool canAttack = true;
    [SerializeField]
    SpriteRenderer renderer;
    [SerializeField]
    private GameObject projectile;

    private void Start()
    {
        attackCol = GetComponent<CircleCollider2D>();
        attackCol.radius = range;
        player = GameObject.Find("Player");
    }

    protected void FixedUpdate()
    {
        //Debug.Log(Vector2.Distance(transform.position, player.transform.position));
        Vector3 myPos = transform.position;
        Vector3 playerPos = player.transform.position;

        if (Vector2.Distance(myPos, playerPos) <= range)
        {
            Attack();
        }

        if (animator.GetBool("Dead"))
        {
            Destroy(this);
        }

    }

    protected void Attack()
    {
        //Debug.Log("Attacking");
        if (!animator.GetBool("Attacking") && canAttack && Vector2.Distance(transform.position, player.transform.position) <= range)
        {
            if ((transform.position - player.transform.position).x < 0)
            {
                renderer.flipX = false;
            }
            else
            {
                renderer.flipX = true;
            }

            canAttack = false;
            animator.SetBool("Attacking", true);
            StartCoroutine(AttackCooldown());
        }
    }

    public void Shoot()
    {
        Vector2 dir = (player.transform.position - transform.position);

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        Debug.Log(angle);

        Instantiate(projectile, transform.position, Quaternion.Euler(0, 0, angle));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerRecieveDamage>(out PlayerRecieveDamage player) && canAttack)
        {
            Attack();
        }
    }

    protected void AttackDone()
    {
        animator.SetBool("Attacking", false);
    }

    protected IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(cooldown + animator.GetCurrentAnimatorClipInfo(0).Length);
        canAttack = true;
        if (Vector2.Distance(transform.position, player.transform.position) <= range || range == 0)
        {
            Debug.Log("I want to attack!");
            Attack();
        }
        else
        {
            Debug.Log(range);
            Debug.Log(Vector2.Distance(transform.position, player.transform.position));
            Debug.Log("Out of range");
            animator.SetBool("Attacking", false);
        }
    }
}
