using Pathfinding;
using Pathfinding.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedPlayerDetector : MonoBehaviour
{
    [SerializeField]
    int detectionRange = 20;
    [SerializeField]
    AIDestinationSetter destinationSetter;
    [SerializeField]
    AIPath aiPath;
    [SerializeField]
    EnemyRecieveDamage healthCheck;
    GameObject player;

    private void Awake()
    {
        player = GameObject.Find("Player");
        destinationSetter.target = player.transform;
    }

    private void FixedUpdate()
    {
       
        if (!(healthCheck.health <= 0))
        {
            //Debug.Log("Alive");
            RaycastHit2D losRay = Physics2D.Raycast(transform.position, player.transform.position - transform.position, detectionRange, LayerMask.GetMask("Player", "Walls"));
            RaycastHit2D rangedRay = Physics2D.Raycast(transform.position, player.transform.position - transform.position, 10, LayerMask.GetMask("Player", "Walls"));
            Debug.DrawRay(transform.position, player.transform.position - transform.position);
            if (losRay.collider != null)
            {
                //Debug.Log(ray.collider.name);
                if (losRay.collider.CompareTag(player.tag))
                {
                    if (destinationSetter.enabled == false)
                    {
                        destinationSetter.enabled = true;
                    }

                    if (rangedRay.collider != null)
                    {
                        if (rangedRay.collider.CompareTag(player.tag))
                        {
                            aiPath.enabled = false;
                        }
                        else
                        {
                            aiPath.enabled = true;
                        }
                    }
                    else
                    {
                        aiPath.enabled = true;
                    }

                    //Debug.Log("Detected player");
                    Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.green);
                }
                else
                {
                    Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.red);
                }

            }
            else
            {
                Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.red);
            }
        }
    }
}
