using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // cache
    private AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        //caching
        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("No AM in scene!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
