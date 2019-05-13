using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triggered : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player"|| other.gameObject.tag=="Enemy")
        {
            openDoor();
            Destroy(this.transform.parent.gameObject);
        }
    }
    IEnumerator openDoor(){
        this.transform.parent.transform.position = Vector3.MoveTowards(transform.parent.transform.position, transform.parent.transform.right * 2, (float)0.25);
        yield return new WaitForSeconds((float)0.1);
        this.transform.parent.transform.position = Vector3.MoveTowards(transform.parent.transform.position, transform.parent.transform.right * 2, (float)0.25);
        yield return new WaitForSeconds((float)0.1);
        this.transform.parent.transform.position = Vector3.MoveTowards(transform.parent.transform.position, transform.parent.transform.right * 2, (float)0.25);
        yield return new WaitForSeconds((float)0.1);
        this.transform.parent.transform.position = Vector3.MoveTowards(transform.parent.transform.position, transform.parent.transform.right * 2, (float)0.25);
        this.transform.parent.transform.position = Vector3.MoveTowards(transform.parent.transform.position, transform.parent.transform.right * 2, (float)0.25);
        yield return new WaitForSeconds((float)0.1);
        this.transform.parent.transform.position = Vector3.MoveTowards(transform.parent.transform.position, transform.parent.transform.right * 2, (float)0.25);
        yield return new WaitForSeconds((float)0.1);
        this.transform.parent.transform.position = Vector3.MoveTowards(transform.parent.transform.position, transform.parent.transform.right * 2, (float)0.25);
        yield return new WaitForSeconds((float)0.1);
        this.transform.parent.transform.position = Vector3.MoveTowards(transform.parent.transform.position, transform.parent.transform.right * 2, (float)0.25);

    }
}
