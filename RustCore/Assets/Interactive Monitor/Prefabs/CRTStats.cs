using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CRTStats : MonoBehaviour
{

    [SerializeField] private Text text;
    // Start is called before the first frame update
    void Start()
    {
        updateText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Savestate getSaveState()
    {
        Savestate savestate = null;
        string save = SaveSettings.Load(SaveSettings.SAVE_FOLDER_Savestate + "savestate.txt");

        savestate = JsonUtility.FromJson<Savestate>(save);

        if(save == null)
        {
            savestate = new Savestate()
            {
                TotalKills = 0,
                MaxKills = 0,
                timesPlayed = 0,
                timesPlayedPublic = 0
            };


            SaveSettings.Save(SaveSettings.SAVE_FOLDER_Savestate + "savestate.txt", JsonUtility.ToJson(savestate));
        }

        return savestate;
    }



    private void updateText()
    {
        int totalkills = 0;
        int maxkills = 0;
        int totalTimes = 0;

        Savestate savestate = getSaveState();

        if (!savestate.Equals(null))
        {
            totalkills = savestate.TotalKills;
            maxkills = savestate.MaxKills;
            totalTimes = savestate.timesPlayedPublic;
        }

        string newText = "Total Kills:" + Environment.NewLine + totalkills
            + Environment.NewLine + "Max Kills:" + Environment.NewLine + maxkills 
            + Environment.NewLine + "Times played" + Environment.NewLine + totalTimes;


        text.text = newText;
    }
}
