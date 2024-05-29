using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    [SerializeField]
    AudioMixerGroup master, sfx, music;
    [SerializeField]
    AudioMixer mixer;
    Image brightnessFilter;
    public bool showing = false;
    [SerializeField]
    GameObject pauseMenu;
    [SerializeField]
    Scrollbar masterSlider, sfxSlider, musicSlider, brightnessSlider;
    float brightnessVal;
    AudioVars av;

    private void Start()
    {
        av = GameObject.Find("AudioVars").GetComponent<AudioVars>();
        DontDestroyOnLoad(av.gameObject);

        if (AudioVars.def)
        {
           AudioVars.masterVol = 1;
           AudioVars.musicVol = 1;
           AudioVars.sfxVol = 1;
           AudioVars.def = false;
        }

        brightnessFilter = GameObject.Find("Brightness").GetComponent<Image>();
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(brightnessFilter.gameObject);
        DontDestroyOnLoad(GameObject.Find("Loading Screen"));

        brightnessVal = 1 - brightnessFilter.color.a;
        masterSlider.value =AudioVars.masterVol;
        sfxSlider.value =AudioVars.sfxVol;
        musicSlider.value =AudioVars.musicVol;
        brightnessSlider.value = brightnessVal;
    }

    public void AdjustMaster(float newVal)
    {
        if (!AudioVars.def)
        {
            mixer.SetFloat(master.name, (1 - newVal) * -20);
           AudioVars.masterVol = newVal;
        }

        if (newVal == 0)
        {
            mixer.SetFloat(master.name, -80);
        }
    }

    public void AdjustSFX(float newVal)
    {
        if (!AudioVars.def)
        {
            mixer.SetFloat(sfx.name, (1 - newVal) * -20);
           AudioVars.sfxVol = newVal;
        }
        if (newVal == 0)
        {
            mixer.SetFloat(sfx.name, -80);
        }
    }

    public void AdjustMusic(float newVal)
    {
        if (!AudioVars.def)
        {
            mixer.SetFloat(music.name, (1 - newVal) * -20);
           AudioVars.musicVol = newVal;
        }
        if (newVal == 0)
        {
            mixer.SetFloat(music.name, -80);
        }
    }

    public void AdjustBrightness(float newVal)
    {
        var tempColor = brightnessFilter.color;
        tempColor.a = .8f - (newVal * .8f);
        brightnessFilter.color = tempColor;
    }

    public void ToggleSettings()
    {
        showing = !showing;
        gameObject.SetActive(showing);
        if (pauseMenu != null) pauseMenu.SetActive(!showing);
        Debug.Log("Settings " + showing);
    }
}
