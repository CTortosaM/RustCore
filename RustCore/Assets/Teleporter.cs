using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
     
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log(player);
            Debug.Log(other.gameObject.name);
            StartCoroutine(teleport());
            // if (LevelBuilder.spawns[random] != null)
            // {
           
           // }
        }
    }
    IEnumerator teleport()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        int random = Random.Range(0, LevelBuilder.spawns.Count - 1);
        Debug.Log(LevelBuilder.spawns[random]);
        yield return new WaitForSeconds(0.5f);
        player.transform.position = LevelBuilder.spawns[random].position;
    }
}
