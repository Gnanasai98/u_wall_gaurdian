using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInputs : MonoBehaviour
{
    private float minX, maxX, minY, maxY;
    private Camera mainCamera;
    PlayerController controller;
    void Start()
    {
        mainCamera = Camera.main;
        controller = GetComponent<PlayerController>();
    }

    void Update()
    {
        CalculateCameraBounds();
        ClampToCameraBounds();
        controller.InputVector = HandleInput();

    }
    Vector2 HandleInput()
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
        return inputVector;
    }
    void CalculateCameraBounds()
    {
        if (mainCamera != null)
        {
            // Calculate camera boundaries in world coordinates
            minX = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
            maxX = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
            minY = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
            maxY = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
        }
    }
    void ClampToCameraBounds()
    {
        // Clamp the player's position within the camera bounds
        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
        float clampedY = Mathf.Clamp(transform.position.y, minY, maxY);

        // Update the player's position
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }

}
