using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyShotSound : MonoBehaviour
{
    private static enemyShotSound Instance;
    private AudioSource pew;

    // Start is called before the first frame update
    void Start()
    {
        pew = GetComponent<AudioSource>();
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public static void pewpew(Transform t)
    {
        Instance.gameObject.transform.position = t.position;
        Instance.pew.Play();
    }
}
