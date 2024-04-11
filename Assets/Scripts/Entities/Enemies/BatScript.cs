using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatScript : EnemyRecieveDamage
{
    public float speed;
    public int damage;
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, GameObject.Find("Player").transform.position, Time.deltaTime*speed);

    }

    private void MetalOnFleshHit()
    {
        audioSource.Play();
    }


}
