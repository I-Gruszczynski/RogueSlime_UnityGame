using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Slime4Axis))]
public class PlayerController : MonoBehaviour
{
    private Vector2 movementDirection;
    private Slime4Axis slimeScript;
    private Animator animator;


    void Start()
    {
        slimeScript = GetComponent<Slime4Axis>();
        movementDirection = Vector2.up;
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("EnemyFieldOfView"))
        {
            EnemyController enemy = other.GetComponentInParent<EnemyController>();
            if(enemy != null)
            {
                enemy.Notify();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("EnemyFieldOfView"))
        {
            EnemyController enemy = other.GetComponentInParent<EnemyController>();
            if (enemy != null)
            {
                enemy.Sleep();
            }
        }
    }

    void Update()
    {
        if(animator != null)
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {
                animator.SetBool("crouching", true);
            }
            else
            {
                animator.SetBool("crouching", false);
            }
        }


        if (Input.GetKeyDown(KeyCode.W) && !slimeScript.isMoving && !slimeScript.isRotating ||
            Input.GetKeyDown(KeyCode.UpArrow) && !slimeScript.isMoving && !slimeScript.isRotating)
        {
            slimeScript.Jump(movementDirection);
            animator.SetTrigger("jump");
        }
        else if(Input.GetKeyDown(KeyCode.D) && !slimeScript.isMoving && !slimeScript.isRotating ||
            Input.GetKeyDown(KeyCode.RightArrow) && !slimeScript.isMoving && !slimeScript.isRotating)
        {
            if(movementDirection == Vector2.up)
            {
                movementDirection = Vector2.right;
            }
            else if (movementDirection == Vector2.right)
            {
                movementDirection = Vector2.down;
            }
            else if (movementDirection == Vector2.down)
            {
                movementDirection = Vector2.left;
            }
            else if (movementDirection == Vector2.left)
            {
                movementDirection = Vector2.up;
            }
            animator.SetTrigger("jump");
            slimeScript.Jump(movementDirection);
        }
        else if(Input.GetKeyDown(KeyCode.A) && !slimeScript.isMoving && !slimeScript.isRotating ||
            Input.GetKeyDown(KeyCode.LeftArrow) && !slimeScript.isMoving && !slimeScript.isRotating)
        {
            if (movementDirection == Vector2.up)
            {
                movementDirection = Vector2.left;
            }
            else if (movementDirection == Vector2.right)
            {
                movementDirection = Vector2.up;
            }
            else if (movementDirection == Vector2.down)
            {
                movementDirection = Vector2.right;
            }
            else if (movementDirection == Vector2.left)
            {
                movementDirection = Vector2.down;
            }
            animator.SetTrigger("jump");
            slimeScript.Jump(movementDirection);
        }
    }
}
