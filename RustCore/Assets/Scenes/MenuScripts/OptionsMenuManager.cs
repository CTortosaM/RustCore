using System;
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



    public delegate void AudioOptionsChanged(AudioSettings settings);
    public static event AudioOptionsChanged onAudioOptionsSaved;
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
        if(index == 2)
        {
            updateAudioSliderValuesOnLoad();
        }
        goBackButton.gameObject.SetActive(true);
        closeMenuButton.gameObject.SetActive(false);
    }

    public void updateAudioValuesText(int which)
    {
        float min = (float)0.0001;
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
        onAudioOptionsSaved(audio);
    }

    public void updateAudioSliderValuesOnLoad()
    {
        float min = (float)0.0001;
        string settings = SaveSettings.Load(SaveSettings.SAVE_FOLDER_Audio + "audioSettings.txt");
        try
        {
            AudioSettings audio = JsonUtility.FromJson<AudioSettings>(settings);
            audioSliders[0].value = audio.masterVolume;
            audioSliders[1].value = audio.musicVolume;
            audioSliders[2].value = audio.effectsVolume;
        } catch(Exception e)
        {
            Debug.Log("Algo ha pasado en Audio");
            audioSliders[0].value = 100;
            audioSliders[1].value = 100;
            audioSliders[2].value = 100;
        }
    }
}
