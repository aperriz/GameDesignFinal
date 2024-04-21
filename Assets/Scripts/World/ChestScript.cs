using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class ChestScript : MonoBehaviour
{
    [SerializeField]
    BoxCollider2D collider;
    [SerializeField]
    Animator animator;
    [SerializeField]
    AudioSource audio;
    [SerializeField]
    AudioClip audioClip;

    HashSet<Vector2> spawnedItemPositions = new HashSet<Vector2>();

    private void Awake()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Player")
        {
            animator.enabled = true;
            audio.clip = audioClip;
            audio.Play();
            Destroy(collider);
            SpawnItems();
        }
    }


    private void SpawnItems()
    {
        Vector2 spawnPos = new Vector2(transform.position.x + Random.Range(-2, 2), transform.position.y);
        while (true)
        {
            if (!spawnedItemPositions.Contains(spawnPos))
            {
                Instantiate(Resources.Load("Prefabs/World/Potion Prefab") as GameObject, spawnPos, Quaternion.Euler(0, 0, 0)).GetComponent<PotionItem>().type = "defense";
                spawnedItemPositions.Add(spawnPos);
                break;
            }
            else
            {
                spawnPos = new Vector2(transform.position.x + Random.Range(-2, 2), transform.position.y);
            }
        }


    }

}
