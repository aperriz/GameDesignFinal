using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnStart : MonoBehaviour
{
    [SerializeField]
    bool generateOnStart = true;

    private void Start()
    {
        if (generateOnStart)
        {
            GameObject.FindObjectOfType<RoomFirstDungeonGenerator>().GenerateDungeon();
        }    
    }
}
