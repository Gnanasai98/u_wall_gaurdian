using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private float runSpeed =20f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        float horizontalInput = 0f;
        float verticalInput = 0f;

        if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            verticalInput = 1f;
        }
        else if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
        {
            verticalInput = -1f;
        }
        else if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            horizontalInput = -1f;
        }
        else if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            horizontalInput = 1f;
        }

        // Normalize the input vector to prevent faster movement when moving diagonally
        Vector2 inputVector = new Vector2(horizontalInput, verticalInput).normalized;

        Movement(inputVector);
    }


    void Movement(Vector2 inputVector)
    {
        Vector2 moveDirection = inputVector;

        if (moveDirection != Vector2.zero)
        {
            // Move the player using Rigidbody
            Vector2 moveVector = moveDirection * runSpeed * Time.deltaTime;
            rb.MovePosition(rb.position + moveVector);

            // Rotate the player to face the movement direction
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}
