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
    private float nextPossibleAttack;
    [SerializeField] private float attackCooldown = .5f;

    //Delegates
    public delegate void EnemyHit(bool isDead);
    public static event EnemyHit onEnemyHit; 

    [SerializeField] private float despawnAfterDeathTimer = 30f;

    private float deathTime;
    private float timeToDespawn;

    [SerializeField] private int enemyID;

    [SerializeField] private bool canDamage = true;
    [SerializeField] private ParticleSystem explosion;
    [SerializeField] private ParticleSystem smoke;

    [SerializeField] private float stopDistance=0;
    Vector3 Forward;
    public int ID=0;
    private int contBoomerbang = 0;
    public float SaludRestante
    {
        get => saludRestante;
        set => saludRestante = value;
    }
    private float SaludTotal;
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
    private bool hasDied;
    public bool CanDamage { get => canDamage; set => canDamage = value; }
    public int EnemyID { get => enemyID;}
    public float DespawnAfterDeathTimer { get => despawnAfterDeathTimer; set => despawnAfterDeathTimer = value; }
    public delegate void boomerangDeath();
    public static event boomerangDeath boom;
    public delegate void kills();
    public static event kills onekill;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if (Estado == EstadosPatrulla.Ataque) Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radio);
    }

    public void Actualizar(int dañoRecibido, int idArma)
    {
        if(Estado != EstadosPatrulla.Muerte)
        {
            if (idArma == 2)
            {
                contBoomerbang++;
            }
            SaludRestante -= dañoRecibido;
            if (SaludRestante <= 0)
            {
               
                    deathTime = Time.time;
                    timeToDespawn = deathTime + despawnAfterDeathTimer;
                    Estado = EstadosPatrulla.Muerte;
                if (!explosion.isPlaying)
                {
                    explosion.Play();
                    enemyExplosionController.explode(gameObject.transform);
                }
                    if (!smoke.isPlaying) smoke.Play();
                    if (ID == 2)
                    {
                        // Destroy(agente.gameObject.GetComponentInChildren<enemyShoot>());
                        agente.GetComponent<CapsuleCollider>().isTrigger = true;
                    }
                    if (agente.gameObject.GetComponent<Animator>() != null)
                    {
                        Destroy(agente.gameObject.GetComponent<Animator>());
                    }
                    
                
            } else
            {
                onEnemyHit(false);
            }
        }
        
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
        hasDied = false;
        nextPossibleAttack = 0f;
        SaludTotal = saludRestante;
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
      
        if(Estado == EstadosPatrulla.Muerte && Time.time >= timeToDespawn)
        {
            Destroy(gameObject);

        }
       
        if(player != null)
        {
            switch (Estado)
            {
                case EstadosPatrulla.Calma:
                    if (ID == 2)
                    {
                        if (agente.gameObject.GetComponentInChildren<enemyShoot>() != null)
                        {
                            agente.gameObject.GetComponentInChildren<enemyShoot>().aux = false;
                        }
                    }
                    if (Vector3.Distance(transform.position, player.position) <= radio)
                    {
                      
                        if (ID == 2)
                        {
                            if (agente.gameObject.GetComponentInChildren<enemyShoot>() != null)
                            {
                                agente.gameObject.GetComponentInChildren<enemyShoot>().aux = true;
                            }
                        }
                        Estado = EstadosPatrulla.Ataque;
                    }
                    break;
                case EstadosPatrulla.Ataque:
                  
                    if (agente.isOnNavMesh)agente.SetDestination(player.position - stopDistance * Forward);
                    if(Vector3.Distance(transform.position, player.position) <= 5) enemySoundsController.sound(gameObject.transform, ID);
                    if (ID == 3)
                    {
                        if (IsAgentOnNavMesh(agente.gameObject))
                        {
                            agente.gameObject.transform.LookAt(player);
                            agente.transform.Rotate(new Vector3(0, 90, 0));
                        }
                        
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
                     
                    }else if (ID == 1)
                    {
                        if (IsAgentOnNavMesh(agente.gameObject))
                        {
                            agente.gameObject.transform.LookAt(player);
                            agente.transform.Rotate(new Vector3(0, 90, 0));
                        }
                        
                       
                    }
                    
                    if (Vector3.Distance(transform.position, player.position) > radio)
                    {
                        if (ID == 2)
                        {
                            if (agente.gameObject.GetComponentInChildren<enemyShoot>() != null)
                            {
                                agente.gameObject.GetComponentInChildren<enemyShoot>().aux = false;
                            }
                        }
                        enemySoundsController.noSound( ID);
                        Estado = EstadosPatrulla.Calma;
                    }
                    break;
                case EstadosPatrulla.Muerte:
                    enemySoundsController.noSound( ID);
                    if (ID == 2)
                    {
                        if (agente.gameObject.GetComponentInChildren<enemyShoot>() != null)
                        {
                            agente.gameObject.GetComponentInChildren<enemyShoot>().aux = false;
                        }
                    }
                    if (!hasDied)
                    {
                        onEnemyHit(true);
                        onekill();
                        if (contBoomerbang == -Mathf.Floor(-SaludTotal / 80))
                        {
                            boom();
                        }
                        hasDied = true;
                    }
                        agente.isStopped = true;
                    canDamage = false;
                    break;
            }
        }
        

    }

    void OnTriggerEnter(Collider other)
    {

        if (Estado != EstadosPatrulla.Muerte)
        {

            if (other.gameObject.CompareTag("Player") && ID != 2 && ID != 3)

            {
                if (other.gameObject.CompareTag("Player"))
                {
                    if (canDamage && Time.time >= nextPossibleAttack)
                    {
                        other.gameObject.GetComponent<HealtAndShield>().TakeDamage(dañoHacido, gameObject.transform.position);
                        nextPossibleAttack = Time.time + attackCooldown;
                    }

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
        if (Estado != EstadosPatrulla.Muerte && !PauseManager.isPaused)
        {
            yield return new WaitForSeconds(0.4f);
            {
                if (Estado != EstadosPatrulla.Muerte && !PauseManager.isPaused)
                {
                    if (!explosion.isPlaying)
                    {
                        explosion.Play();
                        enemyExplosionController.explode(gameObject.transform);
                    } //Código de la explosión va aquí supongo
                    yield return new WaitForSeconds(0.3f);

                    if (canDamage) player.gameObject.GetComponent<HealtAndShield>().TakeDamage(dañoHacido, transform.position);
                    Destroy(gameObject);
                }
            }
        }

    }
    // Don't set this too high, or NavMesh.SamplePosition() may slow down
    float onMeshThreshold = 3;

    public bool IsAgentOnNavMesh(GameObject agentObject)
    {
        Vector3 agentPosition = agentObject.transform.position;
        NavMeshHit hit;

        // Check for nearest point on navmesh to agent, within onMeshThreshold
        if (NavMesh.SamplePosition(agentPosition, out hit, onMeshThreshold, NavMesh.GetAreaFromName("navMesh")))
        {
            // Check if the positions are vertically aligned
            if (Mathf.Approximately(agentPosition.x, hit.position.x)
                && Mathf.Approximately(agentPosition.z, hit.position.z))
            {
                // Lastly, check if object is below navmesh
                return agentPosition.y >= hit.position.y;
            }
        }

        return false;
    }
}
