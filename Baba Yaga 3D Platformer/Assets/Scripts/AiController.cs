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
    AudioManager audioManager;
    public float speed = 4.0f;
    private GameObject[] enemies;
    public float distanceBetweenEnemies = 2.0f;
    private Checkpoint checkpoint;
    public float chaseDistance = 50.0f;
    public Vector3 m_originalPos { get { return originalPos; } set { originalPos = value; } }
    private Vector3 originalPos;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerPositon = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPositon>();
        lantern = GameObject.FindGameObjectWithTag("Lantern");
        enemyStartPos = gameObject.transform;
        gameObject.SetActive(false);
        audioManager = AudioManager.instance;
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        checkpoint = GameObject.FindGameObjectWithTag("Checkpoint").GetComponent<Checkpoint>();
        originalPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        ChasePlayer();

        if(playerPositon.m_playerDead == true)
        {
            transform.position = originalPos;

            if (lantern.GetComponent<ActivateLantern>().m_lanternCollider.enabled)
            {
                lantern.GetComponent<ActivateLantern>().m_lanternCollider.enabled = false;
                lantern.transform.GetChild(0).gameObject.GetComponent<Light>().enabled = false;
            }
        }
    }

    public void ChasePlayer()
    {
        foreach (GameObject enemy in enemies)
        {
            audioManager.PlaySound("EnemySound");
            //gameObject.SetActive(true);
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

        RaycastHit hit;
        int layerMask = 1 << 8;
        layerMask = ~layerMask;

        Vector3 start = player.transform.position;
        if (Physics.Raycast(start, Vector3.down, out hit, 100, layerMask))
        {
            if (lantern.GetComponent<ActivateLantern>().m_lanternCollider.enabled && checkpoint.m_OnCheckpoint == false &&
                lantern.GetComponent<ActivateLantern>().m_lanternCollider.enabled && hit.transform.gameObject.tag != "Checkpoint")
            {
                if (Vector3.Distance(transform.position, player.position) <= chaseDistance)
                {
                    gameObject.GetComponent<MeshRenderer>().enabled = true;
                    gameObject.GetComponent<Collider>().enabled = true;
                    transform.LookAt(player);
                    transform.position += transform.forward * speed * Time.deltaTime;
                }
                else
                {
                    gameObject.GetComponent<MeshRenderer>().enabled = false;
                    gameObject.GetComponent<Collider>().enabled = false;
                }
            }
            else if(!lantern.GetComponent<ActivateLantern>().m_lanternCollider.enabled)
            {
                gameObject.GetComponent<MeshRenderer>().enabled = false;
                gameObject.GetComponent<Collider>().enabled = false;
            }
            else if (lantern.GetComponent<ActivateLantern>().m_lanternCollider.enabled && checkpoint.m_OnCheckpoint == true ||
                    hit.transform.gameObject.tag == "Checkpoint")
            {
                if (Vector3.Distance(transform.position, player.position) <= chaseDistance)
                {
                    gameObject.GetComponent<MeshRenderer>().enabled = true;
                    gameObject.GetComponent<Collider>().enabled = true;
                }   
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerPositon.m_playerDead = true;

            player.transform.position = new Vector3(playerPositon.m_reachedPosition.x, playerPositon.m_reachedPosition.y + 1,
                playerPositon.m_reachedPosition.z);

            transform.position = originalPos;
        }
    }
}
