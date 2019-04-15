using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class voicelinesAudio : MonoBehaviour
{
    public AudioClip[] voicelines;

    AudioSource audioSource;

    public int clipToPlay;

    // Start is called before the first frame update
    void Start()
    {
       audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void playClip(int clipToPlay)
    {

        audioSource.Stop();
        audioSource.PlayOneShot(voicelines[clipToPlay]);
    }
}
