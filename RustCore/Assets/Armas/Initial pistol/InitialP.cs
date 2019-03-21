using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;

public class InitialP : MonoBehaviour
{

    public int maxAmmoPerMagazine = 12;
    public int bulletsLeft;
    public Text text;
    private bool canShoot;
    public Animator animator;
    

    // Start is called before the first frame update
    void Start()
    {
        bulletsLeft = maxAmmoPerMagazine;
        text.text = maxAmmoPerMagazine.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(bulletsLeft > 0)
            {
                bulletsLeft--;
            } 

            text.text = bulletsLeft.ToString();
        }


        if (Input.GetKeyDown(KeyCode.R))
        {
            bulletsLeft = maxAmmoPerMagazine;
            text.text = bulletsLeft.ToString();
        }
    }

    private void OnEnable()
    {
       
    }
}
