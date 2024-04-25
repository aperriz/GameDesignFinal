using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : Weapon
{
    [SerializeField]
    GameObject projectile;
    [SerializeField]
    int speed = 10;
    public int projectileDamage;

    public void Shoot()
    {

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = ((Vector2)transform.position - mousePos);

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        Vector3 spawnPos = new Vector3(dir.x, dir.y, angle);


        Instantiate(projectile, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation).GetComponent<Projectile>().damage = projectileDamage;
    }

    
}
