using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class ActivateLantern : MonoBehaviour
{
    public Collider m_lanternCollider { get { return lanternCollider; } set { lanternCollider = value; } }
    Collider lanternCollider;
    GameObject[] platforms;
    Renderer platformRenderer;
    AudioManager audioManager;
    public Collider m_platformCollider { get { return platformCollider; } set { platformCollider = value; } }
    private Collider platformCollider;
    private PlayerPositon playerPositon;

    public GameObject PPGO;

    public PostProcessProfile dark;
    public PostProcessProfile day;

    PostProcessVolume PPV;

    bool isday = true;


    // Start is called before the first frame update
    void Start()
    {
        audioManager = AudioManager.instance;
        lanternCollider = GetComponent<BoxCollider>();
        platforms = GameObject.FindGameObjectsWithTag("Hidden Platform");
        playerPositon = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPositon>();

        foreach (GameObject platform in platforms)
        {
            platformRenderer = platform.transform.GetChild(0).GetComponent<Renderer>();
            platformCollider = platform.transform.GetChild(0).GetComponent<Collider>();
            platformRenderer.enabled = false;
            platformCollider.enabled = false;
        }

        PPV = PPGO.GetComponent<PostProcessVolume>();

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
                //audioManager.PlaySound("TorchSound");
                PPV.profile = dark;
                transform.GetChild(0).gameObject.GetComponent<Light>().enabled = true;
                transform.GetChild(0).gameObject.GetComponent<Light>().intensity = 6;
            }
            else if (!lanternCollider.enabled)
            {
                PPV.profile = day;
                transform.GetChild(0).gameObject.GetComponent<Light>().enabled = false;
            }
        }

        //If lantern is off, deactivate platform
        if (!transform.GetChild(0).gameObject.GetComponent<Light>().enabled)
        {
            foreach (GameObject platform in platforms)
            {
                platformRenderer = platform.transform.GetChild(0).GetComponent<Renderer>();
                platformCollider = platform.transform.GetChild(0).GetComponent<Collider>();
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
                platformRenderer = platform.transform.GetChild(0).GetComponent<Renderer>();
                platformCollider = platform.transform.GetChild(0).GetComponent<Collider>();
                platformRenderer.enabled = true;
                platformCollider.enabled = true;
            }  
       }
    }
}
