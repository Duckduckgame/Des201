using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    AudioManager audioManager;
    characterController cC;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = AudioManager.instance;

        cC = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<characterController>();
    }


    // Update is called once per frame
    void Update()
    {
        //this.transform.position += new Vector3(0, Mathf.Sin(Time.time) / 200, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            cC.lostSoulsCount++;
            audioManager.PlaySound("lostSoulNoise");
            gameObject.SetActive(false);
        }
    }
}
