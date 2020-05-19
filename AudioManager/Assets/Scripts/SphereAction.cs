using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SphereAction : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AudioManager.Instance.Play(audioclip, gameObject, AudioManager.sfx);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            AudioManager.Instance.Stop(audioclip);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            AudioManager.Instance.PlayRandomizePitch(audioclip, gameObject);
        }
    }

    public AudioItem audioclip;

    
}
