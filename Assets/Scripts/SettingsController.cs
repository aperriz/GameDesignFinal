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

        if (av.instance.def)
        {
            av.instance.masterVol = 1;
            av.instance.musicVol = 1;
            av.instance.sfxVol = 1;
            av.instance.def = false;
        }

        brightnessFilter = GameObject.Find("Brightness").GetComponent<Image>();
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(brightnessFilter.gameObject);
        DontDestroyOnLoad(GameObject.Find("Loading Screen"));

        brightnessVal = 1 - brightnessFilter.color.a;
        masterSlider.value = av.instance.masterVol;
        sfxSlider.value = av.instance.sfxVol;
        musicSlider.value = av.instance.musicVol;
        brightnessSlider.value = brightnessVal;
    }

    public void AdjustMaster(float newVal)
    {
        if (!av.instance.def)
        {
            mixer.SetFloat(master.name, (1 - newVal) * -20);
            av.instance.masterVol = newVal;
        }

        if (newVal == 0)
        {
            mixer.SetFloat(master.name, -80);
        }
    }

    public void AdjustSFX(float newVal)
    {
        if (!av.instance.def)
        {
            mixer.SetFloat(sfx.name, (1 - newVal) * -20);
            av.instance.sfxVol = newVal;
        }
        if (newVal == 0)
        {
            mixer.SetFloat(sfx.name, -80);
        }
    }

    public void AdjustMusic(float newVal)
    {
        if (!av.instance.def)
        {
            mixer.SetFloat(music.name, (1 - newVal) * -20);
            av.instance.musicVol = newVal;
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
