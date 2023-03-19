using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterEnemyController : MonoBehaviour
{
    public float bangNotifyDistance = 10.0f;
    public float notifyDistance = 3f;
    public float crouchNotifyDistance = 1f;
    public float movementTime;
    public AudioSource surpriseSound;
    public List<AudioSource> rzygSounds;

    public bool isTeczowyRzygajacy = false;
    public int attackDamage;
    public float attackTime;
    public float cooldown;
    private bool canAttack = true;

    private Transform player;
    private BetterPlayerController playerScript;
    private HealthPoints playerHealthScript;
    private Vector2Int movementDirection;
    private Vector3 directionToPlayer;
    private SlimeMove slimeMove;
    private GridController grid;
    [SerializeField] private bool isFollowing = false;

    private Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerScript = player.gameObject.GetComponent<BetterPlayerController>();
        playerHealthScript = player.gameObject.GetComponent<HealthPoints>();
        slimeMove = GetComponent<SlimeMove>();
        grid = FindObjectOfType<GridController>();
        anim = GetComponent<Animator>();
    }

    void PlayRandomClip(List<AudioSource> sources)
    {
        int n = Random.Range(0, sources.Count - 1);
        sources[n].Play();
    }

    private void OnEnable()
    {
        Shooting.OnShot += TryDistanceNotifyBang;
    }

    private void OnDisable()
    {
        Shooting.OnShot -= TryDistanceNotifyBang;
    }

    public void Notify()
    {
        isFollowing = true;
        anim.SetTrigger("notify");
        surpriseSound.Play();
    }

    public void TryDistanceNotify(bool isCrouching)
    {
        float distance = isCrouching ? crouchNotifyDistance : notifyDistance;
        if ((Mathf.Abs(transform.position.x - player.position.x) <= distance) && (Mathf.Abs(transform.position.z - player.position.z) <= distance))
        {
            Notify();
        }
    }

    private void Attack()
    {
        StartCoroutine("AttackCoroutine");
    }

    private void RotateTowardsPlayer()
    {
        if (player == null) return;
        Vector2 directionToPlayer = new Vector2(transform.position.x - player.position.x, transform.position.z - player.position.z);
        Vector2Int dir = Vector2Int.zero;
        if(Mathf.Abs(directionToPlayer.x) > 0.8f && Mathf.Abs(directionToPlayer.x) < 1.2f)
        {
            dir.x = (int)(-1 * Mathf.Sign(directionToPlayer.x));
        }
        if (Mathf.Abs(directionToPlayer.y) > 0.8f && Mathf.Abs(directionToPlayer.y) < 1.2f)
        {
            dir.y = (int)(-1 * Mathf.Sign(directionToPlayer.y));
        }
        slimeMove.Rotate(dir);
    }

    private IEnumerator AttackCoroutine()
    {
        if (player != null)
        {
            Vector3 playerPosition = player.position;
            canAttack = false;
            slimeMove.isMoving = true; // lock movement
            float t = 0;
            while (t < attackTime)
            {
                t += Time.deltaTime;
                yield return null;
            }
            slimeMove.isMoving = false; // unlock movement
            canAttack = true;
            if (Vector3.Distance(playerPosition, player.position) <= 1.2f)
            {
                playerHealthScript.DealDamage(attackDamage, player.position - transform.position);
            }
            StartCoroutine("CooldownCoroutine");
        }
        
    }
    private IEnumerator CooldownCoroutine()
    {
        canAttack = false;
        slimeMove.isMoving = true; // lock movement
        float t = 0;
        while (t < cooldown)
        {
            t += Time.deltaTime;
            yield return null;
        }
        slimeMove.isMoving = false; // unlock movement
        canAttack = true;
    }

    public void TryDistanceNotifyBang()
    {
        if (player == null) return;
        if (!isFollowing)
        {
            if (Vector3.Distance(transform.position, player.position) <= bangNotifyDistance)
            {
                Notify();
            }
        }
    }

    public void Unnotify()
    {
        isFollowing = false;
    }

    private void FollowPlayer()
    {
        if (player == null) return;
        directionToPlayer = player.position - transform.position;
        if (Mathf.Abs(directionToPlayer.x) > Mathf.Abs(directionToPlayer.z))
        {
            if (directionToPlayer.x > 0)
            {
                movementDirection = new Vector2Int(1, 0);
            }
            else
            {
                movementDirection = new Vector2Int(-1, 0);
            }
        }
        else
        {
            if (directionToPlayer.z > 0)
            {
                movementDirection = new Vector2Int(0, 1);
            }
            else
            {
                movementDirection = new Vector2Int(0, -1);
            }
        }
        float xDist = Mathf.Abs(player.position.x - transform.position.x);
        float zDist = Mathf.Abs(player.position.z - transform.position.z);
        if ((xDist <= 1.2f && zDist <= 0.1f)|| (xDist <= 0.1f && zDist <= 1.2f))
        {
            if(canAttack)
            {
                Attack();
                if (isTeczowyRzygajacy)
                {
                    anim.SetTrigger("Rzyg");
                    PlayRandomClip(rzygSounds);
                }
                else
                {
                    anim.SetTrigger("jump");
                }
            }         
        }
        else
        {
            anim.SetTrigger("jump");
            grid.MoveObject(gameObject, movementDirection, movementTime);
        }
    }

    void Update()
    {
        if (!slimeMove.isMoving && isFollowing)
        {
            FollowPlayer();
        }

        if (!isFollowing && player != null)
        {
            TryDistanceNotify(playerScript.isCrouching);
        }
        else if (Vector3.Distance(transform.position, player.position) <= 1.1f)
        {
            RotateTowardsPlayer();
        }
    }
}
