using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MovementController : MonoBehaviourPunCallbacks
{
    Animator animator;
    bool isSprinting = false;
    float speed= 0.25f;
    float inputY;
    float inputX;
    Camera addedCamera;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        addedCamera = gameObject.GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            Move();
            Look();
        }
    }

    private void Move()
    {
        inputY = Input.GetAxis("Vertical");
        inputX = Input.GetAxis("Horizontal");
        animator.SetFloat("InputY", inputY);
        animator.SetFloat("InputX", inputX);

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isSprinting = !isSprinting;
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (isSprinting)
            {
                speed = 0.9f;
            }
            else
            {
                speed = 0.5f;
            }
        }
        else
        {
            if (isSprinting)
            {
                speed = 0.4f;
            }
            else
            {
                speed = 0.25f;
            }

        }
        animator.SetFloat("Speed", speed);

        if (inputY > 0)
        {
            transform.position += transform.forward * (speed* 2) * Time.deltaTime;
        }
        else if (inputY < 0)
        {
            transform.position += -transform.forward * (speed * 2) * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("isAttacking", true);
        }
        else
        {
            animator.SetBool("isAttacking", false);
        }
    }
    private void Look()
    {
        float rotationSpeed = 90f;
        transform.Rotate(0, Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime, 0, Space.World);
    }
}
