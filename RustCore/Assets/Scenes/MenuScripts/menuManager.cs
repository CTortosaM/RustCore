using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuManager: MonoBehaviour
{

    [SerializeField] GameObject ExitConfirm;

    // Start is called before the first frame update
    void Start()
    {
        
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
        SceneManager.LoadSceneAsync("Mapa");
    }


}
