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
    
}
