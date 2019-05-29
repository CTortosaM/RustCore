using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashIcon : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        PlayerController.onPlayerDash += startAnimation;
    }

    private void startAnimation()
    {
        if (animator.Equals(null)) animator = FindObjectOfType<DashIcon>().gameObject.GetComponent<Animator>();
        animator.Play("DashExecuted");
    }
    

    
}
