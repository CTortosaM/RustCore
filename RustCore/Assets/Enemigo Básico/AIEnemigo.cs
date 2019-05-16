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

    [SerializeField] private float despawnAfterDeathTimer = 30f;

    private float deathTime;
    private float timeToDespawn;

    [SerializeField] private int enemyID;

    [SerializeField] private bool canDamage = true;
    [SerializeField] private ParticleSystem explosion;
    [SerializeField] private ParticleSystem smoke;
    public float SaludRestante
    {
        get => saludRestante;
        set => saludRestante = value;
    }

    public enum EstadosPatrulla
    {
        Calma,
        Ataque,
        Muerte
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

    public bool CanDamage { get => canDamage; set => canDamage = value; }
    public int EnemyID { get => enemyID;}
    public float DespawnAfterDeathTimer { get => despawnAfterDeathTimer; set => despawnAfterDeathTimer = value; }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if (Estado == EstadosPatrulla.Ataque) Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radio);
    }

    public void Actualizar(int dañoRecibido)
    {
        if(Estado != EstadosPatrulla.Muerte)
        {
            SaludRestante -= dañoRecibido;
            if (SaludRestante <= 0)
            {
                deathTime = Time.time;
                timeToDespawn = deathTime + despawnAfterDeathTimer;
                Estado = EstadosPatrulla.Muerte;
                if (!explosion.isPlaying) explosion.Play();
                if (!smoke.isPlaying) smoke.Play();
            }
        }
        
        
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

        if(Estado == EstadosPatrulla.Muerte && Time.time >= timeToDespawn)
        {
            Destroy(gameObject);
        }

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
                case EstadosPatrulla.Muerte:
                    agente.isStopped = true;
                    canDamage = false;
                    break;
            }
        }
        

    }

    void OnTriggerEnter(Collider other)
    {
        if(Estado != EstadosPatrulla.Muerte)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (canDamage) other.gameObject.GetComponent<HealtAndShield>().TakeDamage(dañoHacido);

                //gameManager.Daño = dañoHacido;
                //gameManager.ComprobarVictoria();
            }

            //Hacer que se eliminen si se les dispara un proyectil
            if (other.gameObject.CompareTag("Proyectil"))
            {
                //Destroy(gameObject);
                Estado = EstadosPatrulla.Muerte;
                deathTime = Time.time;
                timeToDespawn = deathTime + despawnAfterDeathTimer;
            }
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
