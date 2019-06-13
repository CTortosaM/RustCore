using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triggered : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
       // Debug.Log(other.gameObject.name);
        if (other.gameObject.tag == "Player")
        {
            openDoor();
            StartCoroutine(openDoor());
         
        }
    }
    IEnumerator openDoor(){
        PlayerLook.puertaAbrir.Play();
        for(int i=0; i<10; i++)
        {
            this.transform.parent.transform.position = Vector3.MoveTowards(transform.position, transform.right * 2, (float)1);
            yield return new WaitForSeconds((float)0.1);

        }
        Destroy(this.transform.parent.gameObject);
    }
}
