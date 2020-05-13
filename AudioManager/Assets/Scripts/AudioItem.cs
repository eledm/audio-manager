using System;
using UnityEngine;


[System.Serializable]
public class AudioItem
{
    public string name;
    public AudioClip myClip;
    [Range (0.0f,1.0f)]
    public float volume = 1f;
    [HideInInspector]
    public AudioSource myAudioSource;
    public bool isLooping = false;
    public bool playOnAwake = false;

    //AudioItem constructor
    public AudioItem(string name, AudioClip audioClip, AudioSource audioSource)
    {
        this.name = name;
        this.myClip = audioClip;
        this.myAudioSource = audioSource;
    }
}
