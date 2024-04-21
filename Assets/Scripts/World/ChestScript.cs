using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class ChestScript : MonoBehaviour
{
    [SerializeField]
    new BoxCollider2D collider;
    [SerializeField]
    Animator animator;
    [SerializeField]
    new AudioSource audio;
    [SerializeField]
    AudioClip audioClip;
    TileMapVisualizer tileMapVisualizer;
    bool open = false;
    int level = 1;

    int spawnQuantity = 2;

    HashSet<Vector2> spawnedItemPositions = new HashSet<Vector2>();

    private void Awake()
    {
        tileMapVisualizer = GameObject.Find("TilemapVisualizer").GetComponent<TileMapVisualizer>();

        level = GameObject.Find("RoomFirstDungeonGenerator").GetComponent<RoomFirstDungeonGenerator>().level;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player" && !open)
        {
            open = true;
            animator.enabled = true;
            audio.clip = audioClip;
            audio.Play();
            Destroy(collider);
            SpawnItems();
        }
    }


    private void SpawnItems()
    {
        Vector2 spawnPos = new Vector2(transform.position.x + Random.Range(-2, 3), transform.position.y + Random.Range(-2, 3));
        int loopsWithoutPlacing = 0;
        while (true && open)
        {
            for (int i = 0; i < Random.Range(1, spawnQuantity + (int)Mathf.Floor(level/3)); i++)
            {
                if (!spawnedItemPositions.Contains(spawnPos) && (Vector3)spawnPos != transform.position && 
                    tileMapVisualizer.floorMap.HasTile(new Vector3Int((int)spawnPos.x, (int)spawnPos.y, 0)))
                {
                    SpawnRandomItem(spawnPos);
                    spawnedItemPositions.Add(spawnPos);
                    loopsWithoutPlacing = 0;
                    break;
                }
                else
                {
                    spawnPos = new Vector2(transform.position.x + Random.Range(-2, 2), transform.position.y);
                    loopsWithoutPlacing++;
                }
                if (loopsWithoutPlacing >= 8)
                {
                    break;
                }
            }
            if (loopsWithoutPlacing >= 8)
            {
                break;
            }
        }

    }

    private void SpawnRandomItem(Vector2 spawnPos)
    {
        string[] potionTypes = { "health", "speed", "defense" };
        if (Random.Range(1, 4) < 3)
        {
            Instantiate(Resources.Load("Prefabs/World/Potion Prefab") as GameObject, spawnPos, Quaternion.Euler(0, 0, 0), transform).
            GetComponent<PotionItem>().type = potionTypes[Random.Range(0, potionTypes.Length)];
        }
        else
        {
            Instantiate(Resources.Load("Prefabs/World/Scroll Prefab") as GameObject, spawnPos, Quaternion.Euler(0, 0, 0), transform).
            GetComponent<PotionItem>().type = potionTypes[Random.Range(0, potionTypes.Length)];
        }


    }

}
