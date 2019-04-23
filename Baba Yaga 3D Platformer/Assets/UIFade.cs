using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFade : MonoBehaviour
{
    public CanvasGroup cg;
    float timer = 0;
    public bool fadeTo;
    // Start is called before the first frame update
    void Start()
    {
        cg = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeTo)
        {
            cg.alpha += Time.deltaTime;
        }

        }


    public void fadeToBlack() {
        timer = 0;
        cg.alpha = Mathf.Lerp(0, 1, Time.deltaTime * 20f);
    }

    public void fadefromBlack()
    {
        timer = 0;
        cg.alpha = Mathf.Lerp(1, 0, timer);
    }
}
