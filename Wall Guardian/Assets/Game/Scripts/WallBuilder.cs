using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class WallBuilder : MonoBehaviour
{
    [SerializeField] Transform collisionPoint;
    // come back to this later
    public void CreateMesh(Vector3 bottomLeft,Vector3 bottomRight,Vector3 topLeft,Vector3 topRight)
    {
        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        // Define vertices
        Vector3[] vertices = new Vector3[4]
        {
            new Vector3(0, 0, 0),  // Bottom-left
            new Vector3(1, 0, 0),  // Bottom-right
            new Vector3(0, 1, 0),  // Top-left
            new Vector3(1, 1, 0)   // Top-right
        };

        // Define triangles (clockwise order)
        int[] triangles = new int[6] { 0, 2, 1, 1, 2, 3 };

        // Define UV coordinates
        Vector2[] uv = new Vector2[4]
        {
            new Vector2(0, 0),  // Bottom-left
            new Vector2(1, 0),  // Bottom-right
            new Vector2(0, 1),  // Top-left
            new Vector2(1, 1)   // Top-right
        };

        // Assign vertices, triangles, and UV to the mesh
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;

        // Recalculate normals and bounds
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            Vector2 point = (Vector2)collisionPoint.transform.position;
            Debug.Log("TriggerEnter - posX: " + point.x + ", posY: " + point.y);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            Vector2 point = (Vector2)collisionPoint.transform.position;
            Debug.Log("TriggerExit - posX: " + point.x + ", posY: " + point.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //game over
    }

}
