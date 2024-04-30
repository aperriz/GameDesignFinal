using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class MeleeWeapon : Weapon
{
    [SerializeField]
    public int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
       //Debug.Log(collision.name);
       if(collision.name != "Player" && collision.GetType() != typeof(CircleCollider2D))
        {
            if (collision.GetComponentInChildren<EnemyRecieveDamage>() != null)
            {
                //Debug.Log("Hit " + damage);
                collision.GetComponentInChildren<EnemyRecieveDamage>().DealDamage(damage);
            }
            else if(collision.GetComponentInChildren<Boss1RecieveDamage>() != null)
            {
                //Debug.Log("Hit " + damage);
                collision.GetComponentInChildren<Boss1RecieveDamage>().DealDamage(damage);
            }
            else if(collision.GetComponentInChildren<Boss2RecieveDamage>() != null)
            {
                //Debug.Log("Hit " + damage);
                collision.GetComponentInChildren<Boss2RecieveDamage>().DealDamage(damage);
            }
        }
    }
}
