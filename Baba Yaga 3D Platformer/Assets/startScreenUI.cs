using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class startScreenUI : MonoBehaviour
{
    public Button start;
    public Button options;
    public Button quit;
    public Text music;
    public Button musicBtn;
    // Start is called before the first frame update
    void Start()
    {
        start.onClick.AddListener(startClick);
        options.onClick.AddListener(optionsClick);
        quit.onClick.AddListener(quitClick);
    }

    // Update is called once per frame
    void Update()
    {
        
       

        
    }

    void startClick() {
        Debug.Log("starts been clicked");
        SceneManager.LoadScene("lvlDesTests", LoadSceneMode.Single);
    }
    void optionsClick() {
        Debug.Log("options been clicked");
    }
    void quitClick() {
        Debug.Log("quit been clicked");
        Application.Quit();
    }
    
}
