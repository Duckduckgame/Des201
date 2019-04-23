using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class endlevel : MonoBehaviour
{
    public GameObject canvas;

    pauseScreenUI PSUI;

    // Start is called before the first frame update
    void Start()
    {
        PSUI = canvas.GetComponent<pauseScreenUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        PSUI.isEnd = true;
    }
}
