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
    [SerializeField] private Text[] audioValues;
    [SerializeField] private Slider[] audioSliders;
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

    public void updateAudioValuesText(int which)
    {
        audioValues[which].text = audioSliders[which].value.ToString();
    }


    public void saveAudioSettings()
    {
        SaveSettings.Init();
        AudioSettings audio = new AudioSettings
        {
            masterVolume = Mathf.RoundToInt(audioSliders[0].value),
            musicVolume = Mathf.RoundToInt(audioSliders[1].value),
            effectsVolume = Mathf.RoundToInt(audioSliders[2].value)
        };

        string Json = JsonUtility.ToJson(audio);
        Debug.Log(Json);
        SaveSettings.Save(SaveSettings.SAVE_FOLDER_Audio + "audioSettings.txt", Json);
    }
}
