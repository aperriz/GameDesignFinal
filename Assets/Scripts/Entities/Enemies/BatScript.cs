using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatScript : Enemy
{
    
    
    private void Update()
    {
        if(Vector2.Distance(transform.position, player.transform.position) <= aggressionRange)
        {
            aggroed = true;
        }
        else
        {
            aggroed = false;
        }

        if (!animator.GetBool("Dead") && aggroed)
        {
            transform.position = Vector3.MoveTowards(transform.position, GameObject.Find("Player").transform.position, Time.deltaTime * speed);
        }

    }

    private void MetalOnFleshHit()
    {
        audioSource.Play();
    }


}
