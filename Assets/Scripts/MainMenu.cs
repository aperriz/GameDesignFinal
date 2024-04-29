using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject settingsObject;
    [SerializeField]
    public GameObject playerPrefab;
    private static GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
        if(player != null)
        {
            Destroy(player);
        }   
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level 1");
        Time.timeScale = 1;
        if(player != null)
        {
            player.SetActive(true);
        }
        else
        {
            Instantiate(playerPrefab);
        }
    }

    public void ExitGame()
    {
        Debug.Log("Quitting game");
        Application.Quit();
    }


}
