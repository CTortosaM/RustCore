using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class okComputer : MonoBehaviour
{
    public Text text;
     bool clearText;
    private static okComputer that;
    // Start is called before the first frame update
    void Start()
    {
        Teleporter.onBoss += Boss;
        Teleporter.onYes += Yes;
        Teleporter.onNo += No;
        that = this;
       
    }

    // Update is called once per frame
    void Update()
    {
        if(text.text != "" && !clearText)
        {
            clearText = true;
            StartCoroutine(clear());
            
        }
    }
    void Boss()
    {
        text.text = "Good job! <i>Prepare yourself</i> \n\n                                          <color=blue>Press X to interact</color>";
       // StartCoroutine(clear());
    }
    void No()
    {
        text.text = "I won't take you to anywhere unless you kill <color=red><b>" + (Teleporter.killsToUse+Mathf.Floor(-GameManager.contkills)) +  "</b></color> more or wait for a bit";
       // StartCoroutine(clear());
    }
    void Yes()
    {
        text.text = "<b>Hello there</b>, if you kill <color=orange><b>" + (Teleporter.enemyBoomerang +Mathf.Floor(-GameManager.contBoomerang)) + "</b></color> robots just by your boomerang I will take you to the final step of this funny level I've built for you, until then I will take you to <i>wherever I want</i> \n\n                                              <color=green> Press X to interact</color>";
       // StartCoroutine(clear());
    }
    IEnumerator clear()
    {
       yield return new WaitForSeconds(6);
        /*for(int i=0; i < text.text.Length; i++)
        {
            text.text.Remove(i,1);
            yield return new WaitForSeconds(0.1f);
        }*/
        text.text="";
        clearText = false;
    }
    public static void writeText(string t)
    {
        that.text.text = t;

    }
   
}
