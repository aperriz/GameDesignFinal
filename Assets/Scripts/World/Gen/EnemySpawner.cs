using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Cinemachine;
using static UnityEngine.EventSystems.EventTrigger;

public class AgentPlacer : MonoBehaviour
{
    [SerializeField, Range(1, 10)]
    int level = 1;

    [SerializeField]
    private List<GameObject> enemyPrefabs;

    private float maxWeight, minWeight;

    private int playerRoomIndex = 0, bossRoomIndex;

    DungeonData dungeonData;

    [SerializeField]
    private bool showGizmo = false;

    [SerializeField]
    private GameObject[] minibosses;
/*
    [SerializeField]
    private GameObject playerPrefab;*/

    private void Awake()
    {
        dungeonData = FindObjectOfType<DungeonData>();
    }

    public void PlaceAgents()
    {
        maxWeight = 4 + (level * 2);
        minWeight = 2 + (level * 2);

        Debug.Log("Placing agents");
        bossRoomIndex = dungeonData.roomCount - 1;
        if (dungeonData == null)
            return;

        //Loop for each room
        for (int i = 0; i < dungeonData.Rooms.Count; i++)
        {

            Room room = dungeonData.Rooms[i];
            RoomGraph roomGraph = new RoomGraph(room.FloorTiles);


            HashSet<Vector2Int> roomFloor = new HashSet<Vector2Int>(room.FloorTiles);

            roomFloor.IntersectWith(dungeonData.Path);

            Dictionary<Vector2Int, Vector2Int> roomMap = roomGraph.RunBFS(roomFloor.First(), room.PropPositions);

            room.PositionsAccessibleFromPath = roomMap.Keys.OrderBy(x => Guid.NewGuid()).ToList();

            if (i == 0)
            {
                /*GameObject player = Instantiate(playerPrefab);*/
                GameObject player = GameObject.Find("Player");
                player.transform.localPosition = dungeonData.Rooms[i].RoomCenterPos + Vector2.one * 0.5f;
                player.name = "Player";
                //Make the camera follow the player
                dungeonData.PlayerReference = player;
            }
            else if (i == bossRoomIndex)
            {
                PlaceEnemies(room, "boss");
                i++;
                continue;
                
            }
            else
            {
                //PlaceEnemies(room, "");
            }
        }
    }

