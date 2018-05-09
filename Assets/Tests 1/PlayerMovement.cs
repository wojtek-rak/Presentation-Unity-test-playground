using UnityEngine;
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
    private bool changed = false;
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


        //startowanie w dobrej pozycji
        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> ( ( ( TEST ) ) ) <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        //controller.transform.position = new Vector3(10.0f, -50.0f, 2.0f);
        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> ( ( ( TEST ) ) ) <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<


    }


    //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> ( ( ( TEST ) ) ) <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
    public void Jump()
    {
        jump = true;
        inJump = true;
        moveDirection.y = jumpSpeed;
    }


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
            if (Physics.Raycast(bottom, new Vector3(0, -1, 0), out hit))
            {
                controller.Move(new Vector3(0, -hit.distance, 0));
                return true;
            }
        }
        
        return false;
    }

    //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> ( ( ( TEST ) ) ) <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
    void Update()
    {


        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> ( ( ( TEST ) ) ) <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

        if (!changed)
        {
            Vector3 bottom = controller.transform.position - new Vector3(0, controller.height, 0);
            RaycastHit hit;
            Debug.Log(controller.transform.position);
            Debug.Log(Physics.Raycast(bottom, new Vector3(0, -1, 0), out hit));
            if (!Physics.Raycast(bottom, new Vector3(0, -1, 0), out hit))
            {
                Physics.Raycast(bottom, new Vector3(0, 1, 0), out hit);
                Debug.Log("błąd");
                Debug.Log(hit.distance);
                controller.transform.position = new Vector3(controller.transform.position.x, controller.transform.position.y + hit.distance, controller.transform.position.z);
            }
            changed = true;
        }

        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> ( ( ( TEST ) ) ) <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<





        //if (controller.isGrounded)
        if (IsGrounded())
        {
            inJump = false;
            moveDirection = new Vector3(1, 0, 0);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }

        }
        
        //move
        moveDirection.y -= gravity * Time.smoothDeltaTime;
        controller.Move(moveDirection * Time.smoothDeltaTime);


        //for animations
        if (Input.GetMouseButtonDown(0))
        {
            shoot = true;
        }
        AnimationArcher(jump, shoot);
        jump = false;
        shoot = false;


        //Camera follow player
        playerCamera.transform.position = new Vector3(transform.position.x, transform.position.y + 10, playerCamera.transform.position.z);
    }


    //for animations
    private void AnimationArcher(bool j, bool sh)
    {
        animator.SetBool("Right", true);
        animator.SetBool("Jump", j);
        animator.SetBool("Shoot", sh);
    }

}
