using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    private void ReturnToMainMenu()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
        SceneManager.LoadScene("MainMenu");
    }
}