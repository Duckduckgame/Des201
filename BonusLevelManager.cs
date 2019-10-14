using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BonusLevelManager : MonoBehaviour
{
    [SerializeField]
    float levelTimer;

    public Text timerText;
    // Start is called before the first frame update
    private void Start()
    {
        levelTimer = 60.0f;
    }
    // Update is called once per frame
    void Update()
    {
        levelTimer -= Time.deltaTime;
        timerText.text = (levelTimer).ToString("0");
        if(levelTimer < 0)
        {
            TimerEnded();
          
        }
    }
    void TimerEnded()
    {
        SceneManager.LoadScene("lvlDesTests", LoadSceneMode.Single);
    }
}
