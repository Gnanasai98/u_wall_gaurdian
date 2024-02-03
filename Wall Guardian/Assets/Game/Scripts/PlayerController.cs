using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Define the event
    public delegate void InputVectorChangedEventHandler(Vector2 newInputVector);
    public event InputVectorChangedEventHandler OnInputVectorChanged;

    //Inputs
    Vector2 inputVector;
    public Vector2 InputVector 
    { get => inputVector; 
        set
        {
            inputVector = value;
            if (wallBuilder.CanBuildWall) {
                List<Vector2> currentVertices = wallBuilder.InputVertices;
                currentVertices.Add(transform.position);
                wallBuilder.InputVertices = currentVertices;
            }
            
        }
    }



    //reference gameobjects
    PlayerMovement movements;
    WallBuilder wallBuilder;
    
    //other components
    private void Start()
    {
        movements = GetComponent<PlayerMovement>();
        wallBuilder = GetComponent<WallBuilder>();  
    }
    private void Update()
    {

    }
    private void FixedUpdate()
    {
        movements.Movement(inputVector);
    }
}
