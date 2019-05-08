using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunPickup : MonoBehaviour
{
    public string gunName = "Escopeta";
    public int weaponID = 2;
    


    public WeaponManager weaponManager;

   

    // Start is called before the first frame update
    void Start()
    {
        weaponManager = FindObjectOfType<WeaponManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            weaponManager.AddWeapon(weaponID);
           
            Destroy(gameObject);
        } 
    }
}
