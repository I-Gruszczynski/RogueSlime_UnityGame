using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float offset;

    [Tooltip("0-1 fraction for Lerp")]
    public float followSpeed;

    void FixedUpdate()
    {
        if(player != null)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(player.position.x - offset, transform.position.y, player.position.z - offset), followSpeed);
        }
        
    }
}
