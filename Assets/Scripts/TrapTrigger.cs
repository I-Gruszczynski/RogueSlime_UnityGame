using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTrigger : MonoBehaviour
{
    public float timeToDie = 2f;
    public GameObject slime;
    public bool latamy=false;

    Vector3 lastPos;
    public Transform obj;
    float threshold = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Offset wynosi: " + offset);
        if(latamy)
        {
            EnemyController enemyController = slime.GetComponent<EnemyController>();
            enemyController.Sleep();
            Debug.Log("It's a trap!!!");
            StartCoroutine("WaitThenDestroy");   
        }
    }

    public void Lec()
    {
        StartCoroutine("WaitThenDestroy");
    }

    private IEnumerator WaitThenDestroy()
    {
        float t = 0;
        while(t < timeToDie)
        {
           t += Time.deltaTime;
            yield return null;
        }
        Destroy(slime);
    }
}
