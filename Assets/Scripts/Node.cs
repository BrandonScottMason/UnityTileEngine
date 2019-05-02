﻿using System.Collections;
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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
