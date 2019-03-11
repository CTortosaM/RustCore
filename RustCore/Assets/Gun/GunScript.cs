using UnityEngine;

public class GunScript : MonoBehaviour
{

    float timer;
    public int puntosImpacto;
    public int range;
    public int fuerzaImpacto;
    public float rafaga; 

    public Camera fpsCam;

    bool disparo = false;


    // public ParticleSystem fogonazo;

    public GameObject efectoImpacto;


   // public Bullet bala;


    // retrocess
    public Transform RetrocesoIni;
    public Transform RetrocesoFin;

    float velocidadRetroceso = 0.5f;



    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("shoot ordered");
            disparo = true;
            timer = rafaga;
          //  Disparo();

        }

        if (Input.GetButtonUp("Fire1"))
        {
            disparo = false;
        }
        
    }


    private void LateUpdate()
    {
        //Debug.Log("shoot ordered received, disparo = " + disparo.ToString());
        timer += Time.deltaTime;

        if (disparo == true && timer >= rafaga)
        {
            Disparo();
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, RetrocesoFin.position, velocidadRetroceso = Time.deltaTime);
                }
        if (timer >= rafaga)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, RetrocesoIni.position, velocidadRetroceso = Time.deltaTime);
        }

    }
    void Disparo()
    {
        timer = 0f;

        RaycastHit hit;

        // fogonazo.Play();

        //Debug.Log(">>>>>>>>>>>>>>bala.Show() will be executed");
       // bala.ShowBullet();

        //Debug.Log(">>>>>>>>>>>>>>bala.Show() has been executed");


        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {

            Debug.Log("hit name " + hit.transform.name);


            Objetivo objetivo = hit.transform.GetComponent<Objetivo>();
            if (objetivo != null)
            {
                objetivo.TakeImpact(puntosImpacto, hit);
                Debug.Log("Impacted Ball");
            }

            if (hit.rigidbody != null)
            {
                //hit.rigidbody.AddForce(-hit.normal * fuerzaImpacto);
            }

            GameObject impactado = Instantiate(efectoImpacto, hit.point, Quaternion.LookRotation(hit.normal));

            Destroy(impactado, 0.25f);
        }
       // bala.HideBullet();
        //Debug.Log(">>>>>>>>>>>>>>bala.Hide() will be executed");
    }

    void Retroceso()
    {

    }

}
