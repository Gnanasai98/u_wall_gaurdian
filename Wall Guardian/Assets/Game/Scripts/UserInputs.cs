using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UserInputs : MonoBehaviour
{
    private float minX, maxX, minY, maxY;
    private Camera mainCamera;
    PlayerController controller;
    Vector2 currentInput;
    private Vector2 lastInput = Vector2.zero;


    private Renderer noseRenderer;
    public float animationInterval = .1f; // Adjust the interval as needed
    private bool isVisible = true;

    void Start()
    {
        mainCamera = Camera.main;
        controller = GetComponent<PlayerController>();
        noseRenderer = GameObject.FindWithTag("nose").GetComponent<Renderer>();
        InvokeRepeating("ToggleVisibility", 2f, .5f);

    }

    void Update()
    {
        CalculateCameraBounds();
        ClampToCameraBounds();
        HandleInput();
        if (currentInput != HandleInput()) {
            currentInput = HandleInput();
            controller.InputVector = HandleInput();
        }

    }

    Vector2 HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            lastInput = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            lastInput = Vector2.down;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            lastInput = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            lastInput = Vector2.right;
        }

        // Normalize the input vector to prevent faster movement when moving diagonally
        Vector2 inputVector = lastInput.normalized;
       // Debug.Log(inputVector.x + " , " + inputVector.y);
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


     private void ToggleVisibility()
    {
        isVisible = !isVisible;
        noseRenderer.enabled = isVisible;
    }
}
