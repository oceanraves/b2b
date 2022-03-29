using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animator_Swat : MonoBehaviour
{
    Animator animator;
    
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Animate(string animation)
    {
        if (animation == "Walk")
        {
            animator.SetBool("RunBool", false);
            animator.SetBool("WalkBool", true);
        }
        if (animation == "Run")
        {
            animator.SetBool("RunBool", true);
            animator.SetBool("WalkBool", false);
        }
        if (animation == "FallBack")
        {
            animator.SetBool("RunBool", false);
            animator.SetBool("WalkBool", false);

        }
        if (animation == "GetUp")
        {
            animator.SetBool("RunBool", false);
            animator.SetBool("WalkBool", false);
        }
        if (animation == "Shoot")
        {
            animator.SetBool("RunBool", false);
            animator.SetBool("WalkBool", false);
        }
        if (animation == "Idle")
        {
            animator.SetBool("RunBool", false);
            animator.SetBool("WalkBool", false);
        }
    }
}
