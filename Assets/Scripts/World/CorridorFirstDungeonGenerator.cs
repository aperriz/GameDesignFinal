using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorridorFirstDungeonGenerator : SimpleRandomWalkGenerator
{
    [SerializeField]
    private int corLength = 14, corCount = 5;
    [SerializeField]
    [Range(0.1f, 1)]
    private float roomPercent;
    [SerializeField]
    public SimpleRandomWalkSO roomGenerationParameters;

    protected override void RunProceduralGeneration()
    {
        CorridorFirstGeneratiion();
    }

    private void CorridorFirstGeneratiion()
    {
        HashSet<Vector2Int> floorPos = new HashSet<Vector2Int>();

        CreateCorridors(floorPos);

        tileMapVisualizer.PaintFloorTiles(floorPos);
        WallGenerator.CreateWalls(floorPos, tileMapVisualizer);
    }

    private void CreateCorridors(HashSet<Vector2Int> floorPos)
    {
        var curPos = startPos;
        for (int i = 0; i < corCount; i++)
        {
            var cor = ProceduralGenerationAlgorithms.RandomWalkCorridor(curPos, corLength);
            curPos = cor[cor.Count - 1];
            floorPos.UnionWith(cor);
        }
    }
}
