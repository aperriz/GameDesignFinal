using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour
{
    MusicManager musicManager;
    [SerializeField]
    GameObject scoreObject;

    private void Start()
    {

        GameObject player = GameObject.Find("Player");
        scoreObject.GetComponent<Text>().text = "Score: " + player.GetComponent<PlayerExtraStats>().gold.ToString(); ;

        musicManager = GameObject.Find("Music").GetComponent<MusicManager>();
        if (player != null)
        {
            player.SetActive(false);
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
