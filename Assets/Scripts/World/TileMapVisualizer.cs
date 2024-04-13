using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapVisualizer : MonoBehaviour
{
    [SerializeField]
    private Tilemap floorMap, wallMap;
    [SerializeField]
    private TileBase floorTile, wallTop;

    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions)
    {

        PaintTiles(floorPositions, floorMap, floorTile);
    }

    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap map, TileBase tile)
    {
        foreach(var pos in positions)
        {
            PaintSingleTile(map, tile, pos);
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

    internal void PaintSingleBasicWall(Vector2Int pos)
    {
        var tilePos = wallMap.WorldToCell((Vector3Int)pos);
        wallMap.SetTile(tilePos, wallTop);
    }
}
