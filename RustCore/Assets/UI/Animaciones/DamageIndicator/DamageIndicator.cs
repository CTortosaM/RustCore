﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageIndicator : MonoBehaviour
{
     private Animator animator;
    // Start is called before the first frame update
   
    void Start()
    {
        animator = GetComponent<Animator>();
        HealtAndShield.onPlayerHit += StartAnimation;
    }

   private void StartAnimation(bool isOnHealth)
    {
        if (isOnHealth)
        {
            animator.Play("HealthHit");
            return;
        }

        animator.Play("ShieldHit");
    }
    
}
