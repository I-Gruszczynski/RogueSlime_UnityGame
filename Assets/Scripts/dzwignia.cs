using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dzwignia : MonoBehaviour
{
    private bool PlayerInRange = false;

    public Animator anim;


    public SiatkaScript siatka;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            if(PlayerInRange)
            {
                siatka.Siup();
                anim.SetTrigger("Siup");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInRange = false;
        }
    }
}
