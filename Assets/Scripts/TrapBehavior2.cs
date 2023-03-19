using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBehavior2 : MonoBehaviour
{
    public float speed=3f;
    public GameObject trap;
    private Transform start;
    public float comebackTime = 5f;


    
    //private Transform endMarker;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    private IEnumerator SmoothTranslate(Vector3 targetposition)
    {
        //Debug.Log("trochê przesunê");
        Vector3 originalPos = transform.position;
       float t = 0;
       while(t < comebackTime)
       {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(originalPos, targetposition, t / comebackTime);
            yield return null;
       }
        
    }

    public void Trap()
    {
        IEnumerator enumerator = SmoothTranslate(new Vector3(8f, 8f, 5f));
        StartCoroutine(enumerator);
    }

}
