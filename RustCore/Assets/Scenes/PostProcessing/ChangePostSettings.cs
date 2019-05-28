using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class ChangePostSettings : MonoBehaviour
{
    
    private PostProcessVolume postVolume;
    private MotionBlur motionBlur;
    private AmbientOcclusion occlusion;
    private ColorGrading grading;
    private Vignette vignette;
    private VideoSettings VideoSettings;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        postVolume = GetComponent<PostProcessVolume>();
        if (postVolume)
        {
            motionBlur = postVolume.profile.GetSetting<MotionBlur>();
            occlusion = postVolume.profile.GetSetting<AmbientOcclusion>();
            grading = postVolume.profile.GetSetting<ColorGrading>();
            vignette = postVolume.profile.GetSetting<Vignette>();
            loadVideoSettingsFromFile();

        } else
        {
            Debug.LogError("No hay componente volumen");
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

  

    private void loadVideoSettingsFromFile()
    {
        SaveSettings.Init();
        string settings = SaveSettings.Load(SaveSettings.SAVE_FOLDER_Video + "videoSettings.txt");
        if (!settings.Equals(null))
        {
            VideoSettings = JsonUtility.FromJson<VideoSettings>(settings);
        } else
        {
            VideoSettings = new VideoSettings
            {
                motionBlur = true,
                ambientOcclussion = true,
                colorGradient = true,
                vignette = true
            };
        }

        setVideoSettingsFromObject(VideoSettings);

    }

    private void setVideoSettingsFromObject(VideoSettings video)
    {
        if (postVolume)
        {
            if (motionBlur) motionBlur.enabled.value = video.motionBlur;
            if (occlusion) occlusion.enabled.value = video.ambientOcclussion;
            if (grading) grading.enabled.value = video.colorGradient;
            if (vignette) grading.enabled.value = video.vignette;
        }
    }
}
