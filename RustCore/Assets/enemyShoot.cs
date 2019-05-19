using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class enemyShoot : MonoBehaviour
    {
        
       
        public float longitude = 50;
    public Transform Player;
    public GameObject player;
        public bool run = false;
     
        private bool hasArrived = false;
        private float t = (float)0.5;
        private Vector3 Forward;
        private Vector3 Position;

    public float velocidad=0.5f;
        public int Damage = 80;
        public Vector3 originalPosition;
        
        private Transform parentTransform;
   public bool aux=true;
    
        private float i;
       

        // Start is called before the first frame update
        void Awake()
        {
          

     Player= GameObject.FindGameObjectWithTag("Player").transform;

        transform.localPosition = originalPosition;
         
           
            parentTransform = transform.parent;


        }

        // Update is called once per frame
        void FixedUpdate()
        {


        if (aux)
        {
            i = 0.0f;
            hasArrived = false;

            Forward = Player.position;
            Position = player.transform.position;


            transform.SetParent(null, true);
            aux = false;
            run = true;
        }
        if (run)
            {

           // parentTransform.rotation = Quaternion.RotateTowards(;
                transform.Rotate(0, 0, -30, Space.Self);




                if (t >= 4*(1.5 / 100) * longitude)
                {
                    t = 0;



                    hasArrived = true;
                }
                if (!hasArrived)
                {
                    // Vector3 vDirection = playerForward*longitude - transform.position;
                    //vDirection = vDirection.normalized;
                   // i += Time.deltaTime * 2f;

                transform.position = Vector3.MoveTowards(transform.position, Forward,velocidad);// transform.position + i*Forward;//i * Vector3.Lerp(Forward, Player.position,1);


                    t += Time.deltaTime;
                   // Debug.Log(Forward.x + "," + Forward.y + "," + Forward.z);
                }
                else
                {

                run = false;

                       
                     

                transform.SetParent(parentTransform, true);
                 transform.localPosition = originalPosition;
                aux = true;
            }


            }
           

        }

    private void OnTriggerEnter(Collider col)
        {


        Debug.Log(col.gameObject.name);
            
            if ( run)
            {


                if (col.gameObject.tag == "Player")
                {
                    Debug.Log("te dio");
                col.gameObject.GetComponent<HealtAndShield>().TakeDamage(Damage, parentTransform.position);
                // hasArrived = true;
            }
            else
            {

                transform.SetParent(parentTransform, true);
                transform.localPosition = originalPosition;
            }

        }
        else
        {
            transform.SetParent(parentTransform, true);
            transform.localPosition = originalPosition;
        }

        }
    private void OnCollisionEnter(Collision col)
    {
        if (run)
        {


            if (col.gameObject.tag == "Player")
            {
                Debug.Log("te dio");
                col.gameObject.GetComponent<HealtAndShield>().TakeDamage(Damage, col.transform.position);
                // hasArrived = true;
            }
            else
            {

                transform.SetParent(parentTransform, true);
                transform.localPosition = originalPosition;
            }

        }
    }


}



