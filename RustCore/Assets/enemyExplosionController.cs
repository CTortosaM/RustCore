using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyExplosionController : MonoBehaviour
{
   private static enemyExplosionController Instance;
    private AudioSource[] explosions;
    
    // Start is called before the first frame update
    void Start()
    {
        explosions = GetComponents<AudioSource>();
       Instance=this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void explode(Transform t)
    {
        Instance.gameObject.transform.position = t.position;
        Instance.explosions[Random.Range(0, 2)].Play();
    }
}
