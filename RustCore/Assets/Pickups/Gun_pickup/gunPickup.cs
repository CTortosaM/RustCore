using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunPickup : MonoBehaviour
{
    public string gunName = "Escopeta";
    [SerializeField] private float holdPositionX;
    [SerializeField] private float holdPositionY;
    [SerializeField] private float holdPositionZ;

    [SerializeField] private GameObject gun;

    public WeaponManager weaponManager;

    public GameObject Gun { get => gun; set => gun = value; }

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
            weaponManager.AddWeapon(gun, new Vector3(holdPositionX,holdPositionY,holdPositionZ));
            Destroy(gameObject);
        } 
    }
}
