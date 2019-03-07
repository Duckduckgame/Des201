using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    public Transform targetPos;
    private Vector3 initialPosition, targetPosition, targetPosDebug;
    public float speed = 3;
    public bool loop;

    void Start()
    {
        initialPosition = transform.position;
        SetTargetPosition(targetPos.position);
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
            SetTargetPosition(targetPos.position);
        }
        else
        {
            targetPosition = initialPosition;
        }
    }

    private void SetTargetPosition(Vector3 localPosition)
    {
        targetPosition = localPosition;
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
            Debug.DrawLine(transform.position, targetPos.position, Color.red);
            Gizmos.DrawSphere(targetPos.position, 1.0f);
        }
    }
}
