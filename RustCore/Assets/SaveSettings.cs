using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class SaveSettings
{
    public static string SEPARATOR = "#SAVE-VALUE#";
    public static string SAVE_FOLDER_Video = Application.dataPath + "/VideoSettings/";
    public static string SAVE_FOLDER_Audio = Application.dataPath + "/AudioSettings/";
    public static string SAVE_FOLDER_Controls = Application.dataPath + "/ControlsSettings/";


    public static void Init()
    {
        if (!Directory.Exists(SAVE_FOLDER_Video))
        {
            Directory.CreateDirectory(SAVE_FOLDER_Video);
        }

        if (!Directory.Exists(SAVE_FOLDER_Audio))
        {
            Directory.CreateDirectory(SAVE_FOLDER_Audio);
        }

        if (!Directory.Exists(SAVE_FOLDER_Controls))
        {
            Directory.CreateDirectory(SAVE_FOLDER_Controls);
        }
    }


    public static void Save(string path, string save)
    {
        Init();
        File.WriteAllText(path, save);
        Debug.Log(save);
    }

    public static string Load(string path)
    {
        string save = null;
        if(File.Exists(path))
        {
            save = File.ReadAllText(path);
        }

        return save;
    }
}
