using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pauseScreenUI : MonoBehaviour
{

    public Button continuu;
    public Button options;
    public Button extras;
    public Button quit;

    public GameObject pauseMenu;

    public bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        continuu.onClick.AddListener(continuuClick);
        options.onClick.AddListener(optionsClick);
        extras.onClick.AddListener(extrasClick);
        quit.onClick.AddListener(quitClick);

        RectTransform me = this.gameObject.GetComponent<RectTransform>();
       
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (!isPaused)
                Pause();
            else if (isPaused)
            {

                Resume();
            }

        }   
    }

    void Pause() {

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    void Resume() {

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    void continuuClick() {
        Resume();
    }
    void optionsClick() { }
    void extrasClick() { }
    void quitClick()
    {
        Application.Quit();
    }

   
}
