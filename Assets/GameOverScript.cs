using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    MusicManager musicManager;

    private void Start()
    {
        musicManager = GameObject.Find("Music").GetComponent<MusicManager>();
        if (GameObject.Find("Player") != null)
        {
            Destroy(GameObject.Find("Player"));
        }

        if(musicManager != null)
        {
            musicManager.GetComponent<AudioSource>().clip = musicManager.endGame;
            musicManager.GetComponent<AudioSource>().loop = true;
        }
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
