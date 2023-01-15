using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexSphere : MonoBehaviour
{
     public int subdivisions = 1;
    private float PHI = (1 + Mathf.Sqrt(5))/2;
    private Vector3[] vertices;
    private int[] triangles;

    private void Start()
    {
        Initialize();
        for (int i = 0; i < subdivisions; i++)
        {
            Subdivide();
        }
        CreateMesh();
    }

    private void Initialize()
    {
        // Initialize the vertices
        vertices = new Vector3[]
        {
            new Vector3(-1,  0,  PHI),
            new Vector3( 1,  0,  PHI),
            new Vector3(-1,  0, -PHI),
            new Vector3( 1,  0, -PHI),
            new Vector3( 0,  PHI,  1),
            new Vector3( 0,  PHI, -1),
            new Vector3( 0, -PHI,  1),
            new Vector3( 0, -PHI, -1),
            new Vector3( PHI,  1,  0),
            new Vector3(-PHI,  1,  0),
            new Vector3( PHI, -1,  0),
            new Vector3(-PHI, -1,  0)
        };

        // Normalize the vertices
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i].Normalize();
        }

        // Initialize the triangles
        triangles = new int[]
        {
             0, 1, 4,
            1, 9, 4,
            4, 9, 5,
            4, 5, 8,
            4, 8, 1,
            8, 10, 1,
            8, 3, 10,
            5, 3, 8,
            5, 2, 3,
            2, 7, 3,
            7, 10, 3,
            7, 6, 10,
            7, 11, 6,
            11, 0, 6,
            0, 1, 6,
            6, 1, 10,
            9, 0, 11,
            9, 11, 2,
            9, 2, 5,
            7, 2, 11
        };
    }

     private void Subdivide()
    {
        int numVertices = vertices.Length;
        int numTriangles = triangles.Length / 3;

        Vector3[] newVertices = new Vector3[numVertices + numTriangles];
        int[] newTriangles = new int[numTriangles * 4];

        int newVertexIndex = numVertices;
        int newTriangleIndex = 0;
        for (int i = 0; i < numTriangles; i++)
        {
            // Get the three vertices of the triangle
            Vector3 v1 = vertices[triangles[i * 3]];
            Vector3 v2 = vertices[triangles[i * 3 + 1]];
            Vector3 v3 = vertices[triangles[i * 3 + 2]];

            // Create three new vertices at the midpoints
               Vector3 v4 = (v1 + v2) / 2.0f;
            Vector3 v5 = (v2 + v3) / 2.0f;
            Vector3 v6 = (v3 + v1) / 2.0f;

            // Normalize the new vertices (this is necessary to ensure that the truncated icosahedron is a unit sphere)
            v4.Normalize();
            v5.Normalize();
            v6.Normalize();

            // Assign the new vertices to the newVertices array
            newVertices[newVertexIndex++] = v4;
            newVertices[newVertexIndex++] = v5;
            newVertices[newVertexIndex++] = v6;

            // Assign the new triangles to the newTriangles array
            newTriangles[newTriangleIndex++] = triangles[i * 3];
            newTriangles[newTriangleIndex++] = numVertices + i;
            newTriangles[newTriangleIndex++] = numVertices + i + 2;

            newTriangles[newTriangleIndex++] = triangles[i * 3 + 1];
            newTriangles[newTriangleIndex++] = numVertices + i + 1;
            newTriangles[newTriangleIndex++] = numVertices + i;

            newTriangles[newTriangleIndex++] = triangles[i * 3 + 2];
            newTriangles[newTriangleIndex++] = numVertices + i + 2;
            newTriangles[newTriangleIndex++] = numVertices + i + 1;

            newTriangles[newTriangleIndex++] = numVertices + i;
            newTriangles[newTriangleIndex++] = numVertices + i + 1;
            newTriangles[newTriangleIndex++] = numVertices + i + 2;
        }

        // Replace the old vertices and triangles with the new ones
        vertices = newVertices;
        triangles = newTriangles;
    }

    private void CreateMesh()
    {
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        MeshFilter meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = mesh;
    }

}
