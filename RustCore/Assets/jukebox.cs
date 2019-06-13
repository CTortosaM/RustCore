using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class jukebox : MonoBehaviour
{
    private GameObject player;
    public Text text;
    private string[] list = { "none", "electronic music", "classic rock" };
    private int selected;
    // Start is called before the first frame update
    void Start()
    {
        if (AudioControllerClass.getSelected()!=null)
        {
            text.text = list[AudioControllerClass.getSelected()];
        }
        else
        {
            text.text = list[0];
        }
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < 2)
        {

            okComputer.writeText("Press X to select a song to be played while hunting");
            if (Input.GetKeyUp(KeyCode.X))
            {
                selected++;
                if (selected <= 2)
                {
                  
                    text.text = list[selected];
                    AudioControllerClass.setSelected( selected);

                    AudioControllerClass.selected.Play();
                }
                else
                {
                    selected=0;
                    text.text = list[selected];
                    AudioControllerClass.setSelected(selected);

                    AudioControllerClass.selected.Play();
                }
             
            }


        }
    }
}
