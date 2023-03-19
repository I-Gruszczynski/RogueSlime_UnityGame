using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiatkaScript : MonoBehaviour
{
    public Animator anim;
    public float liftingTime = 0.5f;
    public float liftingHeight = 3f;
    public float slimeLiftingHeight = 5f;

    public List<HealthPoints> slimesInRange;
    // Start is called before the first frame update
    void Start()
    {
        slimesInRange = new List<HealthPoints>();
    }

    public void Siup()
    {
        anim.SetTrigger("Siup");
        StartCoroutine("Lift");
        foreach(HealthPoints hp in slimesInRange)
        {
            IEnumerator movePoorSlime = SlimeInNetMove(hp.transform);
            StartCoroutine(movePoorSlime);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        HealthPoints hp = other.GetComponent<HealthPoints>();
        if(hp != null)
        {
            slimesInRange.Add(hp);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        HealthPoints hp = other.GetComponent<HealthPoints>();
        if (hp != null)
        {
            slimesInRange.Remove(hp);
        }
    }

    private IEnumerator Lift()
    {
        Vector3 originalPos = transform.position;
        Vector3 targetPos = originalPos + Vector3.up * liftingHeight;
        float t = 0;
        while(t < liftingTime)
        {
            t += Time.deltaTime;
            yield return null;
            transform.position = Vector3.Lerp(originalPos, targetPos, t/liftingTime);
        }
    }

    private IEnumerator SlimeInNetMove(Transform slime)
    {
        Vector3 originalPos = transform.position;
        Vector3 targetPos = new Vector3(transform.position.x, slimeLiftingHeight, transform.position.z);
        float totalTime = liftingTime;
        float t = 0;
        while(t < totalTime)
        {
            slime.position = Vector3.Lerp(originalPos, targetPos, t / totalTime);
            t += Time.deltaTime;
            yield return null;
        }
        slime.GetComponent<HealthPoints>().SiatkaDie();
    }
}
