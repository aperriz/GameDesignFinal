using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVars:MonoBehaviour
{
    public AudioVars instance;
    public float masterVol = -1, musicVol = -1, sfxVol = -1;
    public bool def = true;
    private void Start()
    {
        DontDestroyOnLoad(this);
        DontDestroyOnLoad(GameObject.Find("Brightness"));
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
