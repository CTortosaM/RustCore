using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{

    [SerializeField] private float SwayAmmount = 0.02f;
    [SerializeField] private float MaxSwayAmmount = 0.09f;
    [SerializeField] private float SwaySmooth = 3f;
    Vector3 def;
    Vector3 defAth;
    Vector3 euler;

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
        def = transform.localPosition;
        euler = transform.localEulerAngles;
        index = 0;
        isSwitching = false;
        EquipWeapon(0);
    }

    float _smooth;

    // Update is called once per frame
    private void Update()
    {

        Sway();


        if(Input.GetAxis("Mouse ScrollWheel") > 0f && !isSwitching && weapons.Count > 1)
        {
            isSwitching = true;

            index++;

            if (index > weapons.Count - 1) index = 0;

            SwitchWeapons(index);
            
           
        }
        else if (Input.GetAxis("Mouse ScrollWheel")<0f && !isSwitching && weapons.Count > 1)
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

    private void Sway()
    {
        _smooth = SwaySmooth;
        float factorX = -Input.GetAxis("Mouse X") * SwayAmmount;
        float factorY = -Input.GetAxis("Mouse Y") * SwayAmmount;

        if (factorX > MaxSwayAmmount || factorX < -MaxSwayAmmount) factorX = MaxSwayAmmount;
        if (factorY > MaxSwayAmmount || factorY < -MaxSwayAmmount) factorY = MaxSwayAmmount;

        Vector3 final = new Vector3(def.x + factorX, def.y + factorY, def.z);
        transform.localPosition = Vector3.Lerp(transform.localPosition, final, Time.deltaTime * _smooth);
    }
}
