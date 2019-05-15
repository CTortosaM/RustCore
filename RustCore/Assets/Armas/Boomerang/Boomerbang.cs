using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerbang : MonoBehaviour
{
    private AmmoCount ammo;
    public AnimationCurve curvey;
    public AnimationCurve curvex;
    public float longitude = 50;
    public float hitRadius = (float)0.5;
    public GameObject player;
    private bool run = true;
    private bool rotationCalculated = false;
    private bool hasArrived = false;
    private float t = (float)0.5;
    private Vector3 playerForward;
    private Vector3 playerPosition;
    public int waiting=10;
    private bool melee = false;
    public int Damage = 80;
    public Vector3 originalPosition;
    public Quaternion originalRotation;
    private Transform parentTransform;
    bool isEquiped;
    private float i;
    private GameObject clone;
    // Start is called before the first frame update
    void Awake()
    {
        isEquiped = true;
       
        gameObject.GetComponent<Rigidbody>().useGravity = false;
        curvey.preWrapMode = WrapMode.PingPong;
        curvey.postWrapMode = WrapMode.PingPong;
        curvex.preWrapMode = WrapMode.PingPong;
        curvex.postWrapMode = WrapMode.PingPong;
        transform.localPosition =originalPosition ;
       transform.localRotation =originalRotation;
        ammo = GetComponent<AmmoCount>();
        parentTransform = transform.parent;
        
     
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (Input.GetButtonDown("Fire2"))
        {
            if (isEquiped)
            {
                if (!melee && !run)
                {
                    i = 0.0f;
                    hasArrived = false;

                    playerForward = player.transform.forward;
                    playerPosition = player.transform.position;
                    //transform.position = player.transform.forward;

                    /* RaycastHit hit;


                    //player.transform.Rotate(0, 0, -30, Space.Self);
                    if (Physics.Raycast(player.transform.position, playerForward*longitude, out hit, 50))
                     {
                         if (hit.collider.gameObject.tag == "Enemy")
                         {
                             hit.collider.gameObject.GetComponent<AIEnemigo>().Actualizar(Damage);
                         }
                     }*/
                    //  transform.position = Vector3.MoveTowards(player.transform.position, longitude * playerForward, (float)1.5);

                    transform.SetParent(null, true);
                    run = true;
                }
            }
        }
        else if (Input.GetButtonDown("Fire1") || Input.GetAxis("Fire1") > 0)
        {
            if (isEquiped)
            {
                if (!run && !melee)
                {
                    transform.SetParent(parentTransform, true);
                    playerForward = player.transform.forward;
                    transform.localPosition = originalPosition;
                    transform.localRotation = originalRotation;

                    melee = true;
                }
            }
        }

        if (run)
        {
           
            transform.Rotate(0, 0, -30, Space.Self);
           



            if (t >= (1.5 / 100) * longitude)
            {
                t = 0;


               
                hasArrived = true;
            }
            if (!hasArrived)
            {
                // Vector3 vDirection = playerForward*longitude - transform.position;
                //vDirection = vDirection.normalized;
                i += Time.deltaTime * 2f;
                transform.position = transform.position +i*playerForward;
               
           
                t += Time.deltaTime;
                Debug.Log(playerForward.x + "," + playerForward.y + "," + playerForward.z);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, (float)1.5);
                if (Vector3.Distance(transform.position, player.transform.position) < 1)
                {
                    transform.SetParent(parentTransform, true);
                    hasArrived = false;

                    run = false;

                    transform.localPosition = originalPosition;
                    transform.localRotation = originalRotation;
                }
            }
           

        }
        else
        {

            if (melee)
            {

                transform.localPosition = new Vector3(transform.localPosition.x, -((curvex.Evaluate(t - 0.5f) * hitRadius) - hitRadius / 2)-0.3f , ((curvey.Evaluate(t) * hitRadius)) );
                t += 0.1f;
               
                if (t >= 2)
                {
                    transform.localPosition = originalPosition;
                    transform.localRotation = originalRotation;
                    melee = false;
                    t = 0f;
                }


            }
          
        }

    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player"|| col.gameObject.transform.parent.tag == "Player")
        {

            StopCoroutine("wait");
            if (!run){
                isEquiped = true;
                transform.SetParent(parentTransform, true);
                gameObject.GetComponent<Rigidbody>().useGravity = false;
                hasArrived = false;
                rotationCalculated = false;
                run = false;
                transform.localPosition = originalPosition;
                transform.localRotation = originalRotation;
            }
        }
        if (melee || run)
        {
           
          
            if (col.gameObject.tag == "Enemy")
            {
                 Debug.Log("le di");
                col.gameObject.GetComponent<AIEnemigo>().Actualizar(Damage);
                hasArrived = true;
            }
            else
            {
                if (!(col.gameObject.tag == "Player"))
                {
                    if (run)
                    {
                        //transform.position = transform.position - i * playerForward;
                        //Instantiate(this.gameObject, transform.position, transform.rotation);
                       // this.gameObject.SetActive(false);
                        //---------------
                        hasArrived = false;
                        rotationCalculated = false;
                        run = false;
                        isEquiped = false;
                        gameObject.GetComponent<Rigidbody>().useGravity = true;
                        
                        StartCoroutine("wait");
                        Debug.Log(col.gameObject.name);
                    }
                }
            }
        }
        else
        {

             gameObject.GetComponent<Rigidbody>().useGravity = false;
            transform.localPosition = originalPosition;
            transform.localRotation = originalRotation;

        }
    }
  
    IEnumerator wait()
    {
        yield return new WaitForSeconds(waiting);
      Debug.Log("Ya puedes");
        isEquiped = true;
        transform.SetParent(parentTransform, true);
        gameObject.GetComponent<Rigidbody>().useGravity = false;
        hasArrived = false;
        rotationCalculated = false;
        run = false;
        transform.localPosition = originalPosition;
        transform.localRotation = originalRotation;
    }
}
