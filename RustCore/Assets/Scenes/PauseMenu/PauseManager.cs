using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
   [SerializeField] private bool canPause = false;
    public static bool isPaused = false;
    public bool CanPause { get => canPause; set => canPause = value; }
    public delegate void restartLevel();
    public static event restartLevel HasRestarted;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(Time.timeScale);
        Debug.Log(isPaused);
    }

    private void Awake()
    {
        LevelBuilder.onLevelFinished += enablePause;
    }

    private void enablePause()
    {
        canPause = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause") && canPause)
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
               Pause(); 
            }
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            Restart();
        }
    }


    public void ResumeGame()
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            if (SceneManager.GetSceneAt(i).name == "PauseMenu")
            {
                SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("PauseMenu").buildIndex);
            }
        }
        //Debug.Log("Voy a reanudar el juego");
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        //menu.SetActive(false);
        
       
        Time.timeScale = 1f;
        isPaused = false;
        
    }


    public void Pause()
    {
        isPaused = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        Time.timeScale = 0f;
        SceneManager.LoadScene("PauseMenu", LoadSceneMode.Additive);
        //menu.SetActive(true);
    }

    public void ReturnToMenu()
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            if (SceneManager.GetSceneAt(i).name == "PauseMenu")
            {
                SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("PauseMenu").buildIndex);
            }
        }
        SceneManager.LoadScene("Menu");
    }

    public void Restart()
    {
        string scene = SceneManager.GetActiveScene().name;
        // SceneManager.UnloadSceneAsync("PauseMenu");
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            if (SceneManager.GetSceneAt(i).name == "PauseMenu")
            {
                SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("PauseMenu").buildIndex);
            }
        }
        Resources.UnloadUnusedAssets();
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("Mapa").buildIndex);
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void OpenOptionsMenu()
    {
        SceneManager.LoadSceneAsync("OptionsMenu", LoadSceneMode.Additive);
    }

}
