using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClipManager : MonoBehaviour
{

    //Singleton instance
    private static ClipManager _instance;
    public static ClipManager Instance
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

        
}
