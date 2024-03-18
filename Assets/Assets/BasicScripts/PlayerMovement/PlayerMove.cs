using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 5f;
    public float sprintSpeed = 10f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;
    bool canDoubleJump = false; // New variable to track the double jump

    public InputActionAsset playerInput;

    private void Update()
    {
        // Check if on the ground
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // Reset velocity and double jump if on the ground
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Small downward force to ensure the player sticks to the ground.
            canDoubleJump = true; // Allow double jumping again once we've touched the ground
        }

        float x = playerInput.actionMaps[0].actions[0].ReadValue<Vector2>().x;
        float z = playerInput.actionMaps[0].actions[0].ReadValue<Vector2>().y;

        // Move the player
        Vector3 move = transform.right * x + transform.forward * z;

        // Check for sprinting
        if (playerInput.actionMaps[0].actions[2].ReadValue<float>() > 0)
        {
            controller.Move(move * sprintSpeed * Time.deltaTime);
        }
        else
        {
            controller.Move(move * speed * Time.deltaTime);
        }

        // Jumping
        if (playerInput.actionMaps[0].actions[3].ReadValue<float>() > 0)
        {
            if (isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
            else if (canDoubleJump) // Perform the double jump if possible
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                canDoubleJump = false; // Disable double jump until we touch the ground again
            }
        }

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}