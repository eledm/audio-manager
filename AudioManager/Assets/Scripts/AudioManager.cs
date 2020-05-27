using UnityEditorInternal;
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


        //assign the audio item settings to its audio source
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

        //initialization of the AudioMixerGroups
        mixer = Resources.Load("AudioMixer") as AudioMixer;

        sfx = mixer.FindMatchingGroups("Sfx")[0];
        music = mixer.FindMatchingGroups("Music")[0];
        ambience = mixer.FindMatchingGroups("Ambience")[0];
        dialogue = mixer.FindMatchingGroups("Ambience")[0];

    }

    //array of audioitems to play
    public AudioItem[] audioItems;
    
    //max and min pitch change levels
    public static float maxPitch = 1.05f;
    public static float minPitch = 0.95f;

    //reference to the Audio Mixer groups to use as outputs for the Audio Sources
    AudioMixer mixer;
    public static AudioMixerGroup sfx;
    public static AudioMixerGroup music;
    public static AudioMixerGroup ambience;
    public static AudioMixerGroup dialogue;


    #region METHODS

    //Play method: takes an AudioItem and the AudioMixerGroup you want to play it through

    public void Play(AudioItem audioItem, GameObject gameObject, AudioMixerGroup audioMixerGroup) //AudioMixerGroup bus)
    {
        audioItem.myAudioSource = gameObject.AddComponent<AudioSource>();
        audioItem.myAudioSource.outputAudioMixerGroup = audioMixerGroup;
        audioItem.myAudioSource.volume = audioItem.volume;
        audioItem.myAudioSource.loop = audioItem.isLooping;
        audioItem.myAudioSource.playOnAwake = audioItem.playOnAwake;
        audioItem.myAudioSource.clip = audioItem.myClip;
        //audioItem.myAudioSource.outputAudioMixerGroup = bus;
        audioItem.myAudioSource.Play();
    }

    public void PlaySfx(AudioItem audioItem, GameObject gameObject)
    {
        audioItem.myAudioSource = gameObject.AddComponent<AudioSource>();
        audioItem.myAudioSource.outputAudioMixerGroup = sfx;
        audioItem.myAudioSource.volume = audioItem.volume;
        audioItem.myAudioSource.loop = audioItem.isLooping;
        audioItem.myAudioSource.playOnAwake = audioItem.isLooping;
        audioItem.myAudioSource.pitch = UnityEngine.Random.Range(minPitch, maxPitch);
        audioItem.myAudioSource.Play();
    }

    //stops playing the selected AudioItem
    public void Stop(AudioItem audioItem)
    {
        if (audioItem.myAudioSource.isPlaying == true)
            audioItem.myAudioSource.Stop();
        else
            Debug.Log("AudioItem " + audioItem.name + " is already stopped.");
    }

    //pauses the selected audio item
    public void Pause(AudioItem audioItem)
    {
        if (audioItem.myAudioSource.isPlaying == true)
            audioItem.myAudioSource.Pause();
        else
            Debug.Log("AudioItem " + audioItem.name + " is already not playing.");
    }   

    //unpauses the selected audio item
    public void Unpause(AudioItem audioItem)
    {
        if (audioItem.myAudioSource.isPlaying == false)
            audioItem.myAudioSource.UnPause();
        else
            Debug.Log("AudioItem " + audioItem.name + "is already playing.");
    }

    //loop through all the the audio sources and stop
    public void StopAll()
    {
        var audioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audio in audioSources)
        {
            audio.Stop();
        }
    }

    //loop through all audio sources and pause
    public void PauseAll()
    {
        var audioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audio in audioSources)
        {
            audio.Pause();
        }
    }

    //unpauses all audiosources
    public void UnpauseAll()
    {
        var audioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audio in audioSources)
        {
            audio.UnPause();
        }
    }


    

    #endregion
}
