using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    // This method is called when the Collider exits another Collider
    private void OnTriggerExit2D(Collider2D other)
    {
        // Check if the exiting object has a specific tag, layer, or component if needed
        if (other.gameObject.CompareTag("Player"))
        {
            // Perform your desired actions or callbacks here
            Debug.Log("Player has left the collider!");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the entering object has a specific tag, layer, or component if needed
        if (other.gameObject.CompareTag("Player"))
        {
            // Perform your desired actions or callbacks here
            Debug.Log("Player has entered the collider!");
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            // Perform your desired actions or callbacks here
            Debug.Log("Enemy has collided with the wall!");
        }
    }
}
