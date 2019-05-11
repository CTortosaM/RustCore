using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickup_ammo : MonoBehaviour
{
    [SerializeField] private string targetTag = "Player";
    private BoxCollider boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == targetTag)
        {
            other.GetComponentInChildren<WeaponManager>().getCurrentWeapon().rechargeAllAmmo();
            Destroy(this.gameObject);
        }
    }

}
