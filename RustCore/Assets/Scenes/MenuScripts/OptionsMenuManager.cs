using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsMenuManager : MonoBehaviour
{
    [SerializeField] private Button goBackButton;
    [SerializeField] private Button closeMenuButton;
    [SerializeField] private List<GameObject> menus;
    // Start is called before the first frame update



    public void GoBack()
    {
        foreach(GameObject menu in menus)
        {
            menu.SetActive(false);
        }
        menus[0].SetActive(true);
        goBackButton.gameObject.SetActive(false);
        closeMenuButton.gameObject.SetActive(true);
    }

    public void CloseMenu()
    {
        SceneManager.UnloadSceneAsync("OptionsMenu");
    }

    public void enterMenu(int index)
    {
        foreach(GameObject menu in menus)
        {
            menu.SetActive(false);
        }
        menus[index].SetActive(true);
        goBackButton.gameObject.SetActive(true);
        closeMenuButton.gameObject.SetActive(false);
    }
}
