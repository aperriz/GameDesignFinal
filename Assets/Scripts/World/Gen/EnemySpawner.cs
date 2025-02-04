using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Cinemachine;
using static UnityEngine.EventSystems.EventTrigger;
using UnityEngine.Events;

public class AgentPlacer : MonoBehaviour
{
    int level = 1;

    [SerializeField]
    private List<GameObject> lowWeightEnemies, highWeightEnemies;

    [SerializeField]
    GameObject portalPrefab, dialogue;

    private float maxWeight, minWeight;

    private int bossRoomIndex;

    DungeonData dungeonData;
    [SerializeField]
    UnityEvent level1Dialogue;

    Transform enemyParent;

    public Room bossRoom = null;
    bool generated = false, portal = false;
    [SerializeField]
    private bool showGizmo = false;

    [SerializeField]
    private GameObject[] minibosses;
    /*
        [SerializeField]
        private GameObject playerPrefab;*/

    private void Awake()
    {
        enemyParent = GameObject.Find("Enemies").transform;
        dungeonData = FindObjectOfType<DungeonData>();
        level = GameManager.level;
    }

    private void FixedUpdate()
    {
        if (bossRoom != null && generated)
        {
            //Debug.Log("Boss Room not Null");
            
            //Debug.Log(generated);
            if (bossRoom.EnemiesInTheRoom.Count == 0 && !portal)
            {
                portal = true;
                Instantiate(portalPrefab, new Vector3(bossRoom.RoomCenterPos.x, bossRoom.RoomCenterPos.y, -15), Quaternion.identity);
                Debug.Log("Portal");
            }

            if (bossRoom.EnemiesInTheRoom.Count != 0)
            {
                //Debug.Log(bossRoom.EnemiesInTheRoom.Count());
                for (int i = 0; i < bossRoom.EnemiesInTheRoom.Count; i++)
                {
                    if (bossRoom.EnemiesInTheRoom.ElementAt(i).gameObject == null)
                    {
                        bossRoom.EnemiesInTheRoom.RemoveAt(i);
                        break;
                    }
                    else
                    {
                        //Debug.Log(bossRoom.EnemiesInTheRoom.ElementAt(i).gameObject);
                    }
                }
            }

        }

    }

