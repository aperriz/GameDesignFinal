using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class ProceduralGenerationAlgorithms : MonoBehaviour
{
    public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPos, int walkLength)
    {
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();

        path.Add(startPos);
        var previousPos = startPos;

        for (int i = 0; i < walkLength; i++)
        {
            var newPos = previousPos + Direction2D.GetRandomCardinalDir();
            path.Add(newPos);
            previousPos = newPos;
        }

        return path;
    }
    public static List<Vector2Int> RandomWalkCorridor(Vector2Int startPos, int corLength)
    {
        List<Vector2Int> corridor = new List<Vector2Int>();
        var dir = Direction2D.GetRandomCardinalDir();
        var curPos = startPos;
        corridor.Add(curPos);

        for (int i = 0; i < corLength; i++)
        {
            curPos += dir;
            corridor.Add(curPos);
        }

        return corridor;
    }

    public static List<BoundsInt> BinarySpacePartitioning(BoundsInt spaceToSplit, int minWidth, int minHeight)
    {
        //https://youtu.be/nbi88hY9hcw?si=Sd3vWtNuvpYrT8lV&t=154
    }
}
public static class Direction2D
{

    public static List<Vector2Int> cardinalDirList = new List<Vector2Int> {
        new Vector2Int(0, 1), //up
        new Vector2Int(1, 0), //right
        new Vector2Int(0, -1), //down
        new Vector2Int(-1,0) //left
    };

    public static Vector2Int GetRandomCardinalDir()
    {
        return cardinalDirList[Random.Range(0, cardinalDirList.Count)];
    }

}