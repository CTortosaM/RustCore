using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private int daño;
    private IndicadorSalud indicadorSalud;
    public GameObject mensajeFinal;
    public static int contBoomerang;
    public static int contkills;
    public static int totalKills;
    public static bool isBossDead;
    public static float ticktock;
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

        AIEnemigo.onekill += enemyKill;
        AIEnemigo.boom += boomerangDeath;
        PauseManager.HasRestarted += ReloadLevel;
        HealtAndShield.onPlayerDeath += death;
        mensajeFinal.SetActive(false);
        //indicadorSalud = FindObjectOfType<IndicadorSalud>();
        //MostrarSalud();
    }

    // Update is called once per frame
    void Update()
    {
        ticktock += Time.deltaTime;
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

    public void boomerangDeath()
    {
        contBoomerang++;
    }
    public void enemyKill()
    {
        contkills++;
        totalKills++;
    }
    public void death()
    {
        if(mensajeFinal) mensajeFinal.SetActive(true);
    }

    public void ReloadLevel()
    {
        Resources.UnloadUnusedAssets();
        SceneManager.LoadScene("Mapa", LoadSceneMode.Single);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        PauseManager.isPaused = false;
    }

    
}
