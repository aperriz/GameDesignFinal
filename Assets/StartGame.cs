using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    GameObject player;
    
    public void Unpause()
    {
        player = GameObject.Find("Player");
        player.GetComponent<PlayerMovement>().paused = false;
        player.transform.GetChild(1).gameObject.SetActive(true);
    }
}
