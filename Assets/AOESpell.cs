using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOESpell : MonoBehaviour
{
    [SerializeField]
    public int damage = 5;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        EnemyRecieveDamage enemy = collision.GetComponentInChildren<EnemyRecieveDamage>();
        Debug.Log(enemy);
        if(enemy != null)
        {
            enemy.DealSpellDamage(damage);
        }

    }
    
    public void AnimationDone()
    {
        Destroy(gameObject);
    }

}
