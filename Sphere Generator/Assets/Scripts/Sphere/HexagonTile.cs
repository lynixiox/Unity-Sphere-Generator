using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonTile : MonoBehaviour
{
    public int ID;
    public List<int> connectedTiles;
    public Vector3 position;
    public Vector3 normal;
    public Quaternion rotation;
    public float height;

    public void SetupTile(Vector3 pos, List<sCorner> corners, sTile self)
    {

        position = pos;
        transform.position = pos;
        ID = self.id;
        connectedTiles = new List<int>();
        for (int i = 0; i < self.tiles.Count; i++)
            connectedTiles.Add(self.tiles[i].id);



        height = self.height;

        SetupMesh(corners);
      
    }
    public void SetupMesh(List<sCorner> corners)
    {
        Vector3[] vertices = new Vector3[corners.Count];
        for (int i = 0; i < corners.Count; i++)
        {
            vertices[i] = corners[i].position - transform.position;
        }
        int[] indices;
        if (corners.Count == 6)
            indices = new int[12];
        else
            indices = new int[9];

        if (corners.Count == 6)
        {
            indices[0] = 0;
            indices[1] = 1;
            indices[2] = 2;

            indices[3] = 0;
            indices[4] = 2;
            indices[5] = 3;

            indices[6] = 0;
            indices[7] = 3;
            indices[8] = 5;

            indices[9] = 5;
            indices[10] = 3;
            indices[11] = 4;

        }
        else
        {
            indices[0] = 0;
            indices[1] = 1;
            indices[2] = 2;

            indices[3] = 0;
            indices[4] = 2;
            indices[5] = 3;

            indices[6] = 0;
            indices[7] = 3;
            indices[8] = 4;
        }

        normal = Vector3.Cross(vertices[1] - vertices[0], vertices[1] - vertices[3]).normalized;
     ////   normal = vertices[0] + vertices[1] + vertices[2];
       // normal /= 3.0f;
      //  normal.Normalize();
        rotation = Quaternion.LookRotation(normal);
     //   rotation *= Quaternion.Euler(-90f, 0, 0);
    //    this.transform.rotation = Quaternion.LookRotation(normal) ;
//
        Mesh mesh = new Mesh();
        if (!transform.GetComponent<MeshFilter>() || !transform.GetComponent<MeshRenderer>()) //If you havent got any meshrenderer or filter
        {
            transform.gameObject.AddComponent<MeshFilter>();
            transform.gameObject.AddComponent<MeshRenderer>();
        }
        transform.GetComponent<MeshFilter>().mesh = mesh;
        transform.GetComponent<MeshCollider>().sharedMesh = transform.GetComponent<MeshFilter>().mesh;
        mesh.name = "MyOwnObject";

        mesh.vertices = vertices;
        mesh.triangles = indices;
        // mesh.uv = UV_MaterialDisplay;
        mesh.RecalculateNormals();
        mesh.Optimize();
    }
}
