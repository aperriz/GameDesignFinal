using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : ProjectileWeapon
{

    void Start()
    {

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 myPos = transform.position;
        Vector2 direction = (mousePos - myPos).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, Quaternion.identity.z + angle);    
    }

    
}
