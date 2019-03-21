using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiController : MonoBehaviour
{
    private NavMeshAgent nav;
    private Transform player;
    private GameObject lantern;
    private Transform enemyStartPos;
    AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        lantern = player.GetChild(1).gameObject;
        enemyStartPos = gameObject.transform;
        gameObject.SetActive(false);
        audioManager = AudioManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChasePlayer()
    {
        if (lantern.transform.GetChild(0).gameObject.GetComponent<Light>().enabled)
        {
            audioManager.PlaySound("EnemySound");
            gameObject.SetActive(true);
            nav.SetDestination(player.position);
        }
        else
            gameObject.SetActive(false);
    }

}
