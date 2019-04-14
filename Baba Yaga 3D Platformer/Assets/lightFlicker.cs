using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightFlicker : MonoBehaviour
{

    public bool isOn;
    Light thisLight;

    // Start is called before the first frame update
    void Start()
    {
        thisLight = gameObject.GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        float rng = Random.Range(0f, 3.5f);

        if (isOn) {
            thisLight.intensity = Mathf.Lerp(thisLight.intensity, rng, Time.deltaTime * 5);
        }
    }
}
