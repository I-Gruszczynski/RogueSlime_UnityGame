using UnityEngine;
using UnityEngine.UI;


public class Shooting : MonoBehaviour
{
    public delegate void ShootAction();
    public static event ShootAction OnShot;
    public bool chceMenu = true;
    public AudioSource wystrzalAudio;

    // Start is called before the first frame update
    public Transform firePoint;
    public GameObject bulletPrefab;

    public int bulletNumer = 10;
    public float bulletForce = 20f;
    private Animator animator;


    public bool jestMenu = false;
    public Image ImageMessageBox;
    public Image ImagePauseMenu;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(chceMenu)
        {
            if (ImageMessageBox.gameObject.activeInHierarchy || ImagePauseMenu.gameObject.activeInHierarchy)
            {
                jestMenu = true;
            }
            else
            {
                jestMenu = false;
            }
        }



        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (jestMenu == false)
        {
            if (bulletNumer > 0)
            {
                if (animator != null)
                {
                    animator.SetTrigger("shoot");
                }
                wystrzalAudio.Play();
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                rb.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);
                bulletNumer--;

                UpdateAmoText();

                OnShot?.Invoke();
            }
        }
    }

    public void UpdateAmoText()
    {
        var number = GameObject.FindGameObjectWithTag("BulletNumber");
        if (number != null)
        {
            number.GetComponent<Text>().text = "x" + bulletNumer;
        }
    }

}
