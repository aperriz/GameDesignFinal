using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class CorridorFirstDungeonGenerator : SimpleRandomWalkGenerator
{
    [SerializeField]
    private int corLength = 14, corCount = 5;
    [SerializeField]
    [Range(0.1f, 1)]
    private float roomPercent;

    private Dictionary<Vector2Int, HashSet<Vector2Int>> roomsDictionary = new Dictionary<Vector2Int, HashSet<Vector2Int>>();
    HashSet<Vector2Int> floorPos, corrPos;
    private List<Color> roomColors = new List<Color>();

    public UnityEvent OnFinishedRoomGeneration;

    protected override void RunProceduralGeneration()
    {
        CorridorFirstGeneratiion();
    }

    private void CorridorFirstGeneratiion()
    {
        HashSet<Vector2Int> floorPos = new HashSet<Vector2Int>();
        HashSet<Vector2Int> potentialRoomPos = new HashSet<Vector2Int>();


        CreateCorridors(floorPos, potentialRoomPos);

        HashSet<Vector2Int> roomPos = CreateRooms(potentialRoomPos);

        List<Vector2Int> deadEnds = FindAllDeadEnds(floorPos);

        CreateRoomsAtDeadEnds(deadEnds, roomPos);

        floorPos.UnionWith(roomPos);


        tileMapVisualizer.PaintFloorTiles(floorPos);
        WallGenerator.CreateWalls(floorPos, tileMapVisualizer);
    }

    private List<Vector2Int> IncreaseCorridorBurhs3by3(List<Vector2Int> corridor)
    {
        List<Vector2Int> newCorridor = new List<Vector2Int>();
        for(int i = 1;i < corridor.Count; i++)
        {
            for(int x = -1; x < 2; x++)
            {
                for(int y = -1; y < 2; y++)
                {
                    newCorridor.Add(corridor[i-1] + new Vector2Int(x, y));
                }
            }
        }
        return newCorridor;
    }

    private List<Vector2Int> IncreaseCorridorSizebyOne(List<Vector2Int> corridor)
    {
        List<Vector2Int> newCorridor = new List<Vector2Int>();
        Vector2Int previewDirection = Vector2Int.zero;
        for(int i = 0;i < corridor.Count; i++)
        {
            Vector2Int directionFromCell = corridor[i] - corridor[i - 1];
            if(previewDirection != Vector2Int.zero && directionFromCell != previewDirection)
            {
                for(int x = -1; x < 2; x++)
                {
                    for(int y = -1; y < 2; y++)
                    {
                        newCorridor.Add(corridor[i-1] + new Vector2Int(x,y));
                    }
                }

                previewDirection = directionFromCell;
            }
            else
            {
                Vector2Int newCorridorTileoffset = GetDirection90From(directionFromCell);
                newCorridor.Add(corridor[i - 1]);
                newCorridor.Add(corridor[i-1]+newCorridorTileoffset);
            }
        }
        return newCorridor;
    }

    private Vector2Int GetDirection90From(Vector2Int dir)
    {
        if(dir == Vector2Int.up)
            return Vector2Int.right;
        if(dir == Vector2Int.right)
            return Vector2Int.down;
        if(dir == Vector2Int.down)
            return Vector2Int.left;
        if( dir == Vector2Int.left)
            return Vector2Int.up;
        return Vector2Int.zero;
    }

    private void CreateRoomsAtDeadEnds(List<Vector2Int> deadEnds, HashSet<Vector2Int> roomFloors)
    {
        foreach (var pos in deadEnds)
        {
            if (!roomFloors.Contains(pos))
            {
                var room = RunRandomWalk(randomWalkParameters, pos);
                roomFloors.UnionWith(room);
            }
        }
    }

    private List<Vector2Int> FindAllDeadEnds(HashSet<Vector2Int> floorPos)
    {
        List<Vector2Int> deadEnds = new List<Vector2Int>();

        foreach (var pos in floorPos)
        {
            int neighboursCount = 0;
            foreach (var dir in Direction2D.cardinalDirList)
            {
                if (floorPos.Contains(pos + dir))
                {
                    neighboursCount++;
                }
            }
            if (neighboursCount == 1)
            {
                deadEnds.Add(pos);
            }
        }

        return deadEnds;
    }

    private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialRoomPos)
    {
        HashSet<Vector2Int> roomPos = new HashSet<Vector2Int>();

        int roomCreateCount = Mathf.RoundToInt(potentialRoomPos.Count * roomPercent);

        List<Vector2Int> roomsToCreate = potentialRoomPos.OrderBy(x => Guid.NewGuid()).Take(roomCreateCount).ToList();
        ClearRoomData();

        foreach (var room in roomsToCreate)
        {
            var roomFloor = RunRandomWalk(randomWalkParameters, room);
            SaveRoomData(room, roomFloor);
            roomPos.UnionWith(roomFloor);
        }

        return roomPos;
    }

    private void SaveRoomData(Vector2Int room, HashSet<Vector2Int> roomFloor)
    {
        roomsDictionary[room] = roomFloor;
        roomColors.Add(UnityEngine.Random.ColorHSV());
    }

    private void ClearRoomData()
    {
        roomsDictionary.Clear();
        roomColors.Clear();
    }

    private void CreateCorridors(HashSet<Vector2Int> floorPos, HashSet<Vector2Int> potentialRoomPos)
    {
        var curPos = startPos;
        potentialRoomPos.Add(curPos);
        List<List<Vector2Int>> corridors = new List<List<Vector2Int>>();

        for (int i = 0; i < corCount; i++)
        {
            var cor = ProceduralGenerationAlgorithms.RandomWalkCorridor(curPos, corLength);
            corridors.Add(cor);
            curPos = cor[cor.Count - 1];
            potentialRoomPos.Add(curPos);

            floorPos.UnionWith(cor);
        }
        corrPos = new HashSet<Vector2Int>(floorPos);

    }

}
