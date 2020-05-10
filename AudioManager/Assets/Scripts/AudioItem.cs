using System;
using UnityEngine;


[System.Serializable]
public class AudioItem
{
    public string name;
    public AudioClip myClip;
    [Range (0.0f,1.0f)]
    public float volume;
    [HideInInspector]
    public AudioSource myAudioSource;
    public bool isLooping;
    public bool playOnAwake;

}
