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
        finalGrid = 
    }

    sHexGrid size_n_grid(int size)
    {
        if(size == 0)
        {
            return CreateZeroSizeSphere();
        }
        else
        {
            return CreateSubdividedSphere(size_n_grid( -1));
        }
    }

    void CreateObjects()
    {
        for(int i = 0; i < finalGrid.tiles.Count; i++)
        {
            Debug.Log("i: " + i);
            var go = Instantiate(tilePrefab);
            go.transform.parent = tileParent.transform;
            go.GetComponent<HexagonTile>().SetupTile(finalGrid.tiles[i].position, finalGrid.tiles[i].corners, finalGrid.tiles[i]);
        }
    }

    void CreateMesh()
    {
        int edgeCount = 0;
        for(int i = 0 ; i < finalGrid.tiles.Count; i++)
        {
            if(finalGrid.tiles[i].edgeCount == 6)
            {
                edgeCount += 6; 
            }
            else
            {
                edgeCount += 5;
            }
        }

        vertices = new Vector3[edgeCount];
    }
}
