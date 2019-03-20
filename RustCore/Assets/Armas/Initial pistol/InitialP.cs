using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitialP : MonoBehaviour
{

    public int maxAmmoPerMagazine = 12;
    public int bulletsLeft;
    private Text text;

    // Start is called before the first frame update
    void Start()
    {
        bulletsLeft = maxAmmoPerMagazine;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
