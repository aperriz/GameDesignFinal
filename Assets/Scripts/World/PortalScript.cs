using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneTemplate;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalScript : MonoBehaviour
{
    bool triggered = false;
    [SerializeField]
    SceneTemplateAsset levelTemplate = null;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player" && !triggered)
        {
            triggered = true;
            Destroy(GetComponent<Collider2D>());
            MainMenu.loadingScreen.GetComponent<Canvas>().enabled = true;

            if (GameObject.Find("RoomFirstDungeonGenerator") != null)
            {
                if (GameManager.level < GameManager.maxLevel)
                {
                    GameManager.level++;
                    SceneManager.LoadScene("Level 1");
                }
                else if (GameManager.level == GameManager.maxLevel)
                {
                    SceneManager.LoadScene("Boss Phase 1");
                }
            }
            else
            {
                MainMenu.loadingScreen.GetComponent<Canvas>().enabled=false;
                SceneManager.LoadScene("Boss Phase 2");
            }

        }
    }

}
