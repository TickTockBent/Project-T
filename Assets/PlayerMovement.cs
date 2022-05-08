using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float moveSpeed = 12;
    public float gravity = -19.62f;
    public Transform groundCheck;
    public float groundDistance;
    public LayerMask groundMask;
    public float jumpHeight = 3f;

    private bool isGrounded;
    private float x;
    private float z;
    private Transform playerTransform;
    private Vector3 velocity;
    
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        playerTransform = transform;
    }

    void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 move = playerTransform.right * x + playerTransform.forward * z;
        
        if (move.magnitude > moveSpeed)
        {
            //clamp speed
            move = move.normalized * moveSpeed;
        }

        controller.Move(move * (moveSpeed * Time.deltaTime));

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
