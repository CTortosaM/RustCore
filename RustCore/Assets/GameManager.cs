using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private int daño;
    private IndicadorSalud indicadorSalud;
    public GameObject mensajeFinal;
    public static float contBoomerang;
    public static float contkills;
    public static float totalKills;
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
        string save = SaveSettings.Load(SaveSettings.SAVE_FOLDER_Savestate + "saveState.txt");
        if (!save.Equals(null))
        {
            Savestate state = JsonUtility.FromJson<Savestate>(save);
            if (state.timesPlayed != 0)
            {
                contBoomerang += (float)1 / state.timesPlayed;
            }
            else
            {
                contBoomerang++;
            }
        }
        else
        {
            contBoomerang++;
        }
    }
    public void enemyKill()
    {
        ticktock = 0;
        string save = SaveSettings.Load(SaveSettings.SAVE_FOLDER_Savestate + "saveState.txt");
        if (!save.Equals(null))
        {
            Savestate state = JsonUtility.FromJson<Savestate>(save);
            if (state.timesPlayed != 0)
            {
                contkills += (float)1 / state.timesPlayed;
                totalKills += (float)1 / state.timesPlayed;
                Debug.Log(totalKills);
            }
            else
            {
                contkills++;
                totalKills++;
            }
        }
        else
        {
            contkills++;
            totalKills++;
        }
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
