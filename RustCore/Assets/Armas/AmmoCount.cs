using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCount : MonoBehaviour
{
    [SerializeField] private int maxTotalAmmo = 200;
    [SerializeField] private int totalAmmo = 200;
    [SerializeField] private int ammoLeftInMagazine = 12;
    [SerializeField] private int maxAmmoPerMagazine = 12;

    public int TotalAmmo { get => totalAmmo; set => totalAmmo = value; }
    public int AmmoLeftInMagazine { get => ammoLeftInMagazine; set => ammoLeftInMagazine = value; }
    public int MaxAmmoPerMagazine { get => maxAmmoPerMagazine; set => maxAmmoPerMagazine = value; }
    public int MaxTotalAmmo { get => maxTotalAmmo; set => maxTotalAmmo = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void reload()
    {
        if (MaxAmmoPerMagazine > TotalAmmo)
        {
            int difference = MaxAmmoPerMagazine - AmmoLeftInMagazine;
            if (difference > TotalAmmo)
            {
                AmmoLeftInMagazine += TotalAmmo;
                TotalAmmo = 0;
            }
            else
            {
                AmmoLeftInMagazine = MaxAmmoPerMagazine;
                TotalAmmo -= difference;
            }
        }
        else
        {
            TotalAmmo -= MaxAmmoPerMagazine - AmmoLeftInMagazine;
            AmmoLeftInMagazine = MaxAmmoPerMagazine;

        }
    }
}
