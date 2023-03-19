using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WilczydolScript : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject pianka;
    public float fallingspeed = 20f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        HealthPoints hp = other.GetComponent<HealthPoints>();
        pianka.SetActive(false);
        if (hp != null)
        {
            hp.grid.RemoveFromGrid(other.gameObject);
            Destroy(other.gameObject.GetComponent<SlimeMove>());
            Rigidbody rb = hp.gameObject.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.useGravity = true;
            hp.DealDamage(int.MaxValue, Vector3.up);
            IEnumerator fall = Falldown(other.gameObject, 1);
            StartCoroutine(fall);
        }
    }

    private IEnumerator Falldown(GameObject obj, float maxT)
    {
        float t = 0;
        while(t < maxT)
        {
            obj.transform.position -= new Vector3(0f, Time.deltaTime * fallingspeed, 0f);
            t += Time.deltaTime;
            yield return null;
        }
    }
}
