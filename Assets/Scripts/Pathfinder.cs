using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    protected TileGenerator TileGeneratorObject;
    private List<Node> m_closedList = new List<Node>();
    private List<Node> m_openList = new List<Node>();
    private Node[] m_nodeMap;

    protected Node FinalPath(Node aStart, Node aEnd)
    {
        if(aEnd.mapTile.TerrainType == TileType.WALL || aEnd.mapTile.TerrainType == TileType.PIT)
        {
            // Not pathing to an invalid tile
            return aStart;
        }

        ClearLists();
        m_closedList.Add(aStart);
        AddValidAdjacentTiles(aStart, aEnd);
        Node currentNode = GetTileWithLowestScore();

        while(currentNode != null && currentNode != aEnd)
        {
            AddValidAdjacentTiles(currentNode, aEnd);
            currentNode = GetTileWithLowestScore();

            if(currentNode.Equals(aEnd))
            {
                return currentNode;
            }

            m_openList.Remove(currentNode);
            m_closedList.Add(currentNode);
        }

        return currentNode;
    }

    private void ClearLists()
    {
        foreach(Node n in m_openList)
        {
            n.mapTile.TerrainType = TileType.OPEN;
            n.mapTile.UpdateTileType();
        }

        m_openList.Clear();

        foreach(Node n in m_closedList)
        {
            if(n.mapTile.TerrainType == TileType.PATH)
            {
                n.mapTile.TerrainType = TileType.OPEN;
                n.mapTile.UpdateTileType();
            }
        }

        m_closedList.Clear();
    }

    private Node GetTileWithLowestScore()
    {
        Node lowestScoreNode = null;
        if (m_openList.Count > 0)
        {
            foreach(Node tile in m_openList)
            {
                if(lowestScoreNode == null || tile.GetFCost() < lowestScoreNode.GetFCost())
                {
                    lowestScoreNode = tile;
                }
            }
        }

        return lowestScoreNode;
    }

    private void HandleNode(Node aNode, Node currentNode, Node aTarget)
    {
        if (!m_closedList.Contains(aNode) && !m_openList.Contains(aNode))
        {
            if (aNode.mapTile.TerrainType != TileType.WALL)
            {
                aNode.gCost = currentNode.gCost + 10;
                aNode.hCost = CalculateHCost(aTarget, aNode);
                aNode.Parent = currentNode;
                m_openList.Add(aNode);
            }
            else
            {
                m_closedList.Add(aNode);
            }
        }
    }

    private void AddValidAdjacentTiles(Node aNode, Node aTarget)
    {
        //int maxRowIndex = aNode.mapTile.gridZ * TileGeneratorObject.NumberOfXTiles;
        int mapLength = m_nodeMap.Length;
        int index = aNode.mapTile.gridZ * TileGeneratorObject.NumberOfXTiles + aNode.mapTile.gridX;
        int adjacent;

        if (aNode.xCoordinate > 0)
        {
            adjacent = index - 1; // start with the left

            if (adjacent >= aNode.mapTile.gridZ * TileGeneratorObject.NumberOfXTiles)
            {
                HandleNode(m_nodeMap[adjacent], aNode, aTarget);
            }

            adjacent = (index - TileGeneratorObject.NumberOfZTiles) - 1; // Up-left (diagnal)
            if (adjacent >= 0)
            {
                HandleNode(m_nodeMap[adjacent], aNode, aTarget);
            }

            adjacent = index + TileGeneratorObject.NumberOfZTiles - 1; // Down-left (diagnal)
            if (adjacent < mapLength)
            {
                HandleNode(m_nodeMap[adjacent], aNode, aTarget);
            }
        }

        adjacent = index - TileGeneratorObject.NumberOfZTiles; // next is the up direction
        if (adjacent >= 0)
        {
            HandleNode(m_nodeMap[adjacent], aNode, aTarget);
        }

        if (aNode.xCoordinate < (TileGeneratorObject.NumberOfXTiles - 1))
        {
            adjacent = (index - TileGeneratorObject.NumberOfZTiles) + 1; // Up-right (diagnal)
            if (adjacent >= 0)
            {
                HandleNode(m_nodeMap[adjacent], aNode, aTarget);
            }

            adjacent = index + 1; // to the right
            HandleNode(m_nodeMap[adjacent], aNode, aTarget);

            adjacent = index + TileGeneratorObject.NumberOfZTiles + 1; // Down-right (diagnal) 
            if (adjacent < mapLength)
            {
                HandleNode(m_nodeMap[adjacent], aNode, aTarget);
            }
        }

        adjacent = index + TileGeneratorObject.NumberOfZTiles; // finally down
        if (adjacent < mapLength)
        {
            HandleNode(m_nodeMap[adjacent], aNode, aTarget);
        }
    }

    int CalculateHCost(Node aOrigin, Node aTarget)
    {
        int diffX = aTarget.mapTile.gridX - aOrigin.mapTile.gridX;
        int diffZ = aTarget.mapTile.gridZ - aOrigin.mapTile.gridZ;

        diffZ = diffZ < 0 ? diffZ * -1 : diffZ;
        diffX = diffX < 0 ? diffX * -1 : diffX;

        return diffX + diffZ;
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        TileGeneratorObject = FindObjectOfType<TileGenerator>();
        GameObject[] tileMap = TileGeneratorObject.GetTileMap();
        m_nodeMap = new Node[tileMap.Length];
        for (int i = 0; i < tileMap.Length; i++)
        {
            m_nodeMap[i] = new Node(tileMap[i].GetComponent<Tile>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
