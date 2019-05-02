using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public TileGenerator TileGeneratorObject;
    public GameObject m_targetTile;
    public List<Node> finalPath;

    private GameObject m_currentTile;
    private int m_currentTileIndex = 0;
    private GameObject[] m_map;
    private Node[] m_gridMap;

    // Start is called before the first frame update
    void Start()
    { 
        if(TileGeneratorObject != null)
        {
            m_map = TileGeneratorObject.GetTileMap();
            m_currentTile = m_map[0];
            Vector3 newPosistion = m_currentTile.transform.position;
            newPosistion.y += m_currentTile.GetComponent<BoxCollider>().size.y;
            this.transform.position = newPosistion;
            //m_closedList.Add(m_currentTile);
            //AddValidAdjacentTiles(m_currentTileIndex, ref m_openList);
            //m_gridMap = new Node[m_map.Length];
            //for(int i = 0; i < m_map.Length; i++)
            //{
            //    m_gridMap[i] = new Node(m_map[i], i % TileGeneratorObject.NumberOfXTiles, i / TileGeneratorObject.NumberofZTiles);
            //}
        }
    }

    private void CalculatePath()
    {
        foreach(Node n in m_gridMap)
        {
            
        }
    }

    //private void AddValidAdjacentTiles(int index, ref List<GameObject> list)
    //{
    //    int mapLength = m_map.Length;
    //    int adjacent = index - 1; // start with the left

    //    if(adjacent >= 0)
    //    {
    //        list.Add(m_map[adjacent]);
    //    }

    //    adjacent = index - TileGeneratorObject.NumberofZTiles; // next is the up direction
    //    if(adjacent >= 0)
    //    {
    //        list.Add(m_map[adjacent]);
    //    }

    //    adjacent = index + 1; // then to the right
    //    if (adjacent <= mapLength)
    //    {
    //        list.Add(m_map[adjacent]);
    //    }

    //    adjacent = index + TileGeneratorObject.NumberofZTiles; // finally down
    //    if(adjacent <= mapLength)
    //    {
    //        list.Add(m_map[adjacent]);
    //    }
    //}

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnValidate()
    {
    }
}
