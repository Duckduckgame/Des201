using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class crappyExample : MonoBehaviour
{
    public Light targetLight;

    public GameObject PPGO;

    public PostProcessProfile dark;
    public PostProcessProfile day;

    PostProcessVolume PPV;

    bool isday = true;

    // Start is called before the first frame update
    void Start()
    {
        PPV = PPGO.GetComponent<PostProcessVolume>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl)) {

            if (isday)
            {
                PPV.profile = dark;
                targetLight.intensity = 6;
                isday = false;
            }
            else if (!isday) {
                PPV.profile = day;
                targetLight.intensity = 0;
                isday = true;
            }
            
        }
    }
}
