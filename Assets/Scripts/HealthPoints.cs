using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPoints : MonoBehaviour
{
    public ParticleSystem hitPartikiel;
    public ParticleSystem deathBoomPartikiel;
    public ParticleSystem deathBleedPartikiel;

    public List<AudioSource> damageSounds, deathSounds;

    public float deathBleedTime;
    public float deathBoomTime;
    public float hitTime;

    public int health = 3;
    public int bulletDamage = 1;
    public float timeToDie;
    public bool diesForAmen = true;
    private bool isDying = false;
    private Animator animator;
    public GridController grid;
    

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        grid = FindObjectOfType<GridController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator WaitAndDeactivate()
    {
        float t = 0;
        while(t < timeToDie)
        {
            t += Time.deltaTime;
            yield return null;
        }
        gameObject.SetActive(false);
    }

    void PlayRandomClip(List<AudioSource> sources)
    {
        int n = Random.Range(0, sources.Count - 1);
        sources[n].Play();
    }

    public void SiatkaDie()
    {
        ParticleSystem p = Instantiate(deathBoomPartikiel, transform.position, Quaternion.identity, transform);
        p.GetComponent<Renderer>().material = gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material;
        Destroy(p, deathBoomTime);
        grid.RemoveFromGrid(gameObject);
        health = 0;
        animator.SetTrigger("die");
        PlayRandomClip(deathSounds);
        if (diesForAmen)
        {
            Destroy(this.gameObject, timeToDie);
        }
        else
        {
            StartCoroutine("WaitAndDeactivate");
        }
    }

    public void DealDamage(int dmg, Vector3 direction)
    {
        health-=dmg;
        if (health <= 0 && !isDying)
        {
            PlayRandomClip(deathSounds);
            ParticleSystem p = Instantiate(deathBleedPartikiel, transform.position, Quaternion.identity);
            p.GetComponent<Renderer>().material = gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material;
            Destroy(p, deathBleedTime);
            isDying = true;

            grid.RemoveFromGrid(gameObject);

            animator.SetTrigger("die");
            if(diesForAmen)
            {
                Destroy(this.gameObject, timeToDie);
            }
            else
            {
                StartCoroutine("WaitAndDeactivate");
            }
            
        }
        else if(health > 0)
        {
            PlayRandomClip(damageSounds);
            Quaternion targetRotation = Quaternion.LookRotation(-direction, Vector3.right); ;
            ParticleSystem p = Instantiate(hitPartikiel, transform.position, targetRotation);
            p.GetComponent<Renderer>().material = gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material;
            Destroy(p, hitTime);
            Debug.Log("jeszcze nie umieram");
            animator.SetTrigger("receiveDamage");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            DealDamage(bulletDamage, (transform.position - collision.transform.position));
        }
    }
}
