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

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
