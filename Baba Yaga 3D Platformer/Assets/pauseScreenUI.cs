using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pauseScreenUI : MonoBehaviour
{

    public Button continuu;
    public Text endT;
    public Button quit;
    public GameObject lostSoulsCount;
    public GameObject scrollsCount;
    public GameObject pauseMenu;

    public CanvasGroup cG;

    public bool isPaused = true;
    public bool isEnd = false;
    characterController cC;

    // Start is called before the first frame update
    void Start()
    {
        continuu.onClick.AddListener(continuuClick);

      
        quit.onClick.AddListener(quitClick);

        cC = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<characterController>();
        cG = gameObject.GetComponent<CanvasGroup>();
        
        

    }
    // Update is called once per frame
    void Update()
    {
        if (isEnd) {
            continuu.gameObject.SetActive(false);
            endT.gameObject.SetActive(true);
            Pause();
           
        }
        if (isPaused) {

            lostSoulsCount.GetComponent<Text>().text = cC.lostSoulsCount.ToString() + "/78";
            scrollsCount.GetComponent<Text>().text = cC.scrollCount.ToString() + "/15";
        }
        
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (!isPaused)
                Pause();
            else if (isPaused)
            {
                if(!isEnd)
                Resume();
            }

        }   
    }

    void Pause() {

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;


        cG.alpha = 1;
        cG.interactable = true;

        lostSoulsCount.GetComponent<Text>().text = cC.lostSoulsCount.ToString() + "/78";
        scrollsCount.GetComponent<Text>().text = cC.scrollCount.ToString() + "/15";
        Time.timeScale = 0.01f;
        isPaused = true;
    }

    void Resume() {

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        cG.alpha = 0;
        cG.interactable = false;
        Time.timeScale = 1f;
        isPaused = false;
    }

    void continuuClick() {
        Resume();
    }

    void quitClick()
    {
        Application.Quit();
    }

   
}
