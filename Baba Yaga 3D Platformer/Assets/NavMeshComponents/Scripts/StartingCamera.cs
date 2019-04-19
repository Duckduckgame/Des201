using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingCamera : MonoBehaviour
{
    public GameObject CineCam;
    public Transform[] views;
    public float transitionSpeed;
    private Transform currentView;
    public float MoveProp;
    float loopDelay;

    IEnumerator Start()
    {
        transitionSpeed = (transitionSpeed / MoveProp) * Time.deltaTime;
        loopDelay = 5;
        foreach (Transform i in views)
        {
            for (int a = 3; a < views.Length; a++)
            {
                currentView = views[a];
                yield return new WaitForSeconds(loopDelay);
            }
            break;
        }
        if(currentView == views[views.Length - 1])
        {
            CineCam.SetActive(false);
        }
    }
    private void LateUpdate()
    {
        MoveCineCamera();
    }
    // Update is called once per frame
    void MoveCineCamera()
    {
       transform.position =  Vector3.Lerp(transform.position, currentView.position, (transitionSpeed / MoveProp) * Time.deltaTime);
       Vector3 currentAngle = new Vector3
          (Mathf.LerpAngle(transform.rotation.eulerAngles.x, currentView.transform.rotation.eulerAngles.x, (transitionSpeed  / MoveProp) * Time.deltaTime),
           Mathf.LerpAngle(transform.rotation.eulerAngles.y, currentView.transform.rotation.eulerAngles.y, (transitionSpeed  / MoveProp) * Time.deltaTime),
           Mathf.LerpAngle(transform.rotation.eulerAngles.z, currentView.transform.rotation.eulerAngles.z, (transitionSpeed /MoveProp) * Time.deltaTime));
        transform.eulerAngles = currentAngle;
    }
}
