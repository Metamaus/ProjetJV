﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_controller : MonoBehaviour
{
    CharacterController characterController;

    public Camera cam;
    public float speed;
    public float jumpStrength;
    public float gravity;
    public float rotateSpeed;

    private Vector3 moveDirection = Vector3.zero;
    private float horizontal;
    private float vertical;
    private float mouse_horizontal;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        characterController = GetComponent<CharacterController>();
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        mouse_horizontal = Input.GetAxis("Mouse X");
        Debug.Log(mouse_horizontal);
        anim.SetBool("Is_Grounded", characterController.isGrounded);

        transform.Rotate(0, mouse_horizontal*rotateSpeed, 0);
        
        if (characterController.isGrounded)
        {
            // We are grounded, so recalculate
            // move direction directly from axes
            if (vertical > 0 || horizontal != 0)
            {
                anim.SetInteger("Is_Running", 1);
            }
            else if (vertical < 0)
            {
                anim.SetInteger("Is_Running", -1);
            }
            else
            {
                anim.SetInteger("Is_Running", 0);
            }

            moveDirection = transform.rotation * (new Vector3(horizontal, 0.0f, vertical));
            moveDirection *= speed;
            

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpStrength;
            }
        }
        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        moveDirection.y -= gravity * Time.deltaTime;

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);
    }

    
}
