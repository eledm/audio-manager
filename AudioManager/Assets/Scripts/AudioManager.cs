using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    //Singleton instance
    private static AudioManager _instance;
    public static AudioManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("AudioManager instance is null.");
            return _instance;
        }
    }

    private void Awake()
    {
        //initialize singleton instance
        _instance = this;

    }

    //audio sources where music will be played through
    public AudioMixer audioMixer;
    public AudioSource musicSource;
    public AudioSource sfxSource;

    //array of audioitems to play
    public AudioItem[] audioItems;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region METHODS

    //ONE PLAY METHOD, DEFAULT GOES TO MASTER, YOU CAN CHANGE THE OUTPUT TO MUSIC OR SFX)

    public void Play(AudioItem audioItem, AudioSource audioSource)
    {
        audioSource.clip = audioItem.myClip;
        audioSource.Play();
    }
   

    public void Stop()
    {
        //loop through all the the audio sources and stop
        var audioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audio in audioSources)
        {
            audio.Stop();
        }
    }

    public void Pause()
    {
        //loop through all audio sources and pause
        var audioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audio in audioSources)
        {
            audio.Pause();
        }
    }

    public void RandomizePitch()
    {

    }

    #endregion
}
