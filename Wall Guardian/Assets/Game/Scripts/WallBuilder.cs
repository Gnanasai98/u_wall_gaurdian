using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBuilder : MonoBehaviour
{
    void CreateMesh(Vector3 bottomLeft,Vector3 bottomRight,Vector3 topLeft,Vector3 topRight)
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
}