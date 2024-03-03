using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float gravity = 20f; // Adjust this value to control gravity
    public Transform groundCheck;
    public LayerMask groundMask;

    private CharacterController controller;
    private Animator animator;
    private bool isGrounded;
    private Vector3 velocity;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Check if the player is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundMask);

        // Movement
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 moveDirection = new Vector3(horizontalInput, 0f, 0f);
        controller.Move(moveDirection * moveSpeed * Time.deltaTime);

        // Set animation
        if (horizontalInput != 0f)
        {
            animator.SetBool("walking", true);
            if (horizontalInput > 0f)
            {
                transform.localScale = new Vector3(1f, 1f, 1f); // Facing right
            }
            else
            {
                transform.localScale = new Vector3(-1f, 1f, 1f); // Facing left
            }
        }
        else
        {
            animator.SetBool("walking", false);
            
        }

        // Apply gravity
        if (isGrounded)
        {
            velocity.y = 0f;
            animator.SetBool("jumping", false);
        }
        else
        {
            velocity.y -= gravity * Time.deltaTime;
        }

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            animator.SetTrigger("jumping");
            velocity.y = jumpForce;
        }

        // Apply vertical movement
        controller.Move(velocity * Time.deltaTime);
    }
}
