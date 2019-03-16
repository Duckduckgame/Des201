using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiController : MonoBehaviour
{
    private Transform player;
    private PlayerPositon playerPositon;
    private GameObject lantern;
    private Transform enemyStartPos;
    public float speed = 4.0f;
    private GameObject[] enemies;
    public float distanceBetweenEnemies = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerPositon = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPositon>();
        lantern = player.GetChild(1).gameObject;
        enemyStartPos = gameObject.transform;
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        ChasePlayer();
    }

    public void ChasePlayer()
    {
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                float currentDistance = Vector3.Distance(transform.position, enemy.transform.position);

                if (currentDistance < distanceBetweenEnemies)
                {
                    Vector3 dist = transform.position - enemy.transform.position;
                    transform.position += dist * Time.deltaTime;
                }
            }
        }

        if (lantern.transform.GetChild(0).gameObject.GetComponent<Light>().enabled)
        {

            gameObject.GetComponent<MeshRenderer>().enabled = true;
            gameObject.GetComponent<Collider>().enabled = true;
            transform.LookAt(player.transform);
            transform.position += transform.forward * speed * Time.deltaTime;

        }
        else
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<Collider>().enabled = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.transform.position = new Vector3(playerPositon.m_reachedPosition.x, playerPositon.m_reachedPosition.y + 1,
                playerPositon.m_reachedPosition.z);
        }
    }
}
