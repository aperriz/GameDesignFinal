using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStartupScript : MonoBehaviour
{
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        player.transform.position = new Vector3(-29, -43,-10);
    }
}
