using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class AudioControllerClass : MonoBehaviour
{
  
    public AudioMixer master;
    public AudioSource electronicMusic;
    public AudioSource silence;
    public AudioSource classicRock;
   public static AudioSource selected;
    private static AudioControllerClass that;
    // Start is called before the first frame update
    void Start()
    {

        DontDestroyOnLoad(this.gameObject);
        AudioSource[] aS = GetComponents<AudioSource>();
        electronicMusic = aS[0];
        silence = aS[1];
        classicRock = aS[2];
        selected = silence;
        that = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void setSelected(int index)
    {
        selected.Stop();
        if (index == 0) {
            selected = that.silence;
                }else if(index==1){
            selected = that.electronicMusic;
        }
        else if(index==2){
            selected = that.classicRock;
        }
    }
}
