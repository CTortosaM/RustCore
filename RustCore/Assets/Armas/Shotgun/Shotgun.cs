using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;

[RequireComponent(typeof(AmmoCount))]
public class Shotgun : MonoBehaviour
{

    private AmmoCount ammo;
    [SerializeField] private float shootInterval = 1f;
    private float nextPossibleShootTime;
    public int perdigones = 8;
    [SerializeField] private int damage;

    public Text text;
    private bool canShoot;
   // private Animator animator;

    [SerializeField] private Camera camera;

    [SerializeField] private float range = 100f;
    [SerializeField] private ParticleSystem muzzleFlash;
    public Vector3 initialPosition;
    public Quaternion initialRotation;
    private bool isReloading = false;


    public int Damage { get => damage; set => damage = value; }
    public float ShootInterval { get => shootInterval; set => shootInterval = value; }
    [SerializeField] private ParticleSystem metalHit;

    // Start is called before the first frame update
    void Awake()
    {
        initialPosition = transform.localPosition;
        initialRotation = transform.localRotation;
        ammo = GetComponent<AmmoCount>();
        text.text = ammo.AmmoLeftInMagazine.ToString();
     // animator = GetComponent<Animator>();
        canShoot = true;
        nextPossibleShootTime = Time.time;
        //ammo.updateAmmoText();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isReloading)
        {
            transform.localPosition = initialPosition;
            transform.localRotation = initialRotation;
        }
        if (!PauseManager.isPaused)
        {
            if (Input.GetButtonDown("Fire1") || Input.GetAxis("Fire1") > 0 && !isReloading && canShoot && Time.time >= nextPossibleShootTime)
            {
                if (ammo.AmmoLeftInMagazine > 0  && canShoot && Time.time >= nextPossibleShootTime)
                {
                    StopCoroutine(reload());
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
        //animator.SetBool("isReloading", true);
        for (int i = 0; i < 10; i++)
        {
            transform.Rotate(-0.01f, 0, 0, Space.Self);// = Vector3.MoveTowards(transform.position, camera.transform.position, (float)0.5);
            yield return new WaitForSeconds(0.01f);
        }
        for (int i = ammo.AmmoLeftInMagazine; i <= ammo.MaxAmmoPerMagazine; i++)
        {
            ammo.AmmoLeftInMagazine = i;
            text.text = ammo.AmmoLeftInMagazine.ToString();
            //transform.position = Vector3.MoveTowards(transform.localPosition, initialPosition, (float)0.5);
            yield return new WaitForSeconds(0.2f);
        }
        for (int i = 0; i < 10; i++)
        {
            transform.Rotate(0.01f, 0, 0, Space.Self);// = Vector3.MoveTowards(transform.position, camera.transform.position, (float)0.5);
            yield return new WaitForSeconds(0.01f);
        }
        //yield return new WaitForSeconds(1);

        ammo.reload();

        isReloading = false;
        ammo.AmmoLeftInMagazine = ammo.MaxAmmoPerMagazine;
        text.text = ammo.AmmoLeftInMagazine.ToString();
       // animator.SetBool("isReloading", false);
    }


    private void Shoot()
    {
        Vector3 direction = camera.transform.forward;
        RaycastHit hit;

        muzzleFlash.Play();

        if (Physics.Raycast(camera.transform.position, direction, out hit, range))
        {
            
            Debug.DrawRay(camera.transform.position, camera.transform.forward, Color.green, 10.0f, false);
            if (hit.collider.gameObject.tag == "Enemy")
            {
                hit.collider.gameObject.GetComponent<AIEnemigo>().Actualizar(Damage, ammo.weaponId);
            }
        }
        for(int i=0; i<perdigones/2; i++)
        {
            Vector3 direction2 = direction;
            float random = Random.Range(0, 1);
            direction2.x += Mathf.Sin(i * 4 * Mathf.PI / perdigones)/(perdigones*6);//Random.Range(-1, 0);
            direction2.y += Mathf.Cos(i * 4 * Mathf.PI / perdigones)/(perdigones*6);//Random.Range(0, 1);
                                                                     // direction2 = Quaternion.AngleAxis(Random.Range(1f,20f), camera.transform.forward) *direction2;
            RaycastHit hit2;
            
          //  muzzleFlash.Play();

            if (Physics.Raycast(camera.transform.position, direction2, out hit2, range))
            {
                Debug.DrawRay(camera.transform.position, direction2, Color.red,  10.0f,  false);
                if (hit2.collider.gameObject.tag == "Enemy")
                {
                    hit2.collider.gameObject.GetComponent<AIEnemigo>().Actualizar(Damage, ammo.weaponId);
                }

                Instantiate(metalHit, hit2.point, Quaternion.LookRotation(hit2.normal));
            }
        }
        for (int i = 0; i < perdigones / 2; i++)
        {
            Vector3 direction2 = direction;
            float random = Random.Range(0, 1);
            direction2.x += Mathf.Sin(i * 4 * Mathf.PI / perdigones) / (perdigones * 3);//Random.Range(-1, 0);
            direction2.y += Mathf.Cos(i * 4 * Mathf.PI / perdigones) / (perdigones * 3);//Random.Range(0, 1);
                                                                                        // direction2 = Quaternion.AngleAxis(Random.Range(1f,20f), camera.transform.forward) *direction2;
            RaycastHit hit2;

            //  muzzleFlash.Play();

            if (Physics.Raycast(camera.transform.position, direction2, out hit2, range))
            {
                Debug.DrawRay(camera.transform.position, direction2, Color.red, 10.0f, false);
                if (hit2.collider.gameObject.tag == "Enemy")
                {
                    hit2.collider.gameObject.GetComponent<AIEnemigo>().Actualizar(Damage, ammo.weaponId);
                }
                Instantiate(metalHit, hit2.point, Quaternion.LookRotation(hit2.normal));
            }
        }
      //  transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.5f);
        StartCoroutine(shootAction());
    }
  
    private IEnumerator shootAction()
    {
        for (int i = 0; i < 10; i++)
        {
            transform.Rotate(-0.01f, 0, 0, Space.Self);// = Vector3.MoveTowards(transform.position, camera.transform.position, (float)0.5);
            yield return new WaitForSeconds(0.007f);
        }
        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i < 10; i++)
        {
            transform.Rotate(0.01f, 0, 0, Space.Self);// = Vector3.MoveTowards(transform.position, camera.transform.position, (float)0.5);
            yield return new WaitForSeconds(0.007f);
        }
       
    }
    void OnEnable()
    {
        ammo.updateAmmoText();
    }
}

