using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sTile
{
    //Tile Variables//
    [SerializeField]
    public int id;                      //Tile id//
    public int edgeCount;               //edge count//
    public Vector3 position;            //Tile position//
    public List<sTile> tiles;           //pList of tiles//
    public List<sCorner> corners;       //plist of corners//
    public List<sEdge> edges;           //pList of edges//


    public void TileSetup(int tID, int tEdgeCount)
    {
        id = tID;
        edgeCount = tEdgeCount;

        //Create List
        tiles = new List<sTile>();
        corners = new List<sCorner>();
        edges = new List<sEdge>();
    
        //Fill the lists with null values so we can acess their space without worrying about out of bounds exceptions//
        for(int i = 0; i < edgeCount; i++)
        {
            tiles.Add(null);
            corners.Add(null);
            edges.Add(null);
        }
    }
}
