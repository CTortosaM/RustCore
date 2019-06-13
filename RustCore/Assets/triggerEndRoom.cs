using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerEndRoom : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            okComputer.writeText("Now kill the <color=red>big guy</color> to enable the exits!");
        }
    }
}
