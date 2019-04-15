using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class voicelinesTrigger : MonoBehaviour
{
    public int thisClip;
    voicelinesAudio voicelinesAudio;
    characterController cC;

    // Start is called before the first frame update
    void Start()
    {
        voicelinesAudio = GameObject.Find("VLManager").GetComponent<voicelinesAudio>();
        cC = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<characterController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            cC.scrollCount++;
            voicelinesAudio.playClip(thisClip);
            this.gameObject.SetActive(false);
        }
    }
}
