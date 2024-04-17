using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    [SerializeField]
    int detectionRange = 10;
    GameObject player;

    private void Awake()
    {
        player = GameObject.Find("Player");
    }

    private void FixedUpdate()
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, player.transform.position - transform.position, detectionRange, LayerMask.GetMask("Player", "Walls"));
        if(ray.collider != null)
        {
            Debug.Log(ray.collider.name);
            if (ray.collider.CompareTag(player.tag))
            {
                if (!GetComponent<AIDestinationSetter>().enabled)
                {
                    GetComponent<AIDestinationSetter>().enabled = true;
                    GetComponent<AIDestinationSetter>().target = player.transform;
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
