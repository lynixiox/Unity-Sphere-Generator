using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sTileMaths 
{

    public static int CalculatePosition(sTile t, sTile n)
    {
        for(int i = 0 ; i < t.edgeCount; i++)
        {
            if(t.tiles[i] == n)
            {
                return i;
            }
        }

        return -1;
    }


    public static int CalculatePosition(sCorner c, sCorner n)
    {
        for(int i = 0; i < 3; i++)
        {
            if(c.corners[i] == n)
            {
                return i;
            }
        }

        return -1;
    }

    public static int CalculatePosition(sTile t, sCorner c)
    {
        for(int i = 0; i < t.edgeCount; i++)
        {
            if(t.corners[i] == c)
            {
                return i;
            }
        }
        return -1;
    }
}
