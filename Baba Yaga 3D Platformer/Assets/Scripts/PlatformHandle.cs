using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformHandle : MonoBehaviour
{
    public Transform targetPos;
    private Vector3 initialPosition, targetPosition, targetPosDebug;
    public float speed = 3;
    public bool loop;

    void Start()
    {
        initialPosition = transform.position;
        targetPosition = targetPos.position;
        targetPosDebug = targetPosition;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (loop && transform.position == targetPosition)
        {
            SwitchDirection();
        }
    }

    public void SwitchDirection()
    {
        if (transform.position == initialPosition)
        {
            targetPosition = targetPos.position;
        }
        else
        {
            targetPosition = initialPosition;
        }
    }

    void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Debug.DrawLine(initialPosition, targetPosDebug, Color.red);
        }
        else
        {
            Debug.DrawLine(transform.position, targetPos.position, Color.red);
            Gizmos.DrawSphere(targetPos.position, 1.0f);
        }
    }
}
