using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sEdge
{
    public int id;              //edge ID//
    public sTile[] tiles;       //two connected tiles to an edge//
    public sCorner[] corners;   //two connected corners to an edge//

    public void SetupEdge(int eID)
    {
        id = eID;
        tiles = new sTile[2];
        corners = new sCorner[2];
    }

}
