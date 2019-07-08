using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public Node Parent;
    public Tile mapTile;

    public int xCoordinate;
    public int zCoordinate;

    public int hCost;
    public int gCost;

    public int GetFCost() { return gCost + hCost; }

    public Node(Tile aMapTile)
    {
        mapTile = aMapTile;
        gCost = 0;
        hCost = 0;
        xCoordinate = mapTile.gridX;
        zCoordinate = mapTile.gridZ;
    }

    public void CalculateGCost(Vector3 startingTilePos)
    {

    }

    public bool Equals(Node obj)
    {
        if(obj.mapTile.gridX == xCoordinate && obj.mapTile.gridZ == zCoordinate)
        {
            return true;
        }

        return false;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
