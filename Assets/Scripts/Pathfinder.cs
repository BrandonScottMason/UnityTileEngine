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
        m_closedList.Add(aStart);
        AddValidAdjacentTiles(aStart, aEnd);
        Node currentNode = GetTileWithLowestScore();

        while(currentNode != aEnd)
        {
            AddValidAdjacentTiles(currentNode, aEnd);
            currentNode = GetTileWithLowestScore();
            m_openList.Remove(currentNode);
            m_closedList.Add(currentNode);
        }

        return currentNode;
    }

    private Node GetTileWithLowestScore()
    {
        Node lowestScoreNode = m_openList[0];
        for(int i = 1; i < m_openList.Count; i++)
        {
            if(m_openList[i].GetFCost() < lowestScoreNode.GetFCost())
            {
                lowestScoreNode = m_openList[i];
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
        int mapLength = m_nodeMap.Length;
        int index = aNode.mapTile.gridX * TileGeneratorObject.NumberofZTiles + aNode.mapTile.gridZ;
        int adjacent = index - 1; // start with the left

        if (adjacent >= 0)
        {
            HandleNode(m_nodeMap[adjacent], aNode, aTarget);
        }

        adjacent = index - TileGeneratorObject.NumberofZTiles; // next is the up direction
        if (adjacent >= 0)
        {
            HandleNode(m_nodeMap[adjacent], aNode, aTarget);
        }

        adjacent = index + 1; // then to the right
        if (adjacent <= mapLength)
        {
            HandleNode(m_nodeMap[adjacent], aNode, aTarget);
        }

        adjacent = index + TileGeneratorObject.NumberofZTiles; // finally down
        if (adjacent <= mapLength)
        {
            HandleNode(m_nodeMap[adjacent], aNode, aTarget);
        }
    }

    int CalculateHCost(Node aOrigin, Node aTarget)
    {
        int diffX = aTarget.mapTile.gridX - aOrigin.mapTile.gridX;
        int diffZ = aTarget.mapTile.gridZ - aOrigin.mapTile.gridZ;

        diffZ = diffZ < 0 ? diffX * -1 : diffZ;
        if(diffX < 0)
        {
            diffX *= -1;
        }

        if(diffZ < 0)
        {
            diffZ *= -1;
        }

        return diffX + diffZ;
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        Debug.Log("Pathfinder.Start()");
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
