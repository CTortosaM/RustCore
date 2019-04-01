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
    public GameManager gameManager;
    private NavMeshAgent agente;

    private float saludRestante = 100;

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
                    agente.SetDestination(player.position);
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
        if (other.gameObject.CompareTag("Player"))
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


}
