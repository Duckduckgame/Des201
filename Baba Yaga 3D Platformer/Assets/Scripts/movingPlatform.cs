using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class movingPlatform : MonoBehaviour
{

    public float moveSpeed = 10;
    public Vector3 pos1;
    public Vector3 pos2;

    // Start is called before the first frame update
    void Start()
    {


        transform.position = pos1;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(pos1, pos2, Time.deltaTime * moveSpeed);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(pos1, 1f);
        Gizmos.DrawSphere(pos2, 1f);
    }
}
