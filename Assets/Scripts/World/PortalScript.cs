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
            GameObject loadingScreen = GameObject.Find("Loading Screen");
            for(int i = 0; i <  loadingScreen.transform.childCount; i++)
            {
                loadingScreen.transform.GetChild(i).gameObject.SetActive(true);
            }

            if(GameObject.Find("RoomFirstDungeonGenerator").GetComponent<RoomFirstDungeonGenerator>().level <= 8)
            {
                SceneManager.LoadScene("Level " + (GameObject.Find("RoomFirstDungeonGenerator").GetComponent<RoomFirstDungeonGenerator>().level + 1));
            }
            else if (GameObject.Find("RoomFirstDungeonGenerator").GetComponent<RoomFirstDungeonGenerator>().level == 9)
            {
                SceneManager.LoadScene("Boss Phase 1");
            }
            else
            {
                SceneManager.LoadScene("Boss Phase 2");
            }
            
        }
    }

}
