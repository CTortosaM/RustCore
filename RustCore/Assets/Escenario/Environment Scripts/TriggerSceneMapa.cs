using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerSceneMapa : MonoBehaviour
{
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < 2)
        {

            okComputer.writeText("Press <color=red>X</color> to start a new fight");
            if (Input.GetKeyUp(KeyCode.X))
            {
                startFight();

            }


        }
    }

    private void startFight()
    {
       
            SceneManager.LoadSceneAsync("Mapa");
        
    }
}
