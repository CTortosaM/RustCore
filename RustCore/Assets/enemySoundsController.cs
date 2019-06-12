using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySoundsController : MonoBehaviour
{
  //  public static float distanceToPlayer;
    private static enemySoundsController Instance;
    private AudioSource[] sounds;
    public float fade = 2;
    // Start is called before the first frame update
    void Start()
    {
       // distanceToPlayer = 100;
        sounds = GetComponents<AudioSource>();
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public static void sound(Transform t, int ID )
    {
      //  if (distance < distanceToPlayer)
        //{
           // distanceToPlayer = distance;
            Instance.gameObject.transform.position = t.position;
            if (ID == 0)
            {

            }
            else if (ID == 1)
            {
                Instance.StartCoroutine(FadeIn(Instance.sounds[1], Instance.fade));
            }
            else if (ID == 2)
            {
                Instance.StartCoroutine(FadeIn(Instance.sounds[0], Instance.fade));
            }
            else if (ID == 3)
            {
                Instance.StartCoroutine(FadeIn(Instance.sounds[1], Instance.fade));
            }
       // }
    }
    public static void noSound( int ID)
    {
       // distanceToPlayer = 100;
        if (ID == 0)
        {

        }
        else if (ID == 1)
        {
            Instance.StartCoroutine(FadeOut(Instance.sounds[1], Instance.fade));
          
        }
        else if (ID == 2)
        {
            Instance.StartCoroutine(FadeOut(Instance.sounds[0], Instance.fade));
        }
        else if (ID == 3)
        {
            Instance.StartCoroutine(FadeOut(Instance.sounds[1], Instance.fade));
        }

    }
    public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }
    public static IEnumerator FadeIn(AudioSource audioSource, float FadeTime)
    {
        float startVolume = 0.2f;

        audioSource.volume = 0;
        if(!audioSource.isPlaying)audioSource.Play();

        while (audioSource.volume < 1.0f)
        {
            audioSource.volume += startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.volume = 1f;
    }
}
