using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    public Vector3 localTargetPosition;
    public Vector3 m_initialPosition { get { return initialPosition; } set { initialPosition = value; } }
    private Vector3 initialPosition, targetPosition, targetPosDebug;
    public float speed = 3;
    public bool loop;
     public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        initialPosition = transform.position;
        SetTargetPosition(localTargetPosition);
    }

    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime, speed);

        if (Vector3.Distance(transform.position, targetPosition) < 0.001f)
        {
            if (loop)
            {
                SwitchDirection();
            }
        }
    }

    public void SwitchDirection()
    {
        if (Vector3.Distance(transform.position, initialPosition) < 0.001f)
        {
            SetTargetPosition(localTargetPosition);
        }
        else
        {
            targetPosition = initialPosition;
        }
    }

    private void SetTargetPosition(Vector3 localPosition)
    {
        targetPosition = initialPosition + transform.TransformDirection(localPosition);
        targetPosDebug = targetPosition;
    }

    void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Debug.DrawLine(initialPosition, targetPosDebug, Color.red);
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(localTargetPosition), Color.red);
        }
    }
}