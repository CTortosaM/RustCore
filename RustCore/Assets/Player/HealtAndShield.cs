using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealtAndShield : MonoBehaviour
{
    [SerializeField] private float shieldRegenInterval = 7f; //Tiempo después del cual, si no has recibido daño, se empieza a regenrar el escudo
    private float nextPossibleRegen; //Momento a partir del cual el jugador puede regenrar el escudo
    [SerializeField] private float shieldRegenStep = 10; // Porcentaje de escudo que regenera cada x tiempo (x = regen )
    [SerializeField] private float shieldRegenSpeed = .5f;
    [SerializeField] private Text healthText;
    [SerializeField] private Text shieldText;
    [SerializeField] private bool isDead = false;
    [SerializeField] private int health = 100;
    [SerializeField] private int shield = 100;
    [SerializeField] private int maxShield = 100;
    [SerializeField] private int maxHealth = 100;

    public int Health { get => health; set => health = value; }
    public int Shield { get => shield; set => shield = value; }
    public bool IsDead { get => isDead; set => isDead = value; }
    public float ShieldRegenInterval { get => shieldRegenInterval; set => shieldRegenInterval = value; }
    public int MaxShield { get => maxShield; set => maxShield = value; }
    public int MaxHealth { get => maxHealth; set => maxHealth = value; }

    public delegate void PlayerDeath();
    public PlayerDeath onPlayerDeath;

    private bool shieldRegenerating = false;

    // Start is called before the first frame update
    void Start()
    {
        UpdateShieldAndHealtText();
    }

    // Update is called once per frame
    void Update()
    {
        if (health == 0) killPlayer();
        if (Time.time > nextPossibleRegen && shield < maxShield && !shieldRegenerating)
        {
            shieldRegenerating = true;
            InvokeRepeating("regenerateShield", 0, shieldRegenSpeed);
        }
    }

    public void TakeDamage(int damageDone)
    {
        CancelInvoke("regenerateShield");
        shieldRegenerating = false;
        if (shield > 0)
        {
            nextPossibleRegen = Time.time + shieldRegenInterval;
            if (shield - damageDone < 0)
            {
                shield = 0;
            }
            else
            {
                shield -= damageDone;
            }
            UpdateShieldAndHealtText();
            
            return;
        }

        if(health - damageDone <= 0)
        {
            health = 0;
            UpdateShieldAndHealtText();
            return;
        }

        health -= damageDone;
        UpdateShieldAndHealtText();
    }

    public void killPlayer()
    {
        IsDead = true;
    }

    public void UpdateShieldAndHealtText()
    {
        shieldText.text = shield.ToString() + " SH";
        healthText.text = health.ToString() + " HP";
    }

    public void regenerateShield()
    {
         int step = Mathf.RoundToInt(maxShield/shieldRegenStep);
         if(shield + step >= maxShield)
         {
             shield = maxShield;
             CancelInvoke("regenerateShield");
            shieldRegenerating = false;
         } else
         {
             shield += step;
         }
         UpdateShieldAndHealtText();
    }


    public void Heal(int ammount)
    {
        if(health + ammount > maxHealth)
        {
            health = maxHealth;
            UpdateShieldAndHealtText();
        } else
        {
            health += ammount;
            UpdateShieldAndHealtText();
        }
    }
}
