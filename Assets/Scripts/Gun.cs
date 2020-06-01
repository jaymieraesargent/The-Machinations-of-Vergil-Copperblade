using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    public float damage;
    public float range;
    public float fireRate;
    public float impaceForce = 50f;
    public float bulletSpeed = 100f;
    public float yAxis;
    public float xAxis;

    public string weaponName;

    public int maxAmmo;
    public int currentAmmo;
    public float reloadTime;
    public bool isReloading = false;
    public bool isZoomingIn = false;
    public bool isFiring = false;

    public Camera fpsCam;
   // public ParticleSystem muzzleFlash;
  //  public AudioSource gunShots;
   // public AudioSource reloadSound;
   // public GameObject impactEffect;
   // public Transform bulletShell;
   // public Transform firePoint;
  //  public GameObject bullet;
   // public GameObject myBullet;
   // public GameObject bulletHole;
    public Text ammoCount;

  //  public Animator anim; 

    private float nextTimeToFire = 0f;

    private Player player;

    private void Start()
    {
        player = GetComponentInParent<Player>();
        if(player == null)
        {
            Debug.LogError("dfds");
        }

        if (currentAmmo == -1)
        {
            currentAmmo = maxAmmo;
        }
    }

    void OnEnable()
    {
        isReloading = false;
     //   anim.SetBool("Reloading", false);
    }

    void Update()
    {
        if (isReloading)
        {
            return;
        }
        //WAS ALWAYS RUNNING RELOAD....but YEAH NAH MATE
        ///so only run Reload on button press.... NAH YEAH THATS GOOD AYE!
        if (Input.GetButtonDown("Reload"))
        {
            if (currentAmmo < maxAmmo)
            {           
                StartCoroutine(Reload());
            }
        }
        if (Input.GetMouseButton(0) && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            isFiring = true;
            Shoot();
        }
        else if(Input.GetMouseButtonUp(0))
        {
     //       anim.SetBool("Firing", false);
            isFiring = false;
        }
        Zoom();
        Walking();
        Cursor.lockState = CursorLockMode.Locked;
    }
    //Was Running Every frame...yea no dont do that...thats scrubby
    IEnumerator Reload()
    {
        //was running this on button press....but the Reload was constantly waiting....SO WE MOVED IT
        isReloading = true;
     //   reloadSound.Play();
   //     anim.SetBool("ZoomIn", false);
        Debug.Log("Reloading...");
     //   anim.SetBool("Reloading", true);
        yield return new WaitForSeconds(reloadTime - .25f);
       // anim.SetBool("Reloading", false);
        yield return new WaitForSeconds(.25f);
        currentAmmo = maxAmmo;
        ammoCount.text = currentAmmo.ToString();
        isReloading = false;
    }

    void Zoom()
    {
        if (Input.GetButton("Fire2"))
        {
            player.isZoomedIn = true;
            isZoomingIn = true;
            Debug.Log("Zoom in weapon");
      //      anim.SetBool("ZoomIn", true);
            fpsCam.fieldOfView = 30.0f;
        }
        else
        {
            player.isZoomedIn = false;
            isZoomingIn = false;
       //     anim.SetBool("ZoomIn", false);
            fpsCam.fieldOfView = 60.0f;
        }
    }

    void Walking()
    {
        if (Input.GetKey(KeyCode.W))
        {
      //      anim.SetBool("Walk", true);
        }
        else
        {
      //      anim.SetBool("Walk", false);
        }
    }
    //PEW PEW WAS RUNNING INTO NEGATIVES COS OLD MATE FORGOT A CAP!
    public void Shoot()
    {
        //Added the ability to not go below Zero...YEEEE BOI!
        if (currentAmmo > 0)
        {
        //    gunShots.Play();
         //   muzzleFlash.Play();
            currentAmmo--;

            if (ammoCount != null)
            {
                ammoCount.text = currentAmmo.ToString();
            }
            RaycastHit hit;
            if (isZoomingIn == true)
            {
                if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
                {
                    Debug.Log(hit.transform.name);
                    CharactetController targetPlayer = hit.collider.GetComponent<CharactetController>();
                    targetPlayer.DamagePlayer(damage);

             //       GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
               //     Destroy(impactGO, 2f);
                    var hitRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
               //     Instantiate(bulletHole, hit.point, hitRotation);
                }
            }
            else
            {
                Vector2 RandomShot = new Vector2(Random.Range(xAxis, yAxis), Random.Range(xAxis, yAxis));
                if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward + new Vector3(RandomShot.x, RandomShot.y, 0), out hit, range))
                {
                    Player targetPlayer = hit.collider.GetComponent<Player>();
                    if (targetPlayer != null)
                    {
                        targetPlayer.DamagePlayer(damage);
                    }

             //       GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
             //       Destroy(impactGO, 2f);
                    var hitRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
            //        Instantiate(bulletHole, hit.point, hitRotation);
                }
            }
        }
        else
        {
        //    anim.SetBool("Firing", false);
        }
    }
}

