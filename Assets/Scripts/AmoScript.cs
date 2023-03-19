using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmoScript : MonoBehaviour
{
    public int amoBonus;
    public float yPosDefault;
    public float movementSpeed, movementRange;
    public float scaleMultiplier, baseScale;

 
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(transform.position.x, yPosDefault, transform.position.z);
        transform.localScale = new Vector3(1, 1, 1) * (baseScale + amoBonus * scaleMultiplier);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.up * Mathf.Sin(Time.time * movementSpeed) * movementRange;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Shooting shootingScript = other.GetComponent<Shooting>();
            if(shootingScript != null)
            {
                shootingScript.bulletNumer += amoBonus;
                shootingScript.UpdateAmoText();
            }
            Destroy(this.gameObject);
        }
    }
}
