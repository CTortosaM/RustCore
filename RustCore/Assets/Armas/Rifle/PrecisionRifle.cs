using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AmmoCount))]
public class PrecisionRifle : MonoBehaviour
{

    private AmmoCount ammo;
    [SerializeField] private float shootInterval = 0.2f;
    private float nextPossibleShootTime;

    [SerializeField] private int damage = 50;

    
    private bool canShoot;
    private Animator animator;

    [SerializeField] private Camera camera;

    [SerializeField] private float range = 100f;
    //[SerializeField] private ParticleSystem muzzleFlash;

    private bool isReloading = false;


    public int Damage { get => damage; set => damage = value; }
    public float ShootInterval { get => shootInterval; set => shootInterval = value; }

    [SerializeField] private ParticleSystem metalHit;


    private void Awake()
    {
        ammo = GetComponent<AmmoCount>();
        canShoot = true;
        nextPossibleShootTime = Time.time;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (!PauseManager.isPaused)
        {
            if (Input.GetButtonDown("Fire1") || Input.GetAxis("Fire1") > 0 && !isReloading && canShoot && Time.time >= nextPossibleShootTime)
            {
                if (ammo.AmmoLeftInMagazine > 0)
                {
                    nextPossibleShootTime = Time.time + ShootInterval;
                    Shoot();
                }

            }


            if (Input.GetButtonDown("Reload") && ammo.AmmoLeftInMagazine < ammo.MaxAmmoPerMagazine && ammo.TotalAmmo != 0)
            {
                StartCoroutine(reload());

            }
        }

      
    }

    private void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, range))
        {
            if (hit.collider.gameObject.tag == "Enemy")
            {
                hit.collider.gameObject.GetComponent<AIEnemigo>().Actualizar(Damage,ammo.weaponId);
            } else
            {
                ammo.AmmoLeftInMagazine--;
                ammo.updateAmmoText();
            }

            Instantiate(metalHit, hit.point, Quaternion.LookRotation(hit.normal));
        }
        else
        {
            ammo.AmmoLeftInMagazine--;
            ammo.updateAmmoText();
        }
    }

    private IEnumerator reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(1);
        ammo.reload();
        isReloading = false;
    }
}
