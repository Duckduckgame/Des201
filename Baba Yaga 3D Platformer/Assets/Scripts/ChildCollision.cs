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
        if (other.gameObject.layer != 8)
            CC.ChildCollisionEnter(gameObject);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer != 8)
            CC.ChildCollisionStay(gameObject);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer != 8)
            CC.ChildCollisionLeave(gameObject);
    }
}
