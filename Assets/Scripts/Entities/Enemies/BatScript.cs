using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatScript : EnemyRecieveDamage
{
    
    
    private void Update()
    {
        if (!animator.GetBool("Dead"))
        {
            transform.position = Vector3.MoveTowards(transform.position, GameObject.Find("Player").transform.position, Time.deltaTime * speed);
        }

    }

    private void MetalOnFleshHit()
    {
        audioSource.Play();
    }


}
