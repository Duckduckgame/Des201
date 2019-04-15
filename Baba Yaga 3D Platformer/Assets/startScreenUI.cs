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

        SceneManager.LoadScene("lvlDesTests", LoadSceneMode.Single);
    }
    void optionsClick() {

    }
    void quitClick() {

        Application.Quit();
    }
    
}
