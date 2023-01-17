using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereCreation : MonoBehaviour
{

    public GameObject tilePrefab;
    public GameObject tileParent;

    /*
    *   Spheres are easier to create from trianglular points
    *   we need
    *   edges
    *   Corners
    *   Tiles
    */
    
    //Gird is also known as a net when creating the truncarted icosahedron
    public sHexGrid finalGrid;
    public int size = 0;
    public Vector3[] vertices;
    public int[] indices;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
