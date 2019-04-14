using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    private bool canPause = true;
    public static bool isPaused = false;
    public bool CanPause { get => canPause; set => canPause = value; }

    public GameObject menu;

    public delegate void restartLevel();
    public static event restartLevel HasRestarted;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause") && canPause && !HealtAndShield.IsDead)
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
    }


    public void ResumeGame()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        //menu.SetActive(false);
        if (GameObject.Find("PauseMenu"))
        {
            SceneManager.UnloadScene("PauseMenu");
        }
        
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
        HasRestarted();
    }

}
