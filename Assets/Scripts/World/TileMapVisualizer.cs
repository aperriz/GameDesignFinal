using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class TileMapVisualizer : MonoBehaviour
{
    public static TileMapVisualizer instance;

    [SerializeField]
    public Tilemap floorMap, wallMap;
    [SerializeField]
    private TileBase[] abnormalFloorTiles;
    [SerializeField]
    private TileBase wallBottomHalf, floorTrapTile, normalFloorTile, wallTop, wallSideRight, wallSiderLeft, wallBottom, wallFull,
        wallInnerCornerDownLeft, wallInnerCornerDownRight,
        wallDiagonalCornerDownRight, wallDiagonalCornerDownLeft, wallDiagonalCornerUpRight, wallDiagonalCornerUpLeft;
    [SerializeField, Range(0, 1)]
    private float percentAbnormal = 0.1f;
    [SerializeField, Range(0, 1)]
    private float percentTraps = 0.01f;

    HashSet<Vector2Int> potentialWallBottoms = new HashSet<Vector2Int>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions)
    {
        PaintTiles(floorPositions, floorMap, normalFloorTile);
        
    }

    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap map, TileBase tile)
    {
        foreach (var pos in positions)
        {
            float roll = Random.Range(0f, 1f);
            if(roll < percentAbnormal)
            {
                PaintSingleTile(map, abnormalFloorTiles[Random.Range(0,abnormalFloorTiles.Length-1)], pos);
            }
            else if(roll < percentAbnormal + percentTraps)
            {
                PaintSingleTile(map, floorTrapTile, pos);
            }
            else
            {
                PaintSingleTile(map, tile, pos);
            }
        }
    }

    private void PaintSingleTile(Tilemap map, TileBase tile, Vector2Int pos)
    {
        var tilePos = map.WorldToCell((Vector3Int)pos);
        map.SetTile(tilePos, tile);
    }

    public void Clear()
    {
        floorMap.ClearAllTiles();
        wallMap.ClearAllTiles();

    }

    internal void PaintSingleBasicWall(Vector2Int position, string binaryType)
    {
        int typeAsInt = Convert.ToInt32(binaryType, 2);
        TileBase tile = null;
        if (WallTypesHelper.wallTop.Contains(typeAsInt))
        {
            tile = wallTop;
        }
        else if (WallTypesHelper.wallSideRight.Contains(typeAsInt))
        {
            tile = wallSideRight;
        }
        else if (WallTypesHelper.wallSideLeft.Contains(typeAsInt))
        {
            tile = wallSiderLeft;
        }
        else if (WallTypesHelper.wallBottm.Contains(typeAsInt))
        {
            tile = wallBottom;
        }
        else if (WallTypesHelper.wallFull.Contains(typeAsInt))
        {
            tile = wallFull;
        }

        if (tile != null)
            PaintSingleTile(wallMap, tile, position);
        if(tile == wallBottom)
            potentialWallBottoms.Add(new Vector2Int(position.x, position.y-1));
    }

    internal void PaintSingleCornerWall(Vector2Int position, string binaryType)
    {
        int typeASInt = Convert.ToInt32(binaryType, 2);
        TileBase tile = null;

        if (WallTypesHelper.wallInnerCornerDownLeft.Contains(typeASInt))
        {
            tile = wallInnerCornerDownLeft;
        }
        else if (WallTypesHelper.wallInnerCornerDownRight.Contains(typeASInt))
        {
            tile = wallInnerCornerDownRight;
        }
        else if (WallTypesHelper.wallDiagonalCornerDownLeft.Contains(typeASInt))
        {
            tile = wallDiagonalCornerDownLeft;
        }
        else if (WallTypesHelper.wallDiagonalCornerDownRight.Contains(typeASInt))
        {
            tile = wallDiagonalCornerDownRight;
        }
        else if (WallTypesHelper.wallDiagonalCornerUpRight.Contains(typeASInt))
        {
            tile = wallDiagonalCornerUpRight;
        }
        else if (WallTypesHelper.wallDiagonalCornerUpLeft.Contains(typeASInt))
        {
            tile = wallDiagonalCornerUpLeft;
        }
        else if (WallTypesHelper.wallFullEightDirections.Contains(typeASInt))
        {
            tile = wallFull;
        }
        else if (WallTypesHelper.wallBottmEightDirections.Contains(typeASInt))
        {
            tile = wallBottom;
        }

        if (tile != null)
            PaintSingleTile(wallMap, tile, position);
        if (tile == wallDiagonalCornerDownLeft || wallDiagonalCornerDownRight)
            potentialWallBottoms.Add(new Vector2Int(position.x, position.y - 1));
    }

    public void GenerateWallBottoms()
    {
        foreach(Vector2Int pos in potentialWallBottoms)
        {
            if (!wallMap.HasTile((Vector3Int)pos) && !floorMap.HasTile((Vector3Int)pos))
            {
                PaintSingleTile(wallMap, wallBottomHalf, pos);
            }
        }
    }
}
