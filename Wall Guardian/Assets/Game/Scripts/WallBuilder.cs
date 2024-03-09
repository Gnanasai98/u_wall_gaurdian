using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class WallBuilder : MonoBehaviour
{
    [SerializeField] private List<Vector2> inputVertices;
    [SerializeField] private Material material;
    private bool canBuildWall = false;

    public List<Vector2> InputVertices { get => inputVertices; set => inputVertices = value; }
    public bool CanBuildWall { get => canBuildWall; set => canBuildWall = value; }

    // come back to this later
    public void CreateMesh(List<Vector2> inputVertices)
    {
        // Create a new GameObject
        GameObject meshObject = new GameObject("GeneratedMesh");

        // Add MeshFilter component
        MeshFilter meshFilter = meshObject.AddComponent<MeshFilter>();

        // Add MeshRenderer component
        MeshRenderer meshRenderer = meshObject.AddComponent<MeshRenderer>();

        // Set the material (you can assign your own material)
        meshRenderer.material = material;

        foreach (Vector2 vertex in inputVertices)
        {
            Debug.Log(vertex);
        }
        Mesh mesh = new Mesh();

        // Define vertices
        Vector3[] vertices = new Vector3[inputVertices.Count];
        // Copy elements from inputVertices to vertices array
        for (int i = 0; i < inputVertices.Count; i++)
        {
            vertices[i] = new Vector3(inputVertices[i].x, inputVertices[i].y, 0f);
        }

        // Define UV coordinates
        Vector2[] uv = new Vector2[inputVertices.Count];
        // You may want to calculate UV coordinates based on your specific requirements
        // For simplicity, let's just set UV coordinates to the same values as inputVertices
        for (int i = 0; i < inputVertices.Count; i++)
        {
            uv[i] = inputVertices[i];
        }

        
        // Define triangles (clockwise order)
        int[] triangles = new int[(int)inputVertices.Count * 3];

        for (int i = 0; i < inputVertices.Count; i++)
        {
            triangles[i * 3] = i;                // Current vertex
            triangles[i * 3 + 1] = (i + 1) % inputVertices.Count; // Next vertex (wrapping around to the first vertex)
            triangles[i * 3 + 2] = (i + 2) % inputVertices.Count; // Vertex after the next one
        }

        // Assign vertices, triangles, and UV to the mesh
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;

        // Recalculate normals and bounds
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        meshFilter.mesh = mesh;
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            Vector2 point = getCollsionPoint();
            if(canBuildWall)
                inputVertices.Add(point);
            canBuildWall = false;
            CreateMesh(inputVertices);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
           Vector2 point = getCollsionPoint();
            inputVertices.Clear();
            inputVertices.Add(point);
            canBuildWall = true;
        }
    }

    private Vector2 getCollsionPoint()
    {
        float roundedPosX, roundedPosY;
        roundedPosX = Mathf.Round(transform.position.x * 1000f) / 1000f;
        roundedPosY = Mathf.Round(transform.position.y * 1000f) / 1000f;
        return new Vector2(roundedPosX, roundedPosY);
    }

    //Enemy detection game over
    private void OnCollisionEnter2D(Collision2D other)
    {
        //game over
    }

}
