using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMove : MonoBehaviour
{
    public bool isMoving = false;
    public float rotationTime;
    private Vector2 lookDirection, prevLookDirection;


    public List<AudioSource> jumpSounds;
    // Start is called before the first frame update
    void Start()
    {
        prevLookDirection = Vector3.zero;
    }

    void PlayRandomClip(List<AudioSource> sources)
    {
        int n = Random.Range(0, sources.Count - 1);
        sources[n].Play();
    }

    public void PlayJumpSound()
    {
        PlayRandomClip(jumpSounds);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Rotate(Vector2 direction)
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
        prevLookDirection = lookDirection;
    }

    private IEnumerator RotateTowards(Quaternion targetRotation)
    {
        Quaternion originalRotation = transform.rotation;
        float t = 0;
        while (t < rotationTime)
        {
            t += Time.deltaTime;
            transform.rotation = Quaternion.Lerp(originalRotation, targetRotation, t / rotationTime);
            yield return null;
        }
    }
}
