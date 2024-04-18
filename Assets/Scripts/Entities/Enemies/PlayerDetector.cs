using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    [SerializeField]
    int detectionRange = 10;
    [SerializeField]
    AIDestinationSetter destinationSetter;
    [SerializeField]
    EnemyRecieveDamage healthCheck;
    GameObject player;

    private void Awake()
    {
        player = GameObject.Find("Player");
    }

    private void FixedUpdate()
    {
        if(healthCheck != null)
        {
            if (!(healthCheck.health <= 0))
            {
                RaycastHit2D ray = Physics2D.Raycast(transform.position, player.transform.position - transform.position, detectionRange, LayerMask.GetMask("Player", "Walls"));
                if (ray.collider != null)
                {
                    Debug.Log(ray.collider.name);
                    if (ray.collider.CompareTag(player.tag))
                    {
                        if (!destinationSetter.enabled)
                        {
                            destinationSetter.enabled = true;
                            destinationSetter.target = player.transform;
                        }

                        Debug.Log("Detected player");
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
}
