﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    private bool canPause = true;
    public static bool isPaused = false;
    public bool CanPause { get => canPause; set => canPause = value; }

    // Start is called before the first frame update
    void Start()
    {
        
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
    }


    public void ResumeGame()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.UnloadSceneAsync("PauseMenu");
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
    }
}
