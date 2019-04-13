using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private int daño;
    private IndicadorSalud indicadorSalud;
    public GameObject mensajeFinal; 
    

    public int Daño
    {
        get => daño;
        set
        {
            if (value < 0) value = 0;
            if (value > 100) value = 100;
            daño = value;
            MostrarSalud();
        }
    }

    private void MostrarSalud()
    {
        indicadorSalud.Actualizar(Daño);
    }

    // Start is called before the first frame update
    void Start()
    {
        HealtAndShield.onPlayerDeath += death;
        mensajeFinal.SetActive(false);
        //indicadorSalud = FindObjectOfType<IndicadorSalud>();
        //MostrarSalud();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("escape"))
        {
            //Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            ReloadLevel();
        }
    }

    public void ComprobarVictoria()
    {
        if (indicadorSalud.VidaRestante <= 0)
        {
            mensajeFinal.SetActive(true);
        }
    }

    public void death()
    {
        mensajeFinal.SetActive(true);
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
