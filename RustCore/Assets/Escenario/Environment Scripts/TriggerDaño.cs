using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDaño : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<HealtAndShield>().TakeDamage(other.gameObject.GetComponent<HealtAndShield>().Shield, gameObject.transform.position);
            other.gameObject.GetComponent<HealtAndShield>().TakeDamage(30, gameObject.transform.position);
        }
    }
}
