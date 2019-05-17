using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIEnemigo : MonoBehaviour
{
    public Transform player;
    public float radio = 6f;
    private int destino = 0;
    [SerializeField] private int dañoHacido = 30;
    [SerializeField] private float saludRestante = 100;
    public GameManager gameManager;
    private NavMeshAgent agente;
    [SerializeField] private float stopDistance=0;
    Vector3 Forward;
    public int ID=0;
    public float SaludRestante
    {
        get => saludRestante;
        set => saludRestante = value;
    }

    public enum EstadosPatrulla
    {
        Calma,
        Ataque
    }

    private EstadosPatrulla _estado = EstadosPatrulla.Calma;

    public EstadosPatrulla Estado
    {
        get => _estado;
        set
        {
            _estado = value;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if (Estado == EstadosPatrulla.Ataque) Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radio);
    }

    public void Actualizar(int dañoRecibido)
    {
        SaludRestante -= dañoRecibido;
        if (SaludRestante <= 0) Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        agente = GetComponent<NavMeshAgent>();

        player = GameObject.FindGameObjectWithTag("Player").transform;
       Forward = player.transform.forward;
        if (ID == 2)
        {
           
           
            agente.gameObject.GetComponentInChildren<enemyShoot>().gameObject.SetActive(true);
        
        }
            //agente.destination = punto.position;
            //SiguientePunto();
        }

    // Update is called once per frame
    void Update()
    {
        //agente.destination = player.position;

        if(player != null)
        {
            switch (Estado)
            {
                case EstadosPatrulla.Calma:
                    if (Vector3.Distance(transform.position, player.position) <= radio)
                    {
                        Estado = EstadosPatrulla.Ataque;
                    }
                    break;
                case EstadosPatrulla.Ataque:
                    
                    agente.SetDestination(player.position - stopDistance * Forward);

                    if (ID == 3)
                    {
                        agente.gameObject.transform.LookAt(player);
                        agente.transform.Rotate(new Vector3(0, 90, 0));
                        if (Vector3.Distance(player.position, agente.transform.position) < 10)
                        {
                           // Destroy(agente.GetComponent<NavMeshAgent>());
                            agente.transform.position = Vector3.MoveTowards(agente.transform.position, player.position, 0.5f);
                            if (Vector3.Distance(player.position, agente.transform.position) < 2)
                            {
                                StartCoroutine(explotar());
                            }
                            }
                    }
                    else if (ID==2)
                    {
                        agente.gameObject.transform.LookAt(player);
                        agente.gameObject.GetComponentInChildren<enemyShoot>().aux = true;
                    }else if (ID == 1)
                    {
                        agente.gameObject.transform.LookAt(player);
                        agente.transform.Rotate(new Vector3(0, 90, 0));
                    }
                    
                    if (Vector3.Distance(transform.position, player.position) > radio)
                    {
                        Estado = EstadosPatrulla.Calma;
                    }
                    break;
            }
        }
        

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && ID!=2 && ID!=3)
        {
            Debug.Log("El enemigo te hace " + dañoHacido + " de daño.");
            other.gameObject.GetComponent<HealtAndShield>().TakeDamage(dañoHacido);
            //gameManager.Daño = dañoHacido;
            //gameManager.ComprobarVictoria();
        }

        //Hacer que se eliminen si se les dispara un proyectil
        if (other.gameObject.CompareTag("Proyectil"))
        {
            Destroy(gameObject);
        }
    }
    /*
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Colision");
        //Destroy(gameObject);
    }
    */
    IEnumerator explotar()
    {
        //Código de la explosión va aquí supongo
        yield return new WaitForSeconds(1);
        player.gameObject.GetComponent<HealtAndShield>().TakeDamage(dañoHacido);
        Destroy(gameObject);

    }


}
