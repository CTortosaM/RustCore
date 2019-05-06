using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    private bool canPause = false;
    public static bool isPaused = false;
    public bool CanPause { get => canPause; set => canPause = value; }
    public delegate void restartLevel();
    public static event restartLevel HasRestarted;

    // Start is called before the first frame update
    void Start()
    {

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
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        //menu.SetActive(false);
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("PauseMenu"));
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
        SceneManager.LoadScene("Menu");
    }

    public void Restart()
    {
       // SceneManager.UnloadSceneAsync("PauseMenu");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
