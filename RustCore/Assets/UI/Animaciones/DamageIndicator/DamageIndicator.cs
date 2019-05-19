using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageIndicator : MonoBehaviour
{
    [SerializeField] private Animator animator;
    // Start is called before the first frame update
   
    void Start()
    {
        //animator = GetComponent<Animator>();
        HealtAndShield.onPlayerHit += StartAnimation;
    }

   private void StartAnimation(bool isOnHealth, bool isDead)
    {
        if (isOnHealth && !isDead)
        {
            animator.Play("HealthHit");
            return;
        }
        if (!animator) animator = FindObjectOfType<DamageIndicator>().GetComponent<Animator>();
        if(!isDead && animator) animator.Play("ShieldHit");
    }
    
}
