using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEngine;

public class BatScript : EnemyRecieveDamage
{

    private void Awake()
    {
        weight = 2;
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        player = GameObject.Find("Player");

    }

    private void MetalOnFleshHit()
    {
        audioSource.Play();
    }


}
