using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UIElements;

public class MeleeWeapon : Weapon
{
    [SerializeField]
    public int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
       Debug.Log(collision.name);
       if(collision.name != "Player" && collision.GetType() != typeof(CircleCollider2D))
        {
            if (collision.GetComponent<EnemyRecieveDamage>() != null)
            {
                collision.GetComponent<EnemyRecieveDamage>().DealDamage(damage);
            }
        }
    }
}
