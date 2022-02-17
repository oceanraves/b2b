using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;

    void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
    }

    public void MoveAnimation(Vector3 direction, bool runAnim)
    {
        if (direction.magnitude >= 0.1f)
        {
            if (runAnim)
            {
                Animate(false, "Walk");
                Animate(true, "Run");
            }
            if (!runAnim)
            {
                Animate(false, "Run");
                Animate(true, "Walk");
            }
        }

        if (direction.magnitude < 0.1f)
        {
            Animate(false, "Walk");
            Animate(false, "Run");
        }
    }

    public void Animate(bool condition, string type)
    {
        if (condition == true && type == "Walk")
        {
            _animator.SetBool("RunBool", false);
            _animator.SetBool("WalkBool", true);
        }
        if (condition == false && type == "Walk")
        {
            _animator.SetBool("WalkBool", false);
        }
        if (condition == true && type == "Run")
        {
            _animator.SetBool("WalkBool", false);
            _animator.SetBool("RunBool", true);
        }
        if (condition == false && type == "Run")
        {
            _animator.SetBool("RunBool", false);
        }
        if (condition == true && type == "LiftWalk")
        {
            _animator.SetBool("RunBool", true);
        }
        if (condition == false && type == "LiftWalk")
        {
            _animator.SetBool("RunBool", false);
        }
        if (condition == true && type == "Dying")
        {
            _animator.SetTrigger("Dying");
        }       
        if (condition == true && type == "Player_Shot")
        {
            _animator.SetTrigger("Shot");
        }
    }

    public void CancelHoldAnimation()
    {
        Animate(false, "LiftWalk");
    }
    
    public void LiftCarAnimation()
    {
        Animate(false, "LiftWalk");
    }
}
