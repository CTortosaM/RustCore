using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class CheckboxList : MonoBehaviour
{
    private VideoSettings SavedSettings;
    public List<Toggle> checkboxes;
    // Start is called before the first frame update
    void Start()
    {
        SaveSettings.Init();
        string saved = SaveSettings.Load(SaveSettings.SAVE_FOLDER_Video + "videoSettings.txt");
        setSettings(saved);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveVideoConfig()
    {
        VideoSettings videoSettings = new VideoSettings
        {
            motionBlur = checkboxes[0].isOn,
            ambientOcclussion = checkboxes[1].isOn,
            colorGradient = checkboxes[2].isOn,
            vignette = checkboxes[3].isOn
        };

        string Json = JsonUtility.ToJson(videoSettings);
        SaveSettings.Save(SaveSettings.SAVE_FOLDER_Video + "videoSettings.txt", Json);

    }


    private void setCheckBoxes(bool succes)
    {
        if (succes)
        {
            checkboxes[0].isOn = SavedSettings.motionBlur;
            checkboxes[1].isOn = SavedSettings.ambientOcclussion;
            checkboxes[2].isOn = SavedSettings.colorGradient;
            checkboxes[3].isOn = SavedSettings.vignette;
        } else
        {
            foreach(Toggle toggle in checkboxes)
            {
                toggle.isOn = true;
            }
        }
        
    }

    private void setSettings(string saved)
    {
        try
        {
            SavedSettings = JsonUtility.FromJson<VideoSettings>(saved);
            setCheckBoxes(true);
        } catch(Exception e)
        {
            setCheckBoxes(false);
        }
    }
   
}
