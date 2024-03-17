using System.Collections.Generic;
using System.Drawing;
using UnityEditor.VersionControl;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using static UnityEngine.Rendering.DebugUI.Table;

public class WallBuilder : MonoBehaviour
{
    [SerializeField] private List<Vector2> inputVertices;
    [SerializeField] private Material material;
    [SerializeField] private GameObject vertexobj;


    //Raycast 
    [SerializeField] LayerMask wallMask;
    public Transform startPoint1, startPoint2,startPointC;
    [SerializeField] float rayLength;


    private bool canBuildWall = false;
    private bool wasHittingWall = false; // Variable to track previous state

    public List<Vector2> InputVertices { get => inputVertices; set => inputVertices = value; }
    public bool CanBuildWall { get => canBuildWall; set => canBuildWall = value; }
   
    private void Start()
    {
    }
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

    public void GenerateLineCast()
    {
        bool hit1 = Physics2D.Linecast(startPoint1.position, startPoint1.position + transform.right * -rayLength, wallMask);
        bool hit2 = Physics2D.Linecast(startPoint1.position, startPoint2.position + transform.right * -rayLength, wallMask);
        bool hitC = Physics2D.Linecast(startPointC.position, startPointC.position + transform.right * -rayLength, wallMask);
        bool isHittingWall = hit1 || hit2 || hitC; // Combine hit checks

        if (isHittingWall && !wasHittingWall) // Check if just started hitting the wall
        {
            EndingPoint();
        }
        else if (!isHittingWall && wasHittingWall) // Check if just stopped hitting the wall
        {
            StartingPoint();
        }

        wasHittingWall = isHittingWall; // Update previous state
    }

    private void EndingPoint()
    {
        Vector2 endPoint = TransformUtilities.getRoundPoint(new Vector2(startPointC.position.x, startPointC.position.y));
        canBuildWall = false;
        inputVertices.Add(endPoint);
        string logString = "Input vertices:";
        inputVertices.ForEach(vertex =>
        {
            logString += " , " + vertex;
            GameObject result = Instantiate(vertexobj, vertex, Quaternion.identity);
        });
        Debug.Log(logString);
    }

    private void StartingPoint()
    {
        Quaternion startPointCRot = startPointC.transform.rotation;
        Debug.Log(startPointCRot.eulerAngles);
        Vector2 startPoint = TransformUtilities.getRoundPoint(new Vector2(startPointC.position.x, startPointC.position.y - rayLength));
        canBuildWall = true;
        inputVertices.Clear();
        if (canBuildWall)
            inputVertices.Add(startPoint);
    }

    private void OnDrawGizmos()
    {
        // Draw the line segments in the Scene view using Gizmos
        Gizmos.color = UnityEngine.Color.green;
        Gizmos.DrawLine(startPoint1.position, startPoint1.position + transform.right * -rayLength);
        Gizmos.DrawLine(startPoint2.position, startPoint2.position + transform.right * -rayLength);
        Gizmos.DrawLine(startPointC.position, startPointC.position + transform.right * -rayLength);

    }
   
   
}
