using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

[DisallowMultipleComponent]
[XmlRoot("TestUnit")]
public class Unit : Pathfinder
{
    public string Name;
    public int Cost;
    public string Alignment;
    public int MaxHelath;
    public int CurrentHealth;
    public int Energy;
    public int Defense;
    public int Attack;
    public int Damage;

    public TextAsset XMLUnitData;
    public Tile m_targetTile;
    public Texture StatCardBackground;
    public Material SelectedMaterial;

    // Prefabs
    public GameObject MoveButtonPrefab;
    public GameObject StatCardPrefab;

    private GameObject UIMoveButton;
    private GameObject UIStatCard;
    private Material m_defaultMaterial;
    private GameObject m_currentTile;
    private Node m_path;
    private bool m_bSettingPath = false;
    private Canvas m_canvas;

    // Start is called before the first frame update
    protected override void Start()
    {
        // Load XML data
        parseXML(XMLUnitData.text);

        m_defaultMaterial = GetComponent<MeshRenderer>().sharedMaterial;

        // Generate UI elements under the canvas
        m_canvas = FindObjectOfType<Canvas>();
        if (m_canvas != null)
        {
            if (StatCardPrefab != null)
            {
                UIStatCard = Instantiate(StatCardPrefab, m_canvas.transform, false);
                UIStatCard.name = Name + "StatCard";
                UIStatCard.SetActive(false);

                UIStatCard.transform.Find("Name").GetComponent<UnityEngine.UI.Text>().text = Name;
                UIStatCard.transform.Find("Alignment").GetComponent<UnityEngine.UI.Text>().text = Alignment.ToString();
                UIStatCard.transform.Find("MaxHealth").GetComponent<UnityEngine.UI.Text>().text = MaxHelath.ToString();
                UIStatCard.transform.Find("CurrentHealth").GetComponent<UnityEngine.UI.Text>().text = MaxHelath.ToString();
                UIStatCard.transform.Find("Energy").GetComponent<UnityEngine.UI.Text>().text = Energy.ToString();
                UIStatCard.transform.Find("Defense").GetComponent<UnityEngine.UI.Text>().text = Defense.ToString();
                UIStatCard.transform.Find("Attack").GetComponent<UnityEngine.UI.Text>().text = Attack.ToString();
                UIStatCard.transform.Find("Damage").GetComponent<UnityEngine.UI.Text>().text = Damage.ToString();
            }

            if (MoveButtonPrefab != null)
            {
                UIMoveButton = Instantiate(MoveButtonPrefab, m_canvas.transform, false);
                UIMoveButton.name = Name + "MoveButton";
                UIMoveButton.SetActive(false);
            }
        }

        base.Start();
        if (TileGeneratorObject != null)
        {
            m_currentTile = TileGeneratorObject.GetTileMap()[0];
            Vector3 newPosistion = m_currentTile.transform.position;
            newPosistion.y += m_currentTile.GetComponent<BoxCollider>().size.y;
            this.transform.position = newPosistion;
        }
    }

    private void parseXML(string xmlData)
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(xmlData);
        XmlNodeList xmlUnit = xmlDoc.GetElementsByTagName("BaseStats");

        foreach (XmlNode stat in xmlUnit[0].ChildNodes)
        {
            switch (stat.Name)
            {
                case "Name": Name = stat.InnerText; break;
                case "Cost": int.TryParse(stat.InnerText, out Cost); break;
                case "Alignment": Alignment = stat.InnerText; break;
                case "MaxHealth": int.TryParse(stat.InnerText, out MaxHelath); break;
                case "Energy": int.TryParse(stat.InnerText, out Energy); break;
                case "Defense": int.TryParse(stat.InnerText, out Defense); break;
                case "Attack": int.TryParse(stat.InnerText, out Attack); break;
                case "Damage": int.TryParse(stat.InnerText, out Damage); break;
                default: Debug.Log("Unexpected Node: " + stat.Name); break;
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
        UIStatCard.SetActive(false);
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
        if(UIMoveButton != null)
        {
            UIMoveButton.SetActive(true);
        }

        if(UIStatCard != null)
        {
            UIStatCard.SetActive(true);
        }
    }

    private void OnDeselect()
    {
        this.GetComponent<MeshRenderer>().sharedMaterial = m_defaultMaterial;
        if (UIMoveButton != null)
        {
            UIMoveButton.SetActive(false);
        }

        if (UIStatCard != null)
        {
            UIStatCard.SetActive(false);
        }
    }

    private void OnValidate()
    {
    }
}
