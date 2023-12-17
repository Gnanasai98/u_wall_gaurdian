using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //input vector
    Vector2 inputVector;
    public Vector2 InputVector { get => inputVector; set => inputVector = value; }



    //reference gameobjects
    PlayerMovement movements;
    
    //other components
    private void Start()
    {
        movements = GetComponent<PlayerMovement>();
    }
    private void Update()
    {

    }
    private void FixedUpdate()
    {
        movements.Movement(inputVector);
    }
}
