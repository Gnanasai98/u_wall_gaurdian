using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
   
    // This method is called when the Collider exits another Collider
    public void OnTriggerExit(Collider other)
    {
        // Check if the exiting object has a specific tag, layer, or component if needed
        if (other.CompareTag("Player"))
        {
            // Perform your desired actions or callbacks here
            Debug.Log("Player has left the collider!");
        }
    }
}
