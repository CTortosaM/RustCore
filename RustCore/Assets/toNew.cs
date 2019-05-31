using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class toNew : MonoBehaviour
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
    }

    // Update is called once per frame
    void Update()
    {

        if (GameManager.contBoomerang >= enemyBoomerang)
        {

        }
        else if (GameManager.contkills >= killsToUse)
        {

            bossboss.SetActive(true);
            notnot.SetActive(false);
            yesyes.SetActive(false);
        }
        else
        {
            bossboss.SetActive(false);
            notnot.SetActive(true);
            yesyes.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log(player);
            Debug.Log(other.gameObject.name);
            Debug.Log(GameManager.contBoomerang);
            Debug.Log(GameManager.contkills);
            if (GameManager.contBoomerang >= enemyBoomerang)
            {

            }
            else if (GameManager.contkills >= killsToUse)
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
    }
    IEnumerator teleport()
    {
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            if (SceneManager.GetSceneAt(i).name == "PauseMenu")
            {
                SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("PauseMenu").buildIndex);
            }
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        PauseManager.isPaused = false;
        /*player = GameObject.FindGameObjectWithTag("Player");
        int random = Random.Range(0, LevelBuilder.spawns.Count - 1);
        Debug.Log(LevelBuilder.spawns[random]);
       
        player.transform.position = LevelBuilder.spawns[random].position;*/
    }
}

