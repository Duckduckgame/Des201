using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerSounds : MonoBehaviour
{
    [SerializeField]
    public AudioClip[] clips;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Step()
    {
        AudioClip clip = GetRandomClip();
        audioSource.PlayOneShot(clip);
    }

    private void Dash()
    {
        AudioClip clip = clips[4];
        audioSource.PlayOneShot(clip);
    }

    private void JumpDash()
    {
        AudioClip clip = clips[4];
        audioSource.PlayOneShot(clip);
    }

    private void JumpEnd()
    {
        AudioClip clip = clips[5];
        audioSource.PlayOneShot(clip);
    }

    private void JumpStart()
    {
        AudioClip clip = clips[6];
        audioSource.PlayOneShot(clip);
    }

    private AudioClip GetRandomClip()
    {
        return clips[UnityEngine.Random.Range(0, 3)];
    }
}