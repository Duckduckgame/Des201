using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private PlayerPositon playerPositon;
    public bool m_OnCheckpoint { get { return onCheckPoint; } set { onCheckPoint = value; } }
    private bool onCheckPoint;

    public GameObject fireLight;
    public lightFlicker LF;
    public GameObject fire;
    AudioManager audioManager;
    AudioSource audioSource;
    public bool startCheckpoint;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = AudioManager.instance;
        audioSource = GetComponent<AudioSource>();
        playerPositon = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPositon>();
        fireLight.GetComponent<Light>().intensity = 0;
        LF = fireLight.GetComponent<lightFlicker>();
        fire.GetComponent<Renderer>().enabled = false;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            onCheckPoint = true;
            playerPositon.m_reachedPosition = transform.position;
            //audioManager.PlaySound("Checkpoint");
            if(!startCheckpoint)
            audioSource.Play();

            LF.isOn = true;
            fire.GetComponent<Renderer>().enabled = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            onCheckPoint = false;
        }
    }
    
}
 