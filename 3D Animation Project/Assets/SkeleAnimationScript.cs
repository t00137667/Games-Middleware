using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeleAnimationScript : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            animator.SetBool("isWalkingForwards", true);
        }
        else
        {
            animator.SetBool("isWalkingForwards", false);
        }
        if (Input.GetKey(KeyCode.S))
        {
            animator.SetBool("isWalkingBackwards", true);
        }
        else
        {
            animator.SetBool("isWalkingBackwards", false);
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }
}
