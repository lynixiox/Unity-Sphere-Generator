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
        
        for(int i = 0; i < self.tiles.Count; i++)
        {
            connectedTiles.Add(self.tiles[i].id);
        }

        height = self.height;

        SetupMesh(corners);


    }

    public void SetupMesh(List<sCorner> corners)
    {
        Vector3[] vertices = new Vector3[corners.Count];
        for(int i = 0 ; i < corners.Count; i++)
        {
            vertices[i] = corners[i].potision - transform.position;
        }
        int[] indicies;
        if(corners.Count == 6)
        {
            indicies = new int[12];
        }
        else
        {
            indicies = new int[9];
        }

        if(corners.Count == 6)
        {
            indicies[0] = 0;
            indicies[1] = 1;
            indicies[2] = 2;

            indicies[3] = 0;
            indicies[4] = 2;
            indicies[5] = 3; 

            indicies[6] = 0;
            indicies[7] = 3;
            indicies[8] = 4;

            indicies[9] = 5;
            indicies[10] = 3;
            indicies[11] = 4;
        }
        else
        {
            indicies[0] = 0;
            indicies[1] = 1;
            indicies[2] = 2;

            indicies[3] = 0;
            indicies[4] = 2;
            indicies[5] = 3; 

            indicies[6] = 0;
            indicies[7] = 3;
            indicies[8] = 4;
        }

        normal = Vector3.Cross(vertices[1] - vertices[0], vertices[1] - vertices[3]).normalized;
        rotation = Quaternion.LookRotation(normal);
        
        //add the mesh component to the game object//
        Mesh mesh = new Mesh();
        if(!transform.GetComponent<MeshFilter>() || !transform.GetComponent<MeshRenderer>())
        {
            transform.gameObject.AddComponent<MeshFilter>();
            transform.gameObject.AddComponent<MeshRenderer>();
        }

        transform.GetComponent<MeshFilter>().mesh = mesh;
        transform.GetComponent<MeshCollider>().sharedMesh = transform.GetComponent<MeshFilter>().mesh;

        mesh.name = "HexTileObject";
        mesh.vertices = vertices;
        mesh.triangles = indicies;
        mesh.RecalculateNormals();
        mesh.Optimize();
    }
}
