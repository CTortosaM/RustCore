using System.Collections;
using UnityEngine;

public class Doorway : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Ray ray = new Ray(transform.position, transform.rotation * Vector3.forward);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(ray);
    }
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.tag == "Enemy")
        {
            StartCoroutine(openDoor());
            Destroy(this.transform.parent.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("t; "+other.gameObject.name);
        if (other.gameObject.tag == "Enemy")
        {
            StartCoroutine(openDoor());
            Destroy(this.transform.parent.gameObject);
        }
    }
    IEnumerator openDoor()
    {
        for (int i = 0; i < 10; i++)
        {
            this.transform.parent.transform.position = Vector3.MoveTowards(transform.parent.transform.position, transform.parent.transform.right * 2, (float)0.3);
            yield return new WaitForSeconds((float)0.1);
        }
    }
}
