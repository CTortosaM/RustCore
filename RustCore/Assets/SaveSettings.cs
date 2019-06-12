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
    public static string SAVE_FOLDER_Savestate = Application.dataPath + "/Savestate/";


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

        if (!Directory.Exists(SAVE_FOLDER_Savestate))
        {
            Directory.CreateDirectory(SAVE_FOLDER_Savestate);
        }
    }


    public static void Save(string path, string save)
    {
        Init();
        File.WriteAllText(path, save);
    }

    public static string Load(string path)
    {
        Init();
        string save = null;
        if(File.Exists(path))
        {
            save = File.ReadAllText(path);
        }

        return save;
    }


    
}
