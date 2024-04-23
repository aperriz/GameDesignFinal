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
            GameObject.Find("Player").GetComponent<PlayerExtraStats>().UpdateGold(Random.Range(1*level, (10*level) + 1));
            //Debug.Log("Opened Chest");
        }
    }


    private void SpawnItems()
    {
        Vector2 spawnPos = new Vector2(transform.position.x + Random.Range(-2, 3), transform.position.y + Random.Range(-2, 3));
        int loopsWithoutPlacing = 0;
        
        Debug.Log("Max spawns: " + (spawnQuantity + (int)Mathf.Floor(level / 3)));
        int chestSpawns = Random.Range(1, spawnQuantity + (int)Mathf.Floor(level / 3));
        Debug.Log("Chest spawns " + chestSpawns);
        int spawns = 0;

        bool placed = false;
        while (spawns < chestSpawns)
        {
            for (int i = 0; i < chestSpawns; i++)
            {
                placed = false;
                if (!spawnedItemPositions.Contains(spawnPos) && (Vector3)spawnPos != transform.position && 
                    tileMapVisualizer.floorMap.HasTile(new Vector3Int((int)spawnPos.x, (int)spawnPos.y, 0)) && !placed )
                {
                    placed = true;
                    Debug.Log("Spawning item");
                    SpawnRandomItem(spawnPos);
                    spawnedItemPositions.Add(spawnPos);
                    loopsWithoutPlacing = 0;
                    spawns++;
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
                spawns++;
            }
        }

    }

    private void SpawnRandomItem(Vector2 spawnPos)
    {
        if (Random.Range(1, 4) < 3)
        {
            SpawnPotion(spawnPos);
        }
        else
        {
            SpawnScroll(spawnPos);
        }
    }

    private void SpawnScroll(Vector2 spawnPos)
    {
        GameObject ScrollObject = Instantiate(Resources.Load("Prefabs/World/Scroll Prefab") as GameObject, spawnPos, Quaternion.Euler(0, 0, 0), transform);

        int scrollType = Random.Range(0, 101);
        if(scrollType <= 60)
        {
            ScrollObject.GetComponent<ScrollItem>().type = "Fire Storm";
        }
        else if(scrollType <= 90)
        {
            ScrollObject.GetComponent<ScrollItem>().type = "Holy Radiance";
        }
        else
        {
            ScrollObject.GetComponent<ScrollItem>().type = "Shield";
        }
    }
    private void SpawnPotion(Vector2 spawnPos)
    {
        GameObject PotionObject = Instantiate(Resources.Load("Prefabs/World/Potion Prefab") as GameObject, spawnPos, Quaternion.Euler(0, 0, 0), transform);

        int potionType = Random.Range(0, 101);
        if(potionType <= 70)
        {
            PotionObject.GetComponent<PotionItem>().type = "health";
        }
        else if(potionType <= 90)
        {
            PotionObject.GetComponent<PotionItem>().type = "speed";
        }
        else
        {
            PotionObject.GetComponent<PotionItem>().type = "defense";
        }
    }

}
