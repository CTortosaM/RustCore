using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class OptionsMenuManager : Toggle
{
    public delegate void ChangeVideoSettings(int id);
    public static event ChangeVideoSettings onChangeVideoSettings;
    // Start is called before the first frame update
    

    public void ToggleVideoOptions(int id)
    {
        onChangeVideoSettings(id);
    }

    
}
