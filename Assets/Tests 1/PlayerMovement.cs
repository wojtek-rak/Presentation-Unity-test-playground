﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;

public class PlayerMovement : MonoBehaviour
{

    public Camera playerCamera;
    private Animator animator;
    public float speed = 12.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;
    private bool jump;
    private bool shoot;
    private bool inJump = true;
    private CharacterController controller;
    Touch touch;
    Rect buttonRect = new Rect(1, 1, 270, 100);
    private bool moved = false;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        if (playerCamera == null)
        {
            playerCamera = Camera.main;
        }

        animator = GetComponent<Animator>();
        playerCamera.transparencySortMode = TransparencySortMode.Orthographic;

        //startowanie w dobrej pozycji

        //start 
        Vector3 bottom = controller.transform.position - new Vector3(0, controller.height, 0);
        RaycastHit hit;
        Debug.Log(controller.transform.position);
        if (Physics.Raycast(bottom, new Vector3(0, -1, 0), out hit))
        {
            Debug.Log("błąd");
            Debug.Log(hit.distance);

        }
        

    }

    public void Jump()
    {
        jump = true;
        inJump = true;
        moveDirection.y = jumpSpeed;
    }
    private void AnimationArcher( bool j, bool sh)
    {
        animator.SetBool("Right", true);
        animator.SetBool("Jump", j);
        animator.SetBool("Shoot", sh);
    }
    //zmiana
    public bool IsGrounded()
    {
        if (controller.isGrounded)
        {
            return true;
        }
        Vector3 bottom = gameObject.transform.position - new Vector3(0, controller.height, 0);

        RaycastHit hit;
        if (!inJump)
        {
            if (Physics.Raycast(bottom, new Vector3(0, -1, 0), out hit, 2f))
            {
                controller.Move(new Vector3(0, -hit.distance, 0));
                return true;
            }
        }
        
        return false;
    }
    void Update()
    {
        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> ( ( ( TEST ) ) ) <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

        //Debug.Log(controller.isGrounded);

        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> ( ( ( TEST ) ) ) <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if (!buttonRect.Contains(touch.position)) shoot = true;
        }
        //ZMIANA
        //if (controller.isGrounded)
        if (IsGrounded())
        {
            inJump = false;
            //jump = Input.GetKey(KeyCode.Space);
            moveDirection = new Vector3(1, 0, 0);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;

            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }

            /*if (Input.touchCount > 0)
            {
                moveDirection.y = jumpSpeed;
            }*/

           if(jump)
            {
                Debug.Log("jump");
                inJump = true;
                moveDirection.y = jumpSpeed;
            }
        }
        

        moveDirection.y -= gravity * Time.smoothDeltaTime;
        controller.Move(moveDirection * Time.smoothDeltaTime);

        AnimationArcher(jump, shoot);
        jump = false;
        shoot = false;

        //Debug.Log(moveDirection.y);

        //After we move, adjust the camera to follow the player
        playerCamera.transform.position = new Vector3(transform.position.x, transform.position.y + 10, playerCamera.transform.position.z);
    }
}