using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;

[RequireComponent(typeof(AmmoCount))]
public class InitialP : MonoBehaviour
{

    private AmmoCount ammo;
    [SerializeField] private float shootInterval = 0.2f;
    private float nextPossibleShootTime;

    [SerializeField] private ParticleSystem metalCollision;

    [SerializeField] private int damage = 50; 
  
    public Text text;
    private bool canShoot;
    private Animator animator;

    [SerializeField] private Camera camera;

    [SerializeField] private float range = 100f;
    [SerializeField] private ParticleSystem muzzleFlash;

    private bool isReloading = false;

 
    public int Damage { get => damage; set => damage = value; }
    public float ShootInterval { get => shootInterval; set => shootInterval = value; }


    // Start is called before the first frame update
    void Awake()
    {
        ammo = GetComponent<AmmoCount>();
        text.text = ammo.AmmoLeftInMagazine.ToString();
        animator = GetComponent<Animator>();
        canShoot = true;
        nextPossibleShootTime = Time.time;
        //ammo.updateAmmoText();
        LevelBuilder.onLevelFinished += ResetAnimation;
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
                    ammo.AmmoLeftInMagazine--;
                    ammo.updateAmmoText();
                    Shoot();
                }

                text.text = ammo.AmmoLeftInMagazine.ToString();
            }


            if (Input.GetButtonDown("Reload") && ammo.AmmoLeftInMagazine < ammo.MaxAmmoPerMagazine && ammo.TotalAmmo != 0)
            {
                StartCoroutine(reload());

            }
        }
    }


    private IEnumerator reload()
    {
        isReloading = true;
        animator.SetBool("isReloading", true);
        
        yield return new WaitForSeconds(1);

        ammo.reload();

        isReloading = false;
        ammo.AmmoLeftInMagazine = 12;
        text.text = ammo.AmmoLeftInMagazine.ToString();
        animator.SetBool("isReloading", false);
    }


    private void Shoot()
    {
        RaycastHit hit;

        muzzleFlash.Play();

        if(Physics.Raycast(camera.transform.position,camera.transform.forward, out hit, range))
        {
            if(hit.collider.gameObject.tag == "Enemy")
            {
                AIEnemigo enemigo = hit.collider.gameObject.GetComponent<AIEnemigo>();
                if(enemigo.Estado != AIEnemigo.EstadosPatrulla.Muerte)
                {
                    enemigo.Actualizar(Damage);
                    
                }
            }
            Instantiate(metalCollision, hit.point, Quaternion.LookRotation(hit.normal));
        }

    }

    void OnEnable()
    {
        //ammo.updateAmmoText();
    }

    private void ResetAnimation()
    {
        if(animator)animator.Rebind();
    }
}
