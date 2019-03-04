using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamCollision : MonoBehaviour
{
    [Tooltip("Sets the minimum camera distance when there is an object between player and camera")]
    public float minDistane = 1.0f;
    [Tooltip("Sets the default/maximum camera distance from the player")]
    public float maxDistance = 4.0f;
    [Tooltip("Sets how smoothly the camera transitions between maximum distance and minimum distance")]
    public float smooth = 10.0f;

    Vector3 dollyDir;
    private float distance;

    // Start is called before the first frame update
    void Awake()
    {
        dollyDir = transform.localPosition.normalized;
        distance = transform.localPosition.magnitude;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 desiredCamPosition = transform.parent.TransformPoint(dollyDir * maxDistance);

        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 8;

        // This would cast rays only against colliders in layer 8 (Hidden).
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;

        RaycastHit hit;

        // If platform is visible, make the camera collide with the platform
        if(GameObject.Find("Lantern").GetComponent<ActivateLantern>().platformCollider.enabled)
        {
            if (Physics.Linecast(transform.parent.position, desiredCamPosition, out hit))
            {
                distance = Mathf.Clamp((hit.distance), minDistane, maxDistance);
            }
            else
                distance = maxDistance;
        }
        // Otherwise if platform is hidden, ignore camera collision with the platform
        else
        {
            if (Physics.Linecast(transform.parent.position, desiredCamPosition, out hit, layerMask))
            {
                distance = Mathf.Clamp((hit.distance), minDistane, maxDistance);
            }
            else
                distance = maxDistance;
        }

        transform.localPosition = Vector3.Lerp(transform.localPosition, dollyDir * distance, Time.deltaTime * smooth);
    }
}
