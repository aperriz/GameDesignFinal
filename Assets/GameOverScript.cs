using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour
{
    GameObject musicManager;
    [SerializeField]
    TextMeshProUGUI scoreObject;

    private void Start()
    {
        
        GameObject player = GameObject.Find("Player");
        Debug.Log(player);

        Destroy(player);

        musicManager = GameObject.Find("Music");
        if (player != null)
        {
            scoreObject.text = "Score: " + player.GetComponent<PlayerExtraStats>().gold.ToString();
            Destroy(player);
        }

        if(musicManager != null)
        {
            musicManager.GetComponent<AudioSource>().clip = musicManager.GetComponent<MusicManager>().endGame;
            musicManager.GetComponent<AudioSource>().loop = true;
        }
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
