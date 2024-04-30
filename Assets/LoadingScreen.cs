using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    void ToggleVisibility()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
