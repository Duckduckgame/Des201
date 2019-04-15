using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class voicelinesTrigger : MonoBehaviour
{
    public int thisClip;
    voicelinesAudio voicelinesAudio;

    // Start is called before the first frame update
    void Start()
    {
        voicelinesAudio = GameObject.Find("VLManager").GetComponent<voicelinesAudio>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        voicelinesAudio.playClip(thisClip);
        this.gameObject.SetActive(false);
    }
}
