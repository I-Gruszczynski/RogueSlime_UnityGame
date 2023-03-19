using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthPlayer : MonoBehaviour
{
    public HealthPoints healthPoints;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.name == "Rampage Slime" && healthPoints.health <= 0)
        {

            StartCoroutine("WaitAndDie");
        }

    }

    private IEnumerator WaitAndDie()
    {
        float t = 0;
        while (t < healthPoints.timeToDie)
        {
            Debug.Log(healthPoints.timeToDie);
            t += Time.deltaTime;
            yield return null;
        }
        ZmienScene(2);
    }

    public void ZmienScene(int numerSceny)
    {
        SceneManager.LoadScene(numerSceny);
    }
}
