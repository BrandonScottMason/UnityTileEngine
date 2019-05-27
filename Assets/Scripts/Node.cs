using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public Node Parent;
    public Tile mapTile;

    public int hCost;
    public int gCost;

    public int GetFCost() { return gCost + hCost; }

    public Node(Tile aMapTile)
    {
        mapTile = aMapTile;
        gCost = 0;
        hCost = 0;
    }

    public void CalculateGCost(Vector3 startingTilePos)
    {

    }

    public override bool Equals(object obj)
    {
        return this.Equals((Node)(obj));
    }

    public bool Equals(Node obj)
    {
        if(obj.mapTile.gridX == mapTile.gridX && obj.mapTile.gridZ == mapTile.gridZ)
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
