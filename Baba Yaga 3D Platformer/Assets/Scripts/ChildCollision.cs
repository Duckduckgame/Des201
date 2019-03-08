using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildCollision : MonoBehaviour
{

    characterController CC;

    private void Start()
    {
        CC = GetComponentInParent<characterController>();
    }


    private void OnTriggerEnter(Collider other)
    {
        CC.ChildCollisionEnter(gameObject);
    }
    private void OnTriggerStay(Collider other)
    {
        CC.ChildCollisionStay(gameObject);
    }
    private void OnTriggerExit(Collider other)
    {
        CC.ChildCollisionLeave(gameObject);
    }
}
