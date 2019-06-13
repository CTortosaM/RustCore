using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class AudioControllerClass : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup[] groups;
    public AudioMixer master;
    public AudioSource electronicMusic;
    public AudioSource silence;
    public AudioSource classicRock;
   public static AudioSource selected;
    private static AudioControllerClass that;

    public int selectedIndex = 0;
    private int masterIndex = 0;
    private int musicIndex = 1;
    private int effectsIndex = 2;

    private float minAudio = -20;
    private float maxAudio = 3;
    // Start is called before the first frame update
    void Start()
    {

        DontDestroyOnLoad(this.gameObject);
        AudioSource[] aS = GetComponents<AudioSource>();
        OptionsMenuManager.onAudioOptionsSaved += onAudioOptionsChanged;
        electronicMusic = aS[0];
        silence = aS[1];
        classicRock = aS[2];
        selected = silence;
        that = this;

        loadAudioSettings();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static int getSelected()
    {
        return that.selectedIndex;
    }
    public static void setSelected(int index)
    {
        that.selectedIndex = index;
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


    private void onAudioOptionsChanged(AudioSettings settings)
    {
        float masterValue = (float) settings.masterVolume;
        float musicValue = (float)settings.musicVolume;
        float effectsValue = (float)settings.effectsVolume;

        master.SetFloat("VolumenMaster", scale(0f,100f, minAudio, maxAudio, masterValue));
        master.SetFloat("VolumenMusic", scale(0f, 100f, minAudio, maxAudio, musicValue));
        master.SetFloat("VolumenVFX", scale(0f, 100f, minAudio, maxAudio, effectsValue));
    }



    public static float scale(float OldMin, float OldMax, float NewMin, float NewMax, float OldValue)
    {

        float OldRange = (OldMax - OldMin);
        float NewRange = (NewMax - NewMin);
        float NewValue = (((OldValue - OldMin) * NewRange) / OldRange) + NewMin;

        return (NewValue);
    }

    private void loadAudioSettings()
    {
        float masterVol = scale(0f, 100f, minAudio, maxAudio, 100f);
        float music = masterVol;
        float effects = masterVol;


        string set = SaveSettings.Load(SaveSettings.SAVE_FOLDER_Audio + "audioSettings.txt");

        if (!set.Equals(null))
        {
            if(set.Length > 0)
            {
                AudioSettings settings = JsonUtility.FromJson<AudioSettings>(set);
                masterVol = scale(0f, 100f, minAudio, maxAudio, (float) settings.masterVolume);
                music = scale(0f, 100f, minAudio, maxAudio, (float)settings.musicVolume);
                effects = scale(0f, 100f, minAudio, maxAudio, (float)settings.effectsVolume);
            }
        }

        master.SetFloat("VolumenMaster", masterVol);
        master.SetFloat("VolumenMusic", music);
        master.SetFloat("VolumenVFX", effects);
        
    }
}
