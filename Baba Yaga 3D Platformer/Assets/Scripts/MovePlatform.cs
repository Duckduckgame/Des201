using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    public Vector3 localTargetPosition = new Vector3(0, 0, 5);
    private Vector3 initialPosition, targetPosition, targetPosDebug;
    public float speed = 3;
    public bool loop;

    void Start()
    {
        initialPosition = transform.position;
        SetTargetPosition(localTargetPosition);
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (transform.position == targetPosition)
        {
            if (loop)
            {
                SwitchDirection();
            }
        }
    }

    public void SwitchDirection()
    {
        if (transform.position == initialPosition)
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
