using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    public float timeToDie = 1f;
    public GameObject slime;
    public GameObject player;
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
        Debug.Log("It's a trap!!!");
        BoxCollider boxcolliderSlime = slime.GetComponent<BoxCollider>();
        BoxCollider boxcolliderPlayer = player.GetComponent<BoxCollider>();
        EnemyController enemyController = slime.GetComponent<EnemyController>();
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log(collision.collider.name);
            enemyController.Sleep();
            boxcolliderSlime.enabled = false;
            StartCoroutine("WaitThenDestroySlime");
        }
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log(collision.collider.name);
            boxcolliderPlayer.enabled = false;
            StartCoroutine("WaitThenDestroyPlayer");
        }
    }

    private IEnumerator WaitThenDestroySlime()
    {
        float t = 0;
        while (t < timeToDie)
        {
            t += Time.deltaTime;
            yield return null;
        }
        Destroy(slime);

    }

    private IEnumerator WaitThenDestroyPlayer()
    {
        float t = 0;
        while (t < timeToDie)
        {
            t += Time.deltaTime;
            yield return null;
        }
        Destroy(player);

    }
}
