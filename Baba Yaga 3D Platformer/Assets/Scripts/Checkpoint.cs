using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private PlayerPositon playerPositon;

    // Start is called before the first frame update
    void Start()
    {
        playerPositon = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPositon>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerPositon.m_reachedPosition = transform.position;
        }
    }
}
