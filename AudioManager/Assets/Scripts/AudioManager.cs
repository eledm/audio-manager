using System.Collections;
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
                Play(sound, gameObject, music);
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

    [Header("Sfx Pitch")]
    public float maxPitch = 1.05f;
    public float minPitch = 0.95f;

    //reference to the Audio Mixer groups to use as outputs for the Audio Sources
    AudioMixer mixer;
    public static AudioMixerGroup sfx;
    public static AudioMixerGroup music;
    public static AudioMixerGroup ambience;
    public static AudioMixerGroup dialogue;


    #region METHODS

    //Play method: takes an AudioItem and the AudioMixerGroup you want to play it through

    public void Play(AudioItem audioItem, GameObject gameObject, AudioMixerGroup audioMixerGroup)
    {
        if (gameObject.GetComponent<AudioSource>() == null)
        {
            audioItem.myAudioSource = gameObject.AddComponent<AudioSource>();
        }
        else
        {
            audioItem.myAudioSource = gameObject.GetComponent<AudioSource>();
        }
        audioItem.myAudioSource.outputAudioMixerGroup = audioMixerGroup;
        audioItem.myAudioSource.volume = audioItem.volume;
        audioItem.myAudioSource.loop = audioItem.isLooping;
        audioItem.myAudioSource.playOnAwake = audioItem.playOnAwake;
        audioItem.myAudioSource.clip = audioItem.myClip;
        audioItem.myAudioSource.Play();
    }

    public void PlaySfx(AudioItem audioItem, GameObject gameObject)
    {
        if (gameObject.GetComponent<AudioSource>() == null)
        {
            audioItem.myAudioSource = gameObject.AddComponent<AudioSource>();
        }
        else
        {
            audioItem.myAudioSource = gameObject.GetComponent<AudioSource>();
        }

        audioItem.myAudioSource.clip = audioItem.myClip;
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

    public void PlayFadeIn(AudioItem audioItem, GameObject gameObject, AudioMixerGroup audioMixerGroup)
    {
        StartCoroutine("FadeIn");
    }

    public void PlayOneShot(AudioItem audioItem, GameObject gameObject, AudioMixerGroup audioMixerGroup)
    {

    }



    //COROUTINES

    private AudioSource myAudioSource;
    private float _minVolume = 0f;
    private float _maxVolume = 1f;
    private float speed = 1f;

    public IEnumerator FadeIn(AudioItem audioItem, GameObject gameObject, AudioMixerGroup audioMixerGroup)
    {

        if (gameObject.GetComponent<AudioSource>() == null || gameObject.GetComponent<AudioSource>().isPlaying)
        {
            audioItem.myAudioSource = gameObject.AddComponent<AudioSource>();
        }
        audioItem.myAudioSource.outputAudioMixerGroup = audioMixerGroup;

        audioItem.myAudioSource.loop = audioItem.isLooping;
        audioItem.myAudioSource.playOnAwake = audioItem.playOnAwake;
        audioItem.myAudioSource.clip = audioItem.myClip;



        myAudioSource.volume = _minVolume;
        float audioVolume = myAudioSource.volume;
        myAudioSource.Play();

        while (myAudioSource.volume < _maxVolume)
        {
            audioVolume += speed;
            myAudioSource.volume = audioVolume;
            yield return new WaitForSeconds(0.1F);
        }

    }

}


#endregion
