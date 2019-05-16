using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SigueObjetivo : MonoBehaviour
{
    public GameObject aSeguir;

    public float movementSpeed; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            transform.LookAt(aSeguir.transform);
            transform.position += transform.forward * movementSpeed * Time.deltaTime;
    }
}
