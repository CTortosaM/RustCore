using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Triggered : MonoBehaviour
{
  
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player"|| other.gameObject.tag=="Enemy")
        {
            openDoor();
           // this.transform.parent.gameObject.layer = 12;
            Destroy(this.transform.parent.gameObject);
            other.transform.position += other.transform.forward*3;
           //transform.parent.transform.parent.transform.parent.GetComponent<LevelBuilder>().surface.BuildNavMesh();
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
