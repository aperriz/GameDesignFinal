using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    // Start is called before the first frame update

    private AudioSource musicSource;
    public AudioClip[] songs;
    float trackTimer;
    public AudioClip endGame;
    int trackNum = 0;
    [SerializeField, Range(0f, 1f)]
    float volume = 0.75f;

    void Start()
    {
        musicSource = GetComponent<AudioSource>();
        musicSource.volume = 0.75f;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(musicSource.isPlaying)
        {
            trackTimer += 1 * Time.deltaTime;
            musicSource.volume = volume;
        }

        if (!musicSource.isPlaying || trackTimer >= musicSource.clip.length)
        {
            ChangeSong(trackNum);
            trackNum++;
            if(trackNum == songs.Length)
            {
                trackNum = 0;
            }
        }

    }

    public void ChangeSong(int selection)
    {
        musicSource.clip = songs[selection];
        musicSource.Play();
    }

    public void CreditMusic()
    {
        musicSource.clip = endGame;
    }
}
