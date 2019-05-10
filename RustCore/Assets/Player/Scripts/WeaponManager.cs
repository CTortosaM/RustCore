using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour
{
    public List<int> avalaibleWeapons;
    [SerializeField] private Text ammoText;
    private KeyCode[] keyCodes = {
         KeyCode.Alpha1,
         KeyCode.Alpha2,
         KeyCode.Alpha3,
         KeyCode.Alpha4,
         KeyCode.Alpha5,           //Teclas numéricas para el cambio de armas
         KeyCode.Alpha6,
         KeyCode.Alpha7,
         KeyCode.Alpha8,
         KeyCode.Alpha9,
     };

    [SerializeField] private Transform position;
    [SerializeField] public List<GameObject> weapons;
    [SerializeField] private float weaponSwitchDelay = .5f;
    private int index;
    private bool isSwitching;

    public float WeaponSwitchDelay { get => weaponSwitchDelay; set => weaponSwitchDelay = value; }

    // Start is called before the first frame update
    private void Start()
    {
        avalaibleWeapons = new List<int>();
        avalaibleWeapons.Add(0);
        index = 0;
        isSwitching = false;
        EquipWeapon(0);
    }

   

    // Update is called once per frame
    private void Update()
    {

        


        if(Input.GetAxis("Mouse ScrollWheel") > 0f  || Input.GetAxis("ArrowAxis") > 0 && !isSwitching && weapons.Count > 1 && avalaibleWeapons.Count > 1)
        {
            isSwitching = true;

            index++;

            if (index > weapons.Count - 1) index = 0;

            SwitchWeapons(index);
            
           
        }
        else if (Input.GetAxis("Mouse ScrollWheel")<0f || Input.GetAxis("ArrowAxis")<0f && !isSwitching && weapons.Count > 1 && avalaibleWeapons.Count > 1)
        {
            isSwitching = true;

            index--;
            if (index < 0) index = weapons.Count - 1;

            SwitchWeapons(index);
        }

        SwitchWeaponsThroughKeys();
    }


    public void EquipWeapon(int index)
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            weapons[i].SetActive(false);
        }
        while (!avalaibleWeapons.Contains(weapons[index].GetComponent<AmmoCount>().weaponId))
        {
            index++;
            if (index >= weapons.Count) index = 0;
        }
        weapons[index].SetActive(true);
        AmmoCount weaponAmmo = weapons[index].GetComponent<AmmoCount>();
        ammoText.text = weaponAmmo.AmmoLeftInMagazine + " / " + weaponAmmo.TotalAmmo;
    }

    private IEnumerator SwitchCooldown()
    {

        yield return new WaitForSeconds(weaponSwitchDelay);
        isSwitching = false;

    }

    private void SwitchWeapons(int newIndex)
    {
        EquipWeapon(index);
        StartCoroutine(SwitchCooldown());
    }

    public void AddWeapon(int id)
    {
        avalaibleWeapons.Add(id);
       // EquipWeapon(weapons.IndexOf(gun));
    }

    private void SwitchWeaponsThroughKeys()
    {
        for(int i = 0; i < keyCodes.Length; i++)
        {
            if (Input.GetKeyDown(keyCodes[i]))
            {
                
                if ( i>=0 && i < weapons.Count && !isSwitching && i != index)
                {
                    index = i;
                    isSwitching = true;
                    SwitchWeapons(index);
                }
            }
        }
    }

    public AmmoCount getCurrentWeapon()
    {
        return weapons[index].GetComponent<AmmoCount>();
    }
  
}
