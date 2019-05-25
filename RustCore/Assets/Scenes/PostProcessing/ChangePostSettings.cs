using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

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
        OptionsMenuManager.onChangeVideoSettings += setVideoSetting;
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

    private void setVideoSetting(int id)
    {
        Debug.Log("Soy el prefab: " + id);
        switch (id)
        {
            case 0:
                if (motionBlur) motionBlur.enabled.value = !motionBlur.enabled.value;
                break;
            case 1:
                if (occlusion) occlusion.enabled.value = !occlusion.enabled.value;
                break;
            case 2:
                if (grading) occlusion.enabled.value = !grading.enabled.value;
                break;
            case 3:
                if (vignette) vignette.enabled.value = !vignette.enabled.value;
                break;
            default:
                break;
        }
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
