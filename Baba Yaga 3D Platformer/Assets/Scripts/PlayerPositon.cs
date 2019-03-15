using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPositon : MonoBehaviour
{
    public Vector3 m_reachedPosition { get { return reachedPosition; } set { reachedPosition = value; } }
    private Vector3 reachedPosition;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "RespawnTrigger")
        {
            transform.position = new Vector3(reachedPosition.x, reachedPosition.y + 1, reachedPosition.z);
        }
    }
}
