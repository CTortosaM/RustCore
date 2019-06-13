using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToHub : MonoBehaviour
{
    GameObject player;
    public int enemyBoomerang = 20;
    public GameObject notnot;
    public GameObject yesyes;
    public GameObject bossboss;
    public int killsToUse = 20;
    

    // Start is called before the first frame update
    private void Awake()
    {
        GameManager.contBoomerang = 0;
        GameManager.contkills = killsToUse;
        notnot.SetActive(true);
        bossboss.SetActive(false);
        yesyes.SetActive(false);
    }
    void Start()
    {
        GameManager.contBoomerang = 0;
        GameManager.contkills = killsToUse;
        notnot.SetActive(true);
        bossboss.SetActive(false);
        yesyes.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
      
        if (GameManager.isBossDead)
        {
            if (Vector3.Distance(player.transform.position, transform.position) < 2)
            {

                okComputer.writeText("Press X to go to your rest room");
                if (Input.GetKeyUp(KeyCode.X))
                {
                    saveProgress();
                    StartCoroutine(teleport());
                    notnot.SetActive(true);
                    yesyes.SetActive(false);

                    GameManager.contkills = 0;

                }


            }
            bossboss.SetActive(false);
            notnot.SetActive(false);
            yesyes.SetActive(true);
        }
        else
        {
            if (Vector3.Distance(player.transform.position, transform.position) < 2)
            {

                okComputer.writeText("Kill the <color=red>big guy</color> first!");
                if (Input.GetKeyUp(KeyCode.X))
                {
                    StartCoroutine(teleport());
                    notnot.SetActive(true);
                    yesyes.SetActive(false);

                    GameManager.contkills = 0;

                }


            }
            bossboss.SetActive(false);
            notnot.SetActive(true);
            yesyes.SetActive(false);
        }
    }
   /* ivate void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log(player);
            Debug.Log(other.gameObject.name);
            Debug.Log(GameManager.contBoomerang);
            Debug.Log(GameManager.contkills);
            if (GameManager.isBossDead)
            {
                StartCoroutine(teleport());
                notnot.SetActive(true);
                yesyes.SetActive(false);
                
                GameManager.contkills = 0;
            }
            // if (LevelBuilder.spawns[random] != null)
            // {

            // }
        }
    }*/
    IEnumerator teleport()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadSceneAsync("SalaHub");
        /*player = GameObject.FindGameObjectWithTag("Player");
        int random = Random.Range(0, LevelBuilder.spawns.Count - 1);
        Debug.Log(LevelBuilder.spawns[random]);
       
        player.transform.position = LevelBuilder.spawns[random].position;*/
    }


    public static void saveProgress()
    {

        try
        {
            string save = SaveSettings.Load(SaveSettings.SAVE_FOLDER_Savestate + "saveState.txt");
            if (!save.Equals(null))
            {
                Savestate state = JsonUtility.FromJson<Savestate>(save);

                state.timesPlayed++;
                state.TotalKills += GameManager.totalKills;
                if (GameManager.totalKills > state.MaxKills) state.MaxKills = GameManager.totalKills;


                SaveSettings.Save(SaveSettings.SAVE_FOLDER_Savestate + "saveState.txt", JsonUtility.ToJson(state));

            }
            else
            {
                Savestate state = new Savestate
                {
                    timesPlayed = 0,
                    MaxKills = 0,
                    TotalKills = 0,
                    LeastTime = 0f
                };

                SaveSettings.Save(SaveSettings.SAVE_FOLDER_Savestate + "saveState.txt", JsonUtility.ToJson(state));
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Algo ocurre en ToHub: " + e.Message);
            Savestate state = new Savestate
            {
                timesPlayed = 0,
                MaxKills = 0,
                TotalKills = 0,
                LeastTime = 0f
            };

            SaveSettings.Save(SaveSettings.SAVE_FOLDER_Savestate + "saveState.txt", JsonUtility.ToJson(state));
        }
    }
}

