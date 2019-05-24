using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parpadeo : MonoBehaviour
{
    public Light _Light;

    public float MinTime;
    public float MaxTime;
    public float Timer;

    public AudioSource AS;
    public AudioClip LightAudio;

    // Start is called before the first frame update
    void Start()
    {
        Timer = Random.Range(MinTime, MaxTime);
    }

    // Update is called once per frame
    void Update()
    {
        LuzParpadeante();
    }

    void LuzParpadeante()
    {
        if (Timer > 0)
            Timer -= Time.deltaTime;

        if (Timer <= 0)
        {
           if(_Light) _Light.enabled = !_Light.enabled;
            Timer = Random.Range(MinTime, MaxTime);
            AS.PlayOneShot(LightAudio);
        }
    }

}
