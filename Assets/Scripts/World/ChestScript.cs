using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

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

        level = RoomFirstDungeonGenerator.level;
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
        Vector2Int spawnPos = new Vector2Int((int)Mathf.Round(transform.position.x) + Random.Range(-2, 3), (int)Mathf.Round(transform.position.y) + Random.Range(-2, 3));
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
                if (!spawnedItemPositions.Contains(spawnPos) && new Vector3(spawnPos.x, spawnPos.y, transform.position.z) != transform.position && 
                    tileMapVisualizer.floorMap.HasTile(new Vector3Int((int)Mathf.Round(spawnPos.x), (int)Mathf.Round(spawnPos.y), 0)) && !placed
                    && !PropPlacementManager.propLocations.Contains(spawnPos))
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
                    Debug.Log(placed);
                    spawnPos = new Vector2Int((int)Mathf.Round(transform.position.x) + Random.Range(-2, 2), (int)Mathf.Round(transform.position.y));
                    loopsWithoutPlacing++;
                }
                if (loopsWithoutPlacing >= 8)
                {
                    Debug.Log("8 Loops");
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
        int roll = Random.Range(1, 11);
        if (roll < 6)
        {
            SpawnPotion(spawnPos);
        }
        else if (roll < 9)
        {
            SpawnScroll(spawnPos);
        }
        else
        {
            
            SpawnWeapon(spawnPos);
        }
    }

    private void SpawnScroll(Vector2 spawnPos)
    {
        GameObject ScrollObject = Instantiate(Resources.Load("Prefabs/World/Scroll Prefab") as GameObject, new Vector3(spawnPos.x, spawnPos.y, -10), Quaternion.Euler(0, 0, 0), transform);

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
        GameObject PotionObject = Instantiate(Resources.Load("Prefabs/World/Potion Prefab") as GameObject, new Vector3(spawnPos.x, spawnPos.y, -10), Quaternion.Euler(0, 0, 0), transform);

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

    private void SpawnWeapon(Vector2 spawnPos)
    {
        GameObject weaponObject = Instantiate(Resources.Load("Prefabs/World/WeaponItem Prefab") as GameObject, new Vector3(spawnPos.x, spawnPos.y, -10), Quaternion.Euler(0, 0, 0), transform);

        int roll = Random.Range(1, 4);
        int minPlus = (int)Mathf.Floor(level / 2) - 1;
        int maxPlus = (int)Mathf.Ceil(level / 2) + 1;
        if (minPlus < 0)
        {
            minPlus = 0;
        }

        if (maxPlus < 2)
        {
            maxPlus = 2;
        }
        int damageRoll = (int)Random.Range(minPlus, maxPlus);
        WeaponItem weaponItem = weaponObject.GetComponent<WeaponItem>();

        switch (roll)
        {
            case 1:
                weaponItem.type = "Sword";
                break;
            case 2:
                weaponItem.type = "Bow";
                break;
            case 3:
                weaponItem.type = "Staff";
                break;
        }

        weaponItem.bonusDamage = damageRoll;
    }
}
