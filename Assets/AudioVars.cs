using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVars:MonoBehaviour
{
    
    public static float masterVol = -1, musicVol = -1, sfxVol = -1;
    public static bool def = true;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
