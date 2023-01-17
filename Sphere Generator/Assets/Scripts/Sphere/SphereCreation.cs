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

        edgeCount = 0; 
        for(int i = 0; i < finalGrid.tiles.Count; i++)
        {
            if(finalGrid.tiles[i].edgeCount == 5)
            {
                for(int o = 0; o < finalGrid.tiles[i].corners.Count; o++)
                {
                    vertices[edgeCount] = finalGrid.tiles[i].corners[o].potision;
                    edgeCount++;
                }
            }
        }
        for(int i = 0; i < finalGrid.tiles.Count; i++)
        {
            if(finalGrid.tiles[i].edgeCount == 6)
            {
                for(int o = 0 ; o < finalGrid.tiles[i].corners.Count; o++)
                {
                    vertices[edgeCount] = finalGrid.tiles[i].corners[o].potision;
                    edgeCount++;
                }
            }
        }

        edgeCount = 0 ;
        for(int i = 0; i < finalGrid.tiles.Count; i++)
        {
            if(finalGrid.tiles[i].edgeCount == 6)
            {
                edgeCount += 12;
            }
            else
            {
                edgeCount += 9;
            }
        }
        
        indices = new int[edgeCount];
        int intCount = 0;
        int indCount = 0;
        
        for(int i = 0; i < finalGrid.tiles.Count; i++)
        {
            if(finalGrid.tiles[i].edgeCount == 5)
            {
                indices[indCount] = 0 + (intCount * 5);
                indCount++;
                indices[indCount] = 1 + (intCount * 5);
                indCount++;
                indices[indCount] = 2 + (intCount * 5);
                indCount++;
                indices[indCount] = 2 + (intCount * 5);
                indCount++;
                indices[indCount] = 3 + (intCount * 5);
                indCount++;
                indices[indCount] = 0 + (intCount * 5);
                indCount++;
                indices[indCount] = 0 + (intCount * 5);
                indCount++;
                indices[indCount] = 3 + (intCount * 5);
                indCount++;
                indices[indCount] = 4 + (intCount * 5);

                indCount++;
                intCount++;
                            
            }
        }

        int oldCount = intCount * 5;
        intCount = 0;
        for(int i = 0; i < finalGrid.tiles.Count; i++)
        {
            if(finalGrid.tiles[i].edgeCount == 6)
            {
                indices[indCount] = 0 + (intCount * 6) + oldCount;
                indCount++;
                indices[indCount] = 1 + (intCount * 6) + oldCount;
                indCount++;
                indices[indCount] = 4 + (intCount * 6) + oldCount;
                indCount++;

                indices[indCount] = 1 + (intCount * 6) + oldCount;
                indCount++;
                indices[indCount] = 2 + (intCount * 6) + oldCount;
                indCount++;
                indices[indCount] = 3 + (intCount * 6) + oldCount;
                indCount++;

                indices[indCount] = 1 + (intCount * 6) + oldCount;
                indCount++;
                indices[indCount] = 3 + (intCount * 6) + oldCount;
                indCount++;
                indices[indCount] = 4 + (intCount * 6) + oldCount;
                indCount++;

                indices[indCount] = 0 + (intCount * 6) + oldCount;
                indCount++;
                indices[indCount] = 4 + (intCount * 6) + oldCount;
                indCount++;
                indices[indCount] = 5 + (intCount * 6) + oldCount;
                indCount++;
                intCount++;
            }
        }

        Mesh mesh = new Mesh();
        if(!transform.GetComponent<MeshFilter>() || !transform.GetComponent<MeshRenderer>())
        {
            transform.gameObject.AddComponent<MeshFilter>();
            transform.gameObject.AddComponent<MeshRenderer>();
        }

        transform.GetComponent<MeshFilter>().mesh = mesh;

        mesh.name = "MyOwnObject";
        mesh.vertices = vertices;
        mesh.triangles = indices;
        mesh.RecalculateNormals();
        mesh.Optimize();
    }
}
