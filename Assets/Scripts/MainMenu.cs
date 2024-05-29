using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject settingsObject;
    [SerializeField]
    public GameObject playerPrefab, musicPrefab, loadingPrefab, brightnessPrefab;
    private static GameObject player;
    public static GameObject music, brightness, loadingScreen;

    private void Start()
    {
        GameManager.level = 1;
        music = GameObject.Find("Music");
        brightness = GameObject.Find("Brightness");
        loadingScreen = GameObject.Find("Loading Screen");

        if (music == null)
        {
            (music = Instantiate(musicPrefab)).name = "Music";
            DontDestroyOnLoad(music);
        }
        if (loadingScreen == null)
        {
            (loadingScreen = Instantiate(loadingPrefab)).name = "Loading Screen";
            DontDestroyOnLoad(loadingScreen);
        }
        if (brightness == null)
        {
            (brightness = Instantiate(brightnessPrefab)).name = "Brightness";
            DontDestroyOnLoad(brightness);
        }

        
        player = GameObject.Find("Player");
        if(player != null)
        {
            Destroy(player);
        }   
    }

    public void StartGame()
    {
        

        if (player != null)
        {
            player.SetActive(true);
        }
        else
        {
            Instantiate(playerPrefab).name = "Player";
        }

        loadingScreen.GetComponent<Canvas>().enabled = true;

        SceneManager.LoadScene("Level 1");
    }

    public void ExitGame()
    {
        Debug.Log("Quitting game");
        Application.Quit();
    }


}