    /// <summary>
    /// Places enemies in the positions accessible from the path
    /// </summary>
    /// <param name="room"></param>
    /// <param name="enemysCount"></param>
    private void PlaceEnemies(Room room, string type)
    {
        int roomMaxWeight = 0;

        int k = 0;
        int roomWeight = 0;

        if (type != "boss" && level % 3 != 0)
        {
            roomMaxWeight = (int)Math.Round(UnityEngine.Random.Range(minWeight, maxWeight));
        }
        else if (level % 3 != 0)
        {
            roomMaxWeight = (int)Math.Round(UnityEngine.Random.Range(minWeight * 1.25f, maxWeight * 1.25f));
        }
        else if (type == "test")
        {
            roomMaxWeight = 1;
        }
        else if ((level % 3 == 0) && type == "boss")
        {
            roomMaxWeight = 0;
            switch (level)
            {
                case 3:
                    {
                        
                        GameObject enemy = Instantiate(minibosses[0]);
                        int enemyWeight = enemy.GetComponentInChildren<EnemyRecieveDamage>().weight;
                        enemy.transform.localPosition = (Vector2)room.PositionsAccessibleFromPath[k] + Vector2.one * 0.5f;
                        room.EnemiesInTheRoom.Add(enemy);
                        k++;
                        break;
                    }
                case 6:
                    {
                        GameObject enemy = Instantiate(minibosses[1]);
                        enemy.transform.localPosition = (Vector2)room.PositionsAccessibleFromPath[k] + Vector2.one * 0.5f;
                        room.EnemiesInTheRoom.Add(enemy);
                        k++;
                        break;
                    }
                case 9:
                    {
                        GameObject enemy = Instantiate(minibosses[2]);
                        enemy.transform.localPosition = (Vector2)room.PositionsAccessibleFromPath[k] + Vector2.one * 0.5f;
                        room.EnemiesInTheRoom.Add(enemy);
                        k++;
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        //Debug.Log(enemyPrefabs.Count);
        while (roomWeight <= roomMaxWeight)
        {
            //Debug.Log("Looking to spawn enemy");
            if (room.PositionsAccessibleFromPath.Count <= k)
            {
                //Debug.Log("Exiting");
                return;
            }

            HashSet<GameObject> spawnableEnemies = new HashSet<GameObject>();

            foreach (GameObject enemy in enemyPrefabs)
            {
                //Debug.Log("Looking through enemies");
                int enemyWeight = enemy.GetComponentInChildren<EnemyRecieveDamage>().weight;
                if (enemyWeight < roomMaxWeight - roomWeight)
                {
                    //Debug.Log("Found enemy");
                    spawnableEnemies.Add(enemy);
                }
            }


            if (spawnableEnemies.Count > 0)
            {
                //Debug.Log("Spawning enemy");
                GameObject enemy = Instantiate(enemyPrefabs[UnityEngine.Random.Range(0, enemyPrefabs.Count)]);
                int enemyWeight = enemy.GetComponentInChildren<EnemyRecieveDamage>().weight;
                roomWeight = roomWeight + enemyWeight;
                enemy.transform.localPosition = (Vector2)room.PositionsAccessibleFromPath[k] + Vector2.one * 0.5f;
                room.EnemiesInTheRoom.Add(enemy);
                k++;

            }
            else
            {
                break;
            }

        }
    }

    private void OnDrawGizmosSelected()
    {
        if (dungeonData == null || showGizmo == false)
            return;
        foreach (Room room in dungeonData.Rooms)
        {
            Color color = Color.green;
            color.a = 0.3f;
            Gizmos.color = color;

            foreach (Vector2Int pos in room.PositionsAccessibleFromPath)
            {
                Gizmos.DrawCube((Vector2)pos + Vector2.one * 0.5f, Vector2.one);
            }
        }
    }
}

public class RoomGraph
{
    public static List<Vector2Int> fourDirections = new()
    {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
    };

    Dictionary<Vector2Int, List<Vector2Int>> graph = new Dictionary<Vector2Int, List<Vector2Int>>();

    public RoomGraph(HashSet<Vector2Int> roomFloor)
    {
        foreach (Vector2Int pos in roomFloor)
        {
            List<Vector2Int> neighbours = new List<Vector2Int>();
            foreach (Vector2Int direction in fourDirections)
            {
                Vector2Int newPos = pos + direction;
                if (roomFloor.Contains(newPos))
                {
                    neighbours.Add(newPos);
                }
            }
            graph.Add(pos, neighbours);
        }
    }

    /// <summary>
    /// Creates a map of reachable tiles in our dungeon.
    /// </summary>
    /// <param name="startPos">Door position or tile position on the path between rooms inside this room</param>
    /// <param name="occupiedNodes"></param>
    /// <returns></returns>
    public Dictionary<Vector2Int, Vector2Int> RunBFS(Vector2Int startPos, HashSet<Vector2Int> occupiedNodes)
    {
        //BFS related variuables
        Queue<Vector2Int> nodesToVisit = new Queue<Vector2Int>();
        nodesToVisit.Enqueue(startPos);

        HashSet<Vector2Int> visitedNodes = new HashSet<Vector2Int>();
        visitedNodes.Add(startPos);

        //The dictionary that we will return 
        Dictionary<Vector2Int, Vector2Int> map = new Dictionary<Vector2Int, Vector2Int>();
        map.Add(startPos, startPos);

        while (nodesToVisit.Count > 0)
        {
            //get the data about specific position
            Vector2Int node = nodesToVisit.Dequeue();
            List<Vector2Int> neighbours = graph[node];

            //loop through each neighbour position
            foreach (Vector2Int neighbourPosition in neighbours)
            {
                //add the neighbour position to our map if it is valid
                if (visitedNodes.Contains(neighbourPosition) == false &&
                    occupiedNodes.Contains(neighbourPosition) == false)
                {
                    nodesToVisit.Enqueue(neighbourPosition);
                    visitedNodes.Add(neighbourPosition);
                    map[neighbourPosition] = node;
                }
            }
        }

        return map;
    }
}