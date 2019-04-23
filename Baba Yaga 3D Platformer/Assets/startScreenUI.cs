using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class startScreenUI : MonoBehaviour
{
    public Button start;

    public Button quit;
    public Text music;
    public Button musicBtn;
    UIFade uifade;
    public GameObject uifadeGO;
    // Start is called before the first frame update
    void Start()
    {
        start.onClick.AddListener(startClick);

        quit.onClick.AddListener(quitClick);

        uifade = uifadeGO.GetComponent<UIFade>();
    }

    // Update is called once per frame
    void Update()
    {

        if (uifadeGO.GetComponent<CanvasGroup>().alpha == 1)
        {
            SceneManager.LoadScene("lvlDesTests", LoadSceneMode.Single);
        }


    }

    void startClick() {
        //uifade.fadeToBlack();
        uifade.fadeTo = true;
       
    }

    void quitClick() {

        Application.Quit();
    }
    
}
