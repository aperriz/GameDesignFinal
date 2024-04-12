using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : EnemyDealDamage
{

    private void Awake()
    {
        float size;
        if(gameObject.GetComponent<SpriteRenderer>().bounds.size.x > gameObject.GetComponent<SpriteRenderer>().bounds.size.y)
        {
            size = gameObject.GetComponent<SpriteRenderer>().bounds.size.x;
        }
        else
        {
            size = gameObject.GetComponent<SpriteRenderer>().bounds.size.y;
        }

        range = size/6;
    }
    
    void Attack()
    {
        Vector2 playerPos = GameObject.Find("Player").transform.position;
        Vector2 myPos = transform.position;
        Vector2 direction = (playerPos - myPos).normalized;
        float angle = Mathf.Atan2(
            (GameObject.Find("Player").transform.position- transform.position).normalized.y, 
            (GameObject.Find("Player").transform.position - transform.position).normalized.x) * Mathf.Rad2Deg;

        if(angle > 90 || angle < -90)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (gameObject.GetComponent<SpriteRenderer>().flipX)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }

        if (!GetComponent<Animator>().GetBool("Attacking"))
        {
            GetComponent<Animator>().SetBool("Attacking", true);
            GameObject.Find("Player").GetComponent<PlayerRecieveDamage>().DealDamage(damage);
            StartCoroutine(AttackCooldown());
        }
    }
}
