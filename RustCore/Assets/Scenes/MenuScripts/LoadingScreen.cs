using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private Color unloaded;
    [SerializeField] private Color loaded;
    private int index;
    [SerializeField] private Image[] bars;
    [SerializeField] private float interval = .5f;
    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        foreach(Image image in bars)
        {
            image.color = unloaded;
        }

        StartCoroutine(LoadingAnimation(updateBar));

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void updateBar()
    {
        bars[index].color = loaded;
        if(index == 0)
        {
            bars[bars.Length - 1].color = unloaded;
        } else
        {
            bars[index - 1].color = unloaded;
        }
    }

    private IEnumerator LoadingAnimation(Action method) 
    {
        while (true)
        {
            method();

            index++;

            if (index >= bars.Length) index = 0;

            yield return new WaitForSeconds(interval);
        }
    }
}
