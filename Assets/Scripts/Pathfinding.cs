using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    private TileGenerator TileGeneratorObject;
    private List<Node> m_closedList = new List<Node>();
    private List<Node> m_openList = new List<Node>();
    private Node[] m_nodeMap;

    List<Node> FinalPath(Node aStart, Node aEnd)
    {
        m_closedList.Add(aStart);
        AddValidAdjacentTiles(aStart, aEnd);

        while (true)
        {
        }

        return m_openList;
    }

    private void AddValidAdjacentTiles(Node aNode, Node aTarget)
    {
        int mapLength = m_nodeMap.Length;
        int index = aNode.mapTile.gridX * TileGeneratorObject.NumberofZTiles + aNode.mapTile.gridZ;
        int adjacent = index - 1; // start with the left
        Node currentNode;

        if (adjacent >= 0)
        {
            currentNode = m_nodeMap[adjacent];
            if (currentNode.mapTile.TerrainType != TileType.WALL)
            {
                currentNode.gCost += 10;
                m_openList.Add(currentNode);
            }
            else
            {
                m_closedList.Add(currentNode);
            }
        }

        adjacent = index - TileGeneratorObject.NumberofZTiles; // next is the up direction
        if (adjacent >= 0)
        {
            currentNode = m_nodeMap[adjacent];
            if (currentNode.mapTile.TerrainType != TileType.WALL)
            {
                currentNode.gCost += 10;
                m_openList.Add(currentNode);
            }
            else
            {
                m_closedList.Add(currentNode);
            }
        }

        adjacent = index + 1; // then to the right
        if (adjacent <= mapLength)
        {
            currentNode = m_nodeMap[adjacent];
            if (currentNode.mapTile.TerrainType != TileType.WALL)
            {
                currentNode.gCost += 10;
                m_openList.Add(currentNode);
            }
            else
            {
                m_closedList.Add(currentNode);
            }
        }

        adjacent = index + TileGeneratorObject.NumberofZTiles; // finally down
        if (adjacent <= mapLength)
        {
            currentNode = m_nodeMap[adjacent];
            if (currentNode.mapTile.TerrainType != TileType.WALL)
            {
                currentNode.gCost += 10;
                m_openList.Add(currentNode);
            }
            else
            {
                m_closedList.Add(currentNode);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
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
