using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sCorner
{
    public int id;                  //corner ID//
    public Vector3 potision;        //corner position//
    public sTile[] tiles;           //adjacent tiles//
    public sCorner[] corners;       //connected corners//
    public sEdge[] edges;           //adjacent edges//
    public float height;

    public void SetupCorner(int cID)
    {
        id = cID;
        tiles = new sTile[3];
        corners = new sCorner[3];
        edges = new sEdge[3];
    }
}
