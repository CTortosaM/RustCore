using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuManager: MonoBehaviour
{
    [SerializeField] GameObject postProcessing;
    [SerializeField] GameObject ExitConfirm;

    // Start is called before the first frame update
    void Start()
    {
        if (!FindObjectOfType<ChangePostSettings>())
        {
            Instantiate(postProcessing);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void toggleExitConf(bool show)
    {
        ExitConfirm.SetActive(show);
    }


    public void closeGame()
    {
        Application.Quit();
    }

    public void LoadLevel()
    {
        SceneManager.LoadSceneAsync("SalaHub");
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        PauseManager.isPaused = false;
    }

    public void OpenSettings()
    {
        SceneManager.LoadSceneAsync("OptionsMenu", LoadSceneMode.Additive);
    }


}
