using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject settingsPannel;
    
    public void StartGame()
    {
        SceneManager.LoadScene("Level 01");
    }

    public void ExitGame()
    {
        Debug.Log("Quitting game");
        Application.Quit();
    }

    public void OpenSettings()
    {
        settingsPannel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPannel?.SetActive(false);
    }

}
