using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicPlayerScript : MonoBehaviour
{

    public Slider volumeSlider;
    public GameObject ObjectMusic;

    private float musicVolume = 100f;
    public AudioSource AudioSource;
    void Start()
    {
        ObjectMusic = GameObject.FindWithTag("GameMusic");
        AudioSource = ObjectMusic.GetComponent<AudioSource>();
        
        musicVolume = PlayerPrefs.GetFloat("volume");
        AudioSource.volume = musicVolume;
        volumeSlider.value = musicVolume;
    }

    // Update is called once per frame
    void Update()
    {
        AudioSource.volume = musicVolume;
        PlayerPrefs.SetFloat("volume", musicVolume);
    }

    public void updateVolume(float volume)
    {
        musicVolume = volume;
    }
}
