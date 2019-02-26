using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingPlatform : MonoBehaviour
{

    public float moveSpeed = 10;
    Vector3 pos1;
    Vector3 pos2;

    // Start is called before the first frame update
    void Start()
    {
        Transform[] transforms = GetComponentsInChildren<Transform>();

        pos1 = transforms[1].position;
        pos2 = transforms[2].position;

        transform.position = pos1;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(pos1, pos2, Time.deltaTime * moveSpeed);
    }
}
