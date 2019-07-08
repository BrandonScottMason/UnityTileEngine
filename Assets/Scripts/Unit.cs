using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Unit : Pathfinder
{
    public Tile m_targetTile;
    public GameObject UIButton;
    public Material SelectedMaterial;

    private Material m_defaultMaterial;
    private GameObject m_currentTile;
    private Node m_path;
    private bool m_bSettingPath = false;

    // Start is called before the first frame update
    protected override void Start()
    {
        m_defaultMaterial = GetComponent<MeshRenderer>().sharedMaterial;
        if(UIButton != null)
        {
            UIButton.SetActive(false);
        }

        base.Start();
        if (TileGeneratorObject != null)
        {
            m_currentTile = TileGeneratorObject.GetTileMap()[0];
            Vector3 newPosistion = m_currentTile.transform.position;
            newPosistion.y += m_currentTile.GetComponent<BoxCollider>().size.y;
            this.transform.position = newPosistion;

            if(m_targetTile != null)
            {
                
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_bSettingPath && Input.GetMouseButtonDown(0))
        {
            Ray toMouse = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rhInfo;
            bool didItHit = Physics.Raycast(toMouse, out rhInfo, 500.0f);
            if(didItHit && rhInfo.transform == this.transform)
            {
                OnSelect();
            }
            else
            {
                //OnDeselect();
            }
            
        }

        if(m_bSettingPath)
        {
            Ray toMouse = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rhInfo;
            bool didItHit = Physics.Raycast(toMouse, out rhInfo, 500.0f);
            if (didItHit)
            {
                foreach (GameObject tile in TileGeneratorObject.GetTileMap())
                {
                    if (rhInfo.transform == tile.transform)
                    {
                        m_targetTile = tile.GetComponent<Tile>();
                        CalculatePath();
                        return;
                    }
                }
            }
        }
    }

    public void OnSetPath()
    {
        m_bSettingPath = true;
    }

    private void CalculatePath()
    {
        if (m_targetTile != null)
        {
            Node playerTile = new Node(m_currentTile.GetComponent<Tile>());
            m_path = FinalPath(playerTile, new Node(m_targetTile));
            while (m_path != playerTile)
            {
                m_path.mapTile.gameObject.GetComponent<Tile>().TerrainType = TileType.PATH;
                m_path.mapTile.gameObject.GetComponent<Tile>().UpdateTileType();
                m_path = m_path.Parent;
            }
        }
    }

    private void OnSelect()
    {
        this.GetComponent<MeshRenderer>().sharedMaterial = SelectedMaterial;
        if(UIButton != null)
        {
            UIButton.SetActive(true);
        }
    }

    private void OnDeselect()
    {
        this.GetComponent<MeshRenderer>().sharedMaterial = m_defaultMaterial;
        if (UIButton != null)
        {
            UIButton.SetActive(false);
        }
    }

    private void OnValidate()
    {
    }
}
