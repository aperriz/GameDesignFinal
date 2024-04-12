using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDealDamage : MonoBehaviour
{
    public int damage;
    protected float range;
    protected CircleCollider2D attackCol;
    protected bool attacking = false;
    public int cooldown;

    private void Start()
    {
        attackCol = GetComponent<CircleCollider2D>();
        attackCol.radius = range;
    }

    protected void Update()
    {
        Vector3 myPos = transform.position;
        Vector3 playerPos = GameObject.Find("Player").transform.position;

        if (Vector2.Distance(myPos, playerPos) <= range)
        {
            Attack();
        }

        if (gameObject.GetComponent<Animator>().GetBool("Dead"))
        {
            Destroy(this);
        }

    }

    protected void Attack()
    {
        if (!GetComponent<Animator>().GetBool("Attacking"))
        {
            GetComponent<Animator>().SetBool("Attacking", true);
            GameObject.Find("Player").GetComponent<PlayerRecieveDamage>().DealDamage(damage);
            StartCoroutine(AttackCooldown());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerRecieveDamage>(out PlayerRecieveDamage player))
        {
            Attack();
        }
    }

    protected IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(cooldown + gameObject.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0).Length);
        if (Vector2.Distance(transform.position, GameObject.Find("Player").transform.position) <= range)
        {
            Attack();
        }
        else
        {
            GetComponent<Animator>().SetBool("Attacking", false);
        }
    }
}
