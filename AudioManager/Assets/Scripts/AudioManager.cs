using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions.Must;
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


        //assign the audio item settings to its audio source
        audioItems = audioItems.Add FindObjectsOfType<AudioItem>();
        AudioItem[] array;
        
        foreach (var sound in audioItems)
        {
            sound.myAudioSource = gameObject.AddComponent<AudioSource>();
            sound.myAudioSource.clip = sound.myClip;
            sound.myAudioSource.volume = sound.volume;
            sound.myAudioSource.loop = sound.isLooping;
            if (sound.playOnAwake == true)
            {
                sound.myAudioSource.Play();
            }

        }

    }
    
    //array of audioitems to play
    public AudioItem[] audioItems;
    //reference to the AudioMixer
    AudioMixer mixer = Resources.Load("Master") as AudioMixer;
    //max and min pitch change levels
    public static float maxPitch = 1.05f;
    public static float minPitch = 0.95f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region METHODS

    //Play method: takes an AudioItem and the AudioMixerGroup you want to play it through

    public static void Play(AudioItem audioItem, AudioMixerGroup bus)
    {
        audioItem.myAudioSource.clip = audioItem.myClip;
        audioItem.myAudioSource.outputAudioMixerGroup = bus;
        audioItem.myAudioSource.Play();
    }

    //stops playing the selected AudioItem
    public static void Stop(AudioItem audioItem)
    {
        if (audioItem.myAudioSource.isPlaying == true)
            audioItem.myAudioSource.Stop();
        else
            Debug.Log("AudioItem " + audioItem.name + " is already stopped.");
    }

    //pauses the selected audio item
    public static void Pause(AudioItem audioItem)
    {
        if (audioItem.myAudioSource.isPlaying == true)
            audioItem.myAudioSource.Pause();
        else
            Debug.Log("AudioItem " + audioItem.name + " is already not playing.");
    }   

    //unpauses the selected audio item
    public static void Unpause(AudioItem audioItem)
    {
        if (audioItem.myAudioSource.isPlaying == false)
            audioItem.myAudioSource.UnPause();
        else
            Debug.Log("AudioItem " + audioItem.name + "is already playing.");
    }

    //loop through all the the audio sources and stop
    public static void StopAll()
    {
        var audioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audio in audioSources)
        {
            audio.Stop();
        }
    }

    //loop through all audio sources and pause
    public static void PauseAll()
    {
        var audioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audio in audioSources)
        {
            audio.Pause();
        }
    }

    //unpauses all audiosources
    public static void UnpauseAll()
    {
        var audioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audio in audioSources)
        {
            audio.UnPause();
        }
    }


    public static void PlayRandomizePitch(AudioItem audio, AudioMixerGroup bus)
    {
        audio.myAudioSource.outputAudioMixerGroup = bus;
        audio.myAudioSource.pitch = Random.Range(minPitch, maxPitch);
        audio.myAudioSource.Play();
    }

    #endregion
}