    public void PlaceAgents()
    {
        generated = false;
        Room farthestRoom = dungeonData.Rooms[0];
        Room playerRoom = dungeonData.Rooms[0];
        float currentFarthest = Math.Abs(Vector3.Distance(farthestRoom.RoomCenterPos, playerRoom.RoomCenterPos));

        foreach (Room room in dungeonData.Rooms)
        {
            float roomDist = Math.Abs(Vector3.Distance(room.RoomCenterPos, playerRoom.RoomCenterPos));

            //Debug.Log(roomDist);
            if (roomDist >= currentFarthest)
            {
                Debug.Log("New Farthest: " + roomDist);
                currentFarthest = roomDist;
                farthestRoom = room;
            }
        }

        bossRoomIndex = dungeonData.Rooms.IndexOf(farthestRoom);
        bossRoom = farthestRoom;

        maxWeight = 3 + (level);
        minWeight = 1 + (level);

        //Debug.Log("Placing agents");
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
                //Debug.Log("Moving Player");
                /*GameObject player = Instantiate(playerPrefab);*/
                GameObject player = GameObject.Find("Player");
                Vector3 playerPos = dungeonData.Rooms[i].RoomCenterPos + Vector2.one * 0.5f;
                player.transform.localPosition = new Vector3(playerPos.x, playerPos.y, -10);
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
                PlaceEnemies(room, "");
            }
        }
        Debug.Log("Done Gen");
        MainMenu.loadingScreen.GetComponent<Canvas>().enabled = false;
        Time.timeScale = 1.0f;
        generated = true;
        if(GameManager.level == 1)
        {
            dungeonData.PlayerReference.GetComponent<PlayerMovement>().paused = true;
            Debug.Log(level);
            Debug.Log(GameManager.level);
            level1Dialogue?.Invoke();
        }
    }

    /// <summary>
    /// Places enemies in the positions accessible from the path
    /// </summary>
    /// <param name="room"></param>
    /// <param name="enemysCount"></param>
    private void PlaceEnemies(Room room, string type)
    {
        //Debug.Log("Placing Enemies");
        int roomMaxWeight = 0;

        int k = 0;
        int roomWeight = 0;

        if (type != "boss")
        {
            roomMaxWeight = (int)Math.Round(UnityEngine.Random.Range(minWeight * 1, maxWeight * 1));
        }
        else if (type == "boss")
        {
            roomMaxWeight = 0;
            switch (level % 9)
            {
                case 3:
                    {
                        Debug.Log("Placing dog");
                        GameObject enemy = Instantiate(minibosses[0], enemyParent);
                        int enemyWeight = enemy.GetComponentInChildren<EnemyRecieveDamage>().weight;
                        enemy.transform.localPosition = (Vector2)room.PositionsAccessibleFromPath[k] + Vector2.one * 0.5f;
                        room.EnemiesInTheRoom.Add(enemy);
                        k++;
                        return;
                    }
                case 6:
                    {
                        Debug.Log("Placing Jeoph");
                        GameObject enemy = Instantiate(minibosses[1], enemyParent);
                        enemy.transform.localPosition = (Vector2)room.PositionsAccessibleFromPath[k] + Vector2.one * 0.5f;
                        room.EnemiesInTheRoom.Add(enemy);
                        k++;
                        return;
                    }
                case 0:
                    {
                        Debug.Log("Placing Balrog");
                        GameObject enemy = Instantiate(minibosses[2], enemyParent);
                        enemy.transform.localPosition = (Vector2)room.PositionsAccessibleFromPath[k] + Vector2.one * 0.5f;
                        room.EnemiesInTheRoom.Add(enemy);
                        k++;
                        GameManager.enemyHealthMultiplier += 0.5f;
                        return;
                    }
                default:
                    {
                        Debug.Log("Default miniboss");
                        roomMaxWeight = roomMaxWeight = (int)Math.Round(UnityEngine.Random.Range(minWeight * 1.5f, maxWeight * 1.5f));
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
            HashSet<GameObject> highWeightSpawnables = new HashSet<GameObject>();
            HashSet<GameObject> lowWeightSpawnables = new HashSet<GameObject>();
            foreach (GameObject enemy in lowWeightEnemies)
            {
                //Debug.Log("Looking through enemies");
                int enemyWeight = enemy.GetComponentInChildren<EnemyRecieveDamage>().weight;
                if (enemyWeight < roomMaxWeight - roomWeight)
                {
                    //Debug.Log("Found enemy");
                    spawnableEnemies.Add(enemy);
                    if (enemyWeight >= 3)
                    {
                        highWeightSpawnables.Add(enemy);
                    }
                    else
                    {
                        lowWeightSpawnables.Add(enemy);
                    }
                }
            }


            if (spawnableEnemies.Count > 0 && room.EnemiesInTheRoom.Count <= 6 + (level * 2))
            {
                //Debug.Log("Spawning enemy");
                GameObject enemyPrefab = null;

                if (highWeightEnemies.Count > 0)
                {
                    if (UnityEngine.Random.Range(0f, 10f) <= (float)(9 * (Math.Pow(Math.Pow(2 / 9, 1 / 8), level - 1))))
                    {
                        enemyPrefab = lowWeightEnemies.ElementAt(UnityEngine.Random.Range(0, lowWeightSpawnables.Count));
                    }
                    else
                    {
                        enemyPrefab = highWeightEnemies.ElementAt(UnityEngine.Random.Range(0, highWeightEnemies.Count));
                    }
                }
                else
                {
                    enemyPrefab = lowWeightEnemies.ElementAt(UnityEngine.Random.Range(0, lowWeightSpawnables.Count));
                }

                GameObject enemy = Instantiate(enemyPrefab, enemyParent);
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

        foreach(var enemy in room.EnemiesInTheRoom)
        {
            enemy.GetComponentInChildren<EnemyRecieveDamage>().health *= GameManager.enemyHealthMultiplier;
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