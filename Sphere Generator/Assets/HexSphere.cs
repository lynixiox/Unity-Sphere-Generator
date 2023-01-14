using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexSphere : MonoBehaviour
{
    public int iterations;
    public float radius = 1f;

    private Vector3[] vertices;
    private int[] triangles;

    // Start is called before the first frame update
    void Start()
    {
        //Create the Game object to assign the sphere too//
        GameObject sphere = new GameObject("Sphere");

        //add a mesh filter and mesh renderer to the game object//
        MeshFilter filter = sphere.AddComponent<MeshFilter>();
        MeshRenderer renderer = sphere.AddComponent<MeshRenderer>();

        //Create a new mesh and assign it to the mesh filter//
        Mesh mesh = filter.mesh;

        //Initialize the arrays for vertices and triangles//
        Initialize();

        //Subdivide the sphere by the given number of iterations//
        for(int i = 0; i < iterations; i++)
        {
            Subdivide();
        }

        //assign the vertices and triangles to the mesh//
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        //Scale the sphere to the desired radius//
        sphere.transform.localScale = Vector3.one * radius;

        //Recalculate the normals and bounds of the mesh//
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        
    }

    private void Initialize()
    {

        int numVerticies = 12;
        int numTriangles = 30;
        //Create and Array of Vector3 Verticies for the sphere//
        vertices = new Vector3[numVerticies];
        for(int i = 0; i < numVerticies; i++)
        {
            float angle = i * Mathf.PI * 2f / numVerticies;
            vertices[i]= new Vector3(Mathf.Cos(angle), 0f, Mathf.Sin(angle));

        }

        //Create an array of int triangles for the sphere//
        triangles = new int[numTriangles];
        for(int i = 0; i < numVerticies; i++)
        {
            Debug.Log(i);
            triangles[i * 3] = i ;
            triangles[i * 3 + 1] = (i+1) % numVerticies;
            triangles[i * 3 + 2] = numVerticies;

        }
    }

    private void Subdivide()
    {
        int numVertices = vertices.Length;
        int numTriangles = triangles.Length;

        Vector3[] newVertices = new Vector3[numVertices + numTriangles];
        int[] newTriangles = new int[numTriangles * 4 ];

        for(int i = 0; i < numTriangles; i+= 3)
        {
            //Get te tree Verticies of the triangle//
            Vector3 v1 = vertices[triangles[i]];
            Vector3 v2 = vertices[triangles[i + 1]];
            Vector3 v3 = vertices[triangles[i + 2]];

            //Create three new vertices at the at the midpoints of the tirangle edges//
            Vector3 v4 = (v1 + v2) / 2.0f;
            Vector3 v5 = (v2 + v3) / 2.0f;
            Vector3 v6 = (v3 + v1) / 2.0f;

            //Normalize the new vertices (this is necessary to ensure that the sphere is a unit sphere)//
            v4.Normalize();
            v5.Normalize();
            v6.Normalize();

            //Assign th enew vertices to the newVertices array//
            newVertices[numVertices + i] = v4;
            newVertices[numVertices + i + 1] = v5;
            newVertices[numVertices + i + 2] = v6;

            //Assign the new triangels to the newTriangles array//
            newTriangles[i * 4] = triangles[i];
            newTriangles[i * 4 + 1] = numVertices + i;
            newTriangles[i * 4 + 2] = numVertices + i + 2;

            newTriangles[i * 4 + 3] = numVertices + i;
            newTriangles[i * 4 + 4] = triangles[i + 1];
            newTriangles[i * 4 + 5] = numVertices + i + 1;

            newTriangles[i * 4 + 6] = numVertices + i + 1;
            newTriangles[i * 4 + 7] = triangles[i + 2];
            newTriangles[i * 4 + 8] = numVertices + i + 2;

            newTriangles[i * 4 + 9] = numVertices + i;
            newTriangles[i * 4 + 10] = numVertices + i + 1;
            newTriangles[i * 4 + 11] = numVertices + i + 2;
        }

        //Replace the old vertices and the triangles with the new ones//
        vertices = newVertices;
        triangles = newTriangles;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
