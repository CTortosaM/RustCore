using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup_HP : MonoBehaviour
{
    [SerializeField] private int healAmmount = 30;
    [SerializeField] private string targetTag = "Player";
    private BoxCollider boxCollider;

    public int HealAmmount { get => healAmmount; set => healAmmount = value; }

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == targetTag)
        {
            if (HealtAndShield.healthPublic < 100)
            {
                StartCoroutine(sound());
                other.gameObject.GetComponent<HealtAndShield>().Heal(healAmmount);
           
            }
        }
    }
    IEnumerator sound()
    {
        gameObject.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(0.7f);
        Destroy(this.gameObject);
    }
}
