using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {

            RoomFirstDungeonGenerator dungeonGenerator = GameObject.Find("RoomFirstDungeonGenerator").GetComponent<RoomFirstDungeonGenerator>();
            if(dungeonGenerator.level <= 8)
            {
                SceneManager.LoadScene("Level " + (dungeonGenerator.level + 1));
            }
            else
            {
                SceneManager.LoadScene("Boss Phase 1");
            }
            GameObject.Find("Loading Screen").SetActive(true);
        }
    }

}
