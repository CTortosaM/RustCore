using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;

public class InitialP : MonoBehaviour
{
    [SerializeField] private float shootInterval = 0.2f;
    private float nextPossibleShootTime;
    public int maxAmmoPerMagazine = 12;
    [SerializeField] private int damage = 50; 
    public int bulletsLeft;
    public Text text;
    private bool canShoot;
    private Animator animator;

    [SerializeField] private Camera camera;

    [SerializeField] private float range = 50f;
    [SerializeField] private ParticleSystem muzzleFlash;

    private bool isReloading = false;

 
    public int Damage { get => damage; set => damage = value; }
    public float ShootInterval { get => shootInterval; set => shootInterval = value; }


    // Start is called before the first frame update
    void Start()
    {
        bulletsLeft = maxAmmoPerMagazine;
        text.text = maxAmmoPerMagazine.ToString();
        animator = GetComponent<Animator>();
        canShoot = true;
        nextPossibleShootTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isReloading && canShoot && Time.time >= nextPossibleShootTime)
        {
            if(bulletsLeft > 0)
            {
                nextPossibleShootTime = Time.time + ShootInterval;
                bulletsLeft--;
                Shoot();
            } 

            text.text = bulletsLeft.ToString();
        }


        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(reload());
            bulletsLeft = maxAmmoPerMagazine;
            text.text = bulletsLeft.ToString();
        }
    }

    private void OnEnable()
    {
       
    }

    private IEnumerator reload()
    {
        isReloading = true;
        animator.SetBool("isReloading", true);
        yield return new WaitForSeconds(1);
        isReloading = false;
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
                hit.collider.gameObject.GetComponent<AIEnemigo>().Actualizar(Damage);
            }
        }

    }
}
