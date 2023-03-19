using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime4Axis : MonoBehaviour
{
    [SerializeField] private float rotationTime;
    [SerializeField] private float movementTime;
    [SerializeField] private float comebackTime;
    [Tooltip("Frac 0-1")]

    public Transform checkPivot;
    private Rigidbody rb;
    private Vector3 lookDirection;
    private Vector3 prevLookDirection;
    public bool isRotating = false, isMoving = false;

    private static float tileWidth = 2f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        prevLookDirection = Vector3.zero;
    }

    void Update()
    {

    }

    public void Jump(Vector2 direction)
    {
        if (!isRotating && !isMoving)
        {
            if (direction.x > 0)
            {
                lookDirection = new Vector3(0, 90, 0);
            }
            else if (direction.y > 0)
            {
                lookDirection = new Vector3(0, 0, 0);
            }
            else if (direction.x < 0)
            {
                lookDirection = new Vector3(0, -90, 0);
            }
            else if (direction.y < 0)
            {
                lookDirection = new Vector3(0, 180, 0);
            }

            if (prevLookDirection != lookDirection)
            {
                IEnumerator rotate = RotateTowards(Quaternion.Euler(lookDirection));
                StartCoroutine(rotate);
            }
            IEnumerator move = Move(direction);
            StartCoroutine(move);
            prevLookDirection = lookDirection;
        }
    }

    private IEnumerator RotateTowards(Quaternion targetRotation)
    {
        Quaternion originalRotation = transform.rotation;
        float t = 0;
        isRotating = true;
        while (t < rotationTime)
        {
            t += Time.deltaTime;
            transform.rotation = Quaternion.Lerp(originalRotation, targetRotation, t / rotationTime);
            yield return null;
        }
        isRotating = false;
    }

    private void AdjustPosition()
    {
        Vector2Int rounded = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z));

        if (rounded.x % 2 == 0)
        {
            if (transform.position.x - rounded.x > 0)
            {
                rounded.x -= 1;
            }
            else
            {
                rounded.x += 1;
            }
        }
        if (rounded.y % 2 == 0)
        {
            if (transform.position.z - rounded.y > 0)
            {
                rounded.y -= 1;
            }
            else
            {
                rounded.y += 1;
            }
        }

        Vector3 targetPosition = new Vector3(rounded.x, transform.position.y, rounded.y);
        transform.position = targetPosition;
        //IEnumerator smoothTranslate = SmoothTranslate(targetPosition);
        //StartCoroutine(smoothTranslate);
    }

    //private IEnumerator SmoothTranslate(Vector3 targetposition)
    //{
    //    Debug.Log("trochê przesunê"); 
    //    Vector3 originalPos = transform.position;
    //    float t = 0;
    //    while (t < comebackTime)
    //    {
    //        t += Time.deltaTime;
    //        transform.position = Vector3.Lerp(originalPos, targetposition, t / comebackTime);
    //        yield return null;
    //    }
    //}

    private IEnumerator Move(Vector2 direction)
    {
        //Debug.Log(name + "moving");
        float t = 0;

        //if(prevLookDirection != lookDirection)
        //{
            while (t < rotationTime)
            {
                //Debug.Log(name + " t =" + t);
                t += Time.deltaTime;
                yield return null;
            }
        //}
        //Debug.Log(name + "waited, moving");

        RaycastHit hit;
        if (!Physics.Raycast(checkPivot.position, transform.forward, out hit, tileWidth) ||
            (hit.collider.gameObject.layer != LayerMask.NameToLayer("nonwalkable") &&
            hit.collider.gameObject.layer != LayerMask.NameToLayer("movableObstacle")))
        {
            t = 0;
            isMoving = true;
            Vector3 originalPosition = transform.position;
            Vector3 targetPosition = new Vector3(originalPosition.x + direction.x * tileWidth,
                                                     0f,
                                                  originalPosition.z + direction.y * tileWidth);
            bool movingInterrupted = false;
            while (t < movementTime)
            {
                rb.position = Vector3.Lerp(transform.position, targetPosition, t / movementTime);
                if (Physics.Raycast(checkPivot.position, transform.forward, out hit, tileWidth * (1 - t) * 0.48f) &&
                    hit.collider.gameObject.layer == LayerMask.NameToLayer("movableObstacle"))
                {
                    movingInterrupted = true;
                    break;
                }
                t += Time.deltaTime;
                yield return null;
            }
            if(movingInterrupted)
            {
                t = 0f;
                while(t<comebackTime)
                {
                    rb.position = Vector3.Lerp(transform.position, originalPosition, t / comebackTime);
                    t += Time.deltaTime;
                    yield return null;
                }
            }
            AdjustPosition();

            isMoving = false;
        }
    }
}
