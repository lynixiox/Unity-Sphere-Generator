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

    sHexGrid CreateZeroSphere()
    {
        //Start all spheres with a zero size sphere to ease tile placement and then just increase them later//
        Debug.Log("step 1");
        //starting points//
        sHexGrid grid = new sHexGrid();
        grid.SetupHexGrid(0);
        Debug.Log("Step 2");
        float x = -0.525731112119133606f;
        float z = -0.850650808352039932f;

        Debug.Log("Step 3");
        Vector3[] icosahedronTiles = //List of icosahedron tile placement// 
        {
            new Vector3(-x, 0, z), new Vector3(x, 0, z), new Vector3(-x, 0, -z), new Vector3(x, 0, -z),
            new Vector3(0, z, x), new Vector3(0, z, -x), new Vector3(0, -z, x), new Vector3(0, -z, -x),
            new Vector3(z, x, 0), new Vector3(-z, x, 0), new Vector3(z, -x, 0), new Vector3(-z, -x, 0)    
        };

        Debug.Log("Step 4");
        int[,] icosahedronTileNumbers = //List of icosahedron tile numbering//
        {
            {9, 4, 1, 6, 11}, {4, 8, 10, 6, 0}, {11, 7, 3, 5, 9}, {2, 7, 10, 8, 5},
            {9, 5, 8, 1, 0}, {2, 3, 8, 4, 9}, {0, 1, 10, 7, 11}, {11, 6, 10, 3, 2},
            {5, 3, 10, 1, 4}, {2, 5, 4, 0, 11}, {3, 7, 6, 1, 8}, {7, 2, 9, 0, 6}
        };

        Debug.Log("Step 5");
        foreach(sTile t in grid.tiles)
        {
            t.position = icosahedronTiles[t.id];
            for(int i = 0; i < 5; i++)
            {
                t.tiles[i] = grid.tiles[icosahedronTileNumbers[t.id, i]];
            }
        }
    }
}
