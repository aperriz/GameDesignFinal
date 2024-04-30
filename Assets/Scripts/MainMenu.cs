using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject settingsObject;
    [SerializeField]
    public GameObject playerPrefab, loadingScreen;
    private static GameObject player;

    private void Start()
    {
        DontDestroyOnLoad(loadingScreen);
        player = GameObject.Find("Player");
        if(player != null)
        {
            Destroy(player);
        }   
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level 1");
        loadingScreen.SetActive(true);
        if(player != null)
        {
            player.SetActive(true);
        }
        else
        {
            Instantiate(playerPrefab).name = "Player";
        }
    }

    public void ExitGame()
    {
        Debug.Log("Quitting game");
        Application.Quit();
    }


}
