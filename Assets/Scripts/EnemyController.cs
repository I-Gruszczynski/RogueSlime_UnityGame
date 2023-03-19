using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float bangNotifyDistance;

    private Transform player;
    private Vector2 movementDirection;
    private Slime4Axis slimeScript;
    private bool isFollowing = false;

    private Vector3 directionToPlayer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        slimeScript = GetComponent<Slime4Axis>();
        Sleep();
    }

    private void OnEnable()
    {
        Shooting.OnShot += TryDistanceNotify;
    }

    private void OnDisable()
    {
        Shooting.OnShot -= TryDistanceNotify;
    }


    public void Notify()
    {
        isFollowing = true;
    }

    public void TryDistanceNotify()
    {
        if(Vector3.Distance(transform.position, player.position) < bangNotifyDistance)
        {
            Notify();
        }
    }

    public void Sleep()
    {
        isFollowing = false;
    }

    private void FollowPlayer()
    {
        directionToPlayer = player.position - transform.position;
        if (Mathf.Abs(directionToPlayer.x) > Mathf.Abs(directionToPlayer.z))
        {
            if (directionToPlayer.x > 0)
            {
                movementDirection = new Vector2(1, 0);
            }
            else
            {
                movementDirection = new Vector2(-1, 0);
            }
        }
        else
        {
            if (directionToPlayer.z > 0)
            {
                movementDirection = new Vector2(0, 1);
            }
            else
            {
                movementDirection = new Vector2(0, -1);
            }
        }
        Debug.Log(name + "tring to jumpt to " + movementDirection);
        slimeScript.Jump(movementDirection);
    }

    void Update()
    {
        if(isFollowing)
        {
            FollowPlayer();
        }
    }
}
