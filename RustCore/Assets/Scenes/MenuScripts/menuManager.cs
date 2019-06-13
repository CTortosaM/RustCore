using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuManager: MonoBehaviour
{
    [SerializeField] GameObject postProcessing;
    [SerializeField] GameObject ExitConfirm;
    [SerializeField] GameObject audio;

    // Start is called before the first frame update
    void Start()
    {
        resetTimesPlayed();
        if (!FindObjectOfType<ChangePostSettings>())
        {
            Instantiate(postProcessing);
        }
        if (!FindObjectOfType<AudioControllerClass>())
        {
            Instantiate(audio);
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
        string save = SaveSettings.Load(SaveSettings.SAVE_FOLDER_Savestate + "saveState.txt");

        if (!save.Equals(null))
        {
            Savestate state = JsonUtility.FromJson<Savestate>(save);
            if (!state.isOnGame)
            {
                state.isOnGame = false;
            }
            SaveSettings.Save(SaveSettings.SAVE_FOLDER_Savestate + "saveState.txt", JsonUtility.ToJson(state));
        }
        Application.Quit();
    }

    public void LoadLevel()
    {
      
        SceneManager.LoadSceneAsync("Tutorial");
        AudioControllerClass.setSelected(0);
        AudioControllerClass.selected.Play();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        PauseManager.isPaused = false;
    }

    public void OpenSettings()
    {
        SceneManager.LoadSceneAsync("OptionsMenu", LoadSceneMode.Additive);
    }

    private void resetTimesPlayed()
    {
        string save = SaveSettings.Load(SaveSettings.SAVE_FOLDER_Savestate + "saveState.txt");

        if (!save.Equals(null))
        {
            Savestate state = JsonUtility.FromJson<Savestate>(save);
            if (!state.isOnGame)
            {
                state.timesPlayed = 1;
                state.isOnGame = true;
            }
            SaveSettings.Save(SaveSettings.SAVE_FOLDER_Savestate + "saveState.txt", JsonUtility.ToJson(state));
        }
    }
}
