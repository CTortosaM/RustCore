using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunPickup : MonoBehaviour
{
 
    [SerializeField] private GameObject gun;
    public WeaponManager weaponManager;

    public GameObject Gun { get => gun; set => gun = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            weaponManager.AddWeapon(gun);
            Destroy(gameObject);
        } 
    }
}
