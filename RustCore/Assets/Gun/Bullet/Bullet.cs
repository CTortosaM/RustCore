using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private void Awake()
    {
        gameObject.SetActive(false);
    }
/*
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("XXXXXXXXXXXXXXXXXXXX> bala false ");

        gameObject.SetActive(false);
    }
    */
    // Update is called once per frame
    void Update()
    {
    }

    public void ShowBullet()
    {
        Debug.Log(" xxxxxxxx> bala true ");

        gameObject.SetActive(true);
    }

    public void HideBullet()
    {
        Debug.Log(" xxxxxxxx> bala false ");
        Invoke("Hide", 0.01f);
        Debug.Log(" xxxxxxxx> bala false 22222");
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
