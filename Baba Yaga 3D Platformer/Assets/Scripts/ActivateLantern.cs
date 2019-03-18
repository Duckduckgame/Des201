using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateLantern : MonoBehaviour
{
    Collider lanternCollider;
    GameObject[] platforms;
    Renderer platformRenderer;
    public Collider platformCollider;
    AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = AudioManager.instance;
        lanternCollider = GetComponent<BoxCollider>();
        platforms = GameObject.FindGameObjectsWithTag("Hidden Platform");
        foreach(GameObject platform in platforms)
        {
            platformRenderer = platform.transform.gameObject.GetComponent<Renderer>();
            platformCollider = platform.transform.gameObject.GetComponent<Collider>();
            platformRenderer.enabled = false;
            platformCollider.enabled = false;
        }
    
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L) || Input.GetButtonDown("Fire3"))
        {
            
            //Toggle the lantern on and off when pressing the L key
            lanternCollider.enabled = !lanternCollider.enabled;

            if (lanternCollider.enabled)
            {
                audioManager.PlaySound("TorchSound");
                transform.GetChild(0).gameObject.GetComponent<Light>().enabled = true;
            }
            else if (!lanternCollider.enabled)
            {
                transform.GetChild(0).gameObject.GetComponent<Light>().enabled = false;
            }
        }

        //If lantern is off, deactivate platform
        if (!transform.GetChild(0).gameObject.GetComponent<Light>().enabled)
        {
            foreach (GameObject platform in platforms)
            {
                platformRenderer = platform.transform.gameObject.GetComponent<Renderer>();
                platformCollider = platform.transform.gameObject.GetComponent<Collider>();
                platformRenderer.enabled = false;
                platformCollider.enabled = false;
            }       
        }

    }

    //If lantern's collider collides with platform's collider, activate platform
    private void OnTriggerEnter(Collider other)
    {

       foreach (GameObject platform in platforms)
       {
             if (other.tag == "Hidden Platform")
             {
                 platformRenderer = platform.transform.gameObject.GetComponent<Renderer>();
                 platformCollider = platform.transform.gameObject.GetComponent<Collider>();
                 platformRenderer.enabled = true;
                 platformCollider.enabled = true;
             }
                    
       }
        
    }
}
