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
    float masterVal, sfxVal, musicVal, brightnessVal;

    private void Start()
    {
        brightnessFilter = GameObject.Find("Brightness").GetComponent<Image>();
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(brightnessFilter.gameObject);
        mixer.GetFloat(master.name, out masterVal);
        mixer.GetFloat (sfx.name, out sfxVal);
        mixer.GetFloat(music.name, out musicVal);

        brightnessVal = brightnessFilter.color.a;

        masterSlider.value = masterVal;
        sfxSlider.value = sfxVal;
        musicSlider.value = musicVal;
        brightnessSlider.value = brightnessVal;
    }

    public void AdjustMaster(float newVal)
    {
        mixer.SetFloat(master.name, newVal);
    }

    public void AdjustSFX(float newVal)
    {
        mixer.SetFloat(sfx.name, newVal);
    }

    public void AdjustMusic(float newVal)
    {
        mixer.SetFloat(music.name, newVal);
    }

    public void AdjustBrightness(float newVal)
    {
        var tempColor = brightnessFilter.color;
        tempColor.a = .8f - (newVal*.8f);
        brightnessFilter.color = tempColor;
    }

    public void ToggleSettings()
    {
        showing = !showing;
        gameObject.SetActive(showing);
        if(pauseMenu != null ) pauseMenu.SetActive(!showing);
        Debug.Log("Settings " + showing);
    }
}
