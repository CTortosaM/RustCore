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
                LeastTime = 0f
            };


            SaveSettings.Save(SaveSettings.SAVE_FOLDER_Savestate + "savestate.txt", JsonUtility.ToJson(savestate));
        }

        return savestate;
    }



    private void updateText()
    {
        int totalkills = 0;
        int maxkills = 0;
        float leasttime = 0f;

        Savestate savestate = getSaveState();

        if (!savestate.Equals(null))
        {
            totalkills = savestate.TotalKills;
            maxkills = savestate.MaxKills;
            leasttime = savestate.LeastTime;
        }

        string newText = "Total Kills:" + Environment.NewLine + totalkills
            + Environment.NewLine + "Max Kills:" + Environment.NewLine + maxkills 
            + Environment.NewLine + "Least time" + Environment.NewLine +leasttime;


        text.text = newText;
    }
}
