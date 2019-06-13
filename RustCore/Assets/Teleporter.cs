using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Teleporter : MonoBehaviour
{
    GameObject player;
    public static int enemyBoomerang=20;
    public GameObject notnot;
    public GameObject yesyes;
    public GameObject bossboss;
    public static int killsToUse = 5;
    public float secondsToWait = 60;
    private Transform spawn;
    public TextMeshPro text;

    public delegate void textYes();
    public static event textYes onYes;
    public delegate void textNo();
    public static event textYes onNo;
    public delegate void textBoss();
    public static event textYes onBoss;
    public AudioSource[] aS;
    bool aux;
    // Start is called before the first frame update
    private void Awake()
    {
        aS = GetComponents<AudioSource>();
        GameManager.contBoomerang = 0;
        GameManager.contkills = killsToUse;
        notnot.SetActive(false);
        bossboss.SetActive(false);
        yesyes.SetActive(true);
        aux = false;
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        GameManager.contBoomerang = 0;
        GameManager.contkills = killsToUse;
        notnot.SetActive(false);
        bossboss.SetActive(false);
        yesyes.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

        

        if ((-Mathf.Floor(-GameManager.contBoomerang)) >= enemyBoomerang)
        {
            if (Vector3.Distance(player.transform.position, transform.position) < 2)
            {
            
                onBoss();
                if (Input.GetKeyUp(KeyCode.X))
                {
                    GameManager.contkills = 0;
                    StartCoroutine(teleport());
                }


            }
           
            yesyes.SetActive(false);
            notnot.SetActive(false);
            bossboss.SetActive(true);
        }else if((-Mathf.Floor(-GameManager.contkills)) >= killsToUse||GameManager.ticktock>=60)
        {

            if (Vector3.Distance(player.transform.position, transform.position) < 2)
            {
                if (!aS[1].isPlaying) aS[1].Play();
                onYes();
                if (Input.GetKeyUp(KeyCode.X))
                {
                    StartCoroutine(teleport());
                    notnot.SetActive(true);
                    yesyes.SetActive(false);
                    GameManager.ticktock = 0;
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
          
                onNo();

            }
            
            bossboss.SetActive(false);
            notnot.SetActive(true);
            yesyes.SetActive(false);
            text.text = (-Mathf.Floor(-GameManager.contkills)).ToString()+"/"+killsToUse;
        }
      
    }

  
    /*
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
                GameManager.contkills = 0;
                StartCoroutine(teleport());
            }
            else if(GameManager.contkills >= killsToUse || GameManager.ticktock>=60)
            {
                StartCoroutine(teleport());
                notnot.SetActive(true);
                yesyes.SetActive(false);
                GameManager.ticktock = 0;
                GameManager.contkills = 0;
            }
            // if (LevelBuilder.spawns[random] != null)
            // {
           
           // }
        }
    }
    */

    IEnumerator teleport()
    {
        aS[0].Play();
        player = GameObject.FindGameObjectWithTag("Player");
        spawn = GameObject.FindGameObjectWithTag("EndRoom").GetComponent<EndRoom>().playerSpawn;
        int random = Random.Range(0, LevelBuilder.spawns.Count - 1);
        Debug.Log(LevelBuilder.spawns[random]);
        yield return new WaitForSeconds(0.5f);
        if (GameManager.contBoomerang >= enemyBoomerang)
        {
            okComputer.writeText("Now kill the <color=red>big guy</color> to enable the exits!");
            player.transform.position = spawn.position;
        }
        else
        {
            player.transform.position = LevelBuilder.spawns[random].position;
        }
  
    }
}
