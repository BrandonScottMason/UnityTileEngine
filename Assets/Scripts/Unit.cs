using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Unit : Pathfinder
{
    public Tile m_targetTile;
    //public List<Node> finalPath;
    private GameObject m_currentTile;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        Debug.Log("Unit.Start()");
        if (TileGeneratorObject != null)
        {
            m_currentTile = TileGeneratorObject.GetTileMap()[0];
            Vector3 newPosistion = m_currentTile.transform.position;
            newPosistion.y += m_currentTile.GetComponent<BoxCollider>().size.y;
            this.transform.position = newPosistion;

            if(m_targetTile != null)
            {
                Node playerTile = new Node(m_currentTile.GetComponent<Tile>());
                Node path = FinalPath(playerTile, new Node(m_targetTile));
                float wireSize = path.mapTile.GetComponent<BoxCollider>().size.x;
                while (path != playerTile)
                {
                    Gizmos.DrawWireCube(path.mapTile.gameObject.transform.position, new Vector3(wireSize, 2, wireSize));
                    path = path.Parent;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnValidate()
    {
    }
}
