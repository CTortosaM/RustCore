using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitmarker : MonoBehaviour
{
    [SerializeField] private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        AIEnemigo.onEnemyHit += PlayAnimation;
    }

    private void PlayAnimation(bool isDead)
    {
        Debug.Log("El enemigo está muerto?: " + isDead);
        if (animator.Equals(null))
        {
            animator = FindObjectOfType<Hitmarker>().gameObject.GetComponent<Animator>();
        }
       if(animator) animator.Play("hitmarkerHitOnEnemy");
    }
}
