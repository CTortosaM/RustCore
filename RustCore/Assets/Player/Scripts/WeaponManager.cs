using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{

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
    private int index = 0;
    private bool isSwitching = false;

    public float WeaponSwitchDelay { get => weaponSwitchDelay; set => weaponSwitchDelay = value; }

    // Start is called before the first frame update
    private void Start()
    {
        EquipWeapon(0);
    }

    // Update is called once per frame
    private void Update()
    {
        if(Input.GetAxis("Mouse ScrollWheel") > 0f && !isSwitching)
        {
            isSwitching = true;

            index++;

            if (index > weapons.Count - 1) index = 0;

            SwitchWeapons(index);
            
           
        }
        else if (Input.GetAxis("Mouse ScrollWheel")<0f && !isSwitching)
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

        weapons[index].SetActive(true);
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

    public void AddWeapon(GameObject gun, Vector3 holdPosition)
    {
        weapons.Add(gun);
        gun.transform.SetParent(position,false);
        gun.transform.localPosition = holdPosition;
        gun.transform.localRotation = new Quaternion(0, 0, 0, 0);
        EquipWeapon(weapons.IndexOf(gun));
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
}
