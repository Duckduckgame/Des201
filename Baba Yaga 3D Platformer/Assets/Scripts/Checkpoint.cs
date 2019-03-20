using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private PlayerPositon playerPositon;
    public bool m_OnCheckpoint { get { return onCheckPoint; } set { onCheckPoint = value; } }
    private bool onCheckPoint;

    // Start is called before the first frame update
    void Start()
    {
        playerPositon = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPositon>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            onCheckPoint = true;
            playerPositon.m_reachedPosition = transform.position;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            onCheckPoint = false;
        }
    }
}
