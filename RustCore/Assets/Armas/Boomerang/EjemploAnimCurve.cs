using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EjemploAnimCurve : MonoBehaviour
{
    private AmmoCount ammo;
    public AnimationCurve curvey;
    public AnimationCurve curvex;
    public float longitude = 50;
    public float hitRadius = (float)0.5;
    public GameObject player;
    public bool run = true;
    private bool rotationCalculated = false;
    private bool hasArrived = false;
    float t = (float)0.5;
    Vector3 playerForward;
    public bool melee = false;
    public int Damage = 80;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    // Start is called before the first frame update
    void Awake()
    {
        //gameObject.GetComponent<Rigidbody>().useGravity = false;
        curvey.preWrapMode = WrapMode.PingPong;
        curvey.postWrapMode = WrapMode.PingPong;
        curvex.preWrapMode = WrapMode.PingPong;
        curvex.postWrapMode = WrapMode.PingPong;
        originalPosition = transform.localPosition;
        originalRotation = transform.localRotation;
        ammo = GetComponent<AmmoCount>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            if (!melee&&!run)
            {
                for(int i=0; i<10; i++)
                {
                    transform.position = Vector3.MoveTowards(transform.position, -player.transform.forward, .5f);
                }
                run = true;
            }

        }
        else if (Input.GetKey(KeyCode.Mouse0))
        {
            if (!run&&!melee)
            {
                transform.localPosition = originalPosition;
                transform.localRotation = originalRotation;

                melee = true;
            }
        }

        if (run)
        {
            transform.Rotate(0, 0, -30, Space.Self);
            if (!rotationCalculated)
            {
               
                playerForward = player.transform.forward;
                transform.position = player.transform.position;
                
                rotationCalculated = true;
            }



            if (t >= (1.5 / 100) * longitude)
            {
                t = 0;


               
                hasArrived = true;
            }
            if (!hasArrived)
            {
                transform.position = Vector3.MoveTowards(transform.position, longitude * playerForward, (float)1.5);
                t += Time.deltaTime;
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, (float)1.5);
            }
            if (transform.position == player.transform.position)
            {
                hasArrived = false;
                rotationCalculated = false;
                run = false;
                transform.localPosition = originalPosition;
                transform.localRotation = originalRotation;
            }

        }
        else
        {

            if (melee)
            {

                transform.position = new Vector3(transform.position.x, -((curvex.Evaluate(t - 0.5f) * hitRadius) - hitRadius / 2) -0.5f, ((curvey.Evaluate(t) * hitRadius)) + 1);
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
    void OnCollisionEnter(Collision col)
    {

        if (col.gameObject.tag == "Player")
        {
           //gameObject.GetComponent<Rigidbody>().useGravity = false;
            hasArrived = false;
            rotationCalculated = false;
            run = false;
            transform.localPosition = originalPosition;
            transform.localRotation = originalRotation;
        }
        if (melee || run)
        {
            Debug.Log("le di");
            hasArrived = true;
            if (col.gameObject.tag == "Enemy")
            {
                
                 col.gameObject.GetComponent<AIEnemigo>().Actualizar(Damage);
            }
            else
            {
                //gameObject.GetComponent<Rigidbody>().useGravity = true;
            }
        }
        else
        {
           
               // gameObject.GetComponent<Rigidbody>().useGravity = false;
                transform.localPosition = originalPosition;
                transform.localRotation = originalRotation;
            
        }
    }
    
}
