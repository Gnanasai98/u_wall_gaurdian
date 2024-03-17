using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
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
                currentVertices.Add(TransformUtilities.getRoundPoint(wallBuilder.startPointC.transform.position));
                wallBuilder.InputVertices = currentVertices;
                Debug.Log(TransformUtilities.getRoundPoint(wallBuilder.startPointC.transform.position));
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
        wallBuilder.GenerateLineCast();

        movements.Movement(inputVector);
    }   
}
