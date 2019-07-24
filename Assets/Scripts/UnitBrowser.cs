using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class UnitBrowser : MonoBehaviour
{
    public GameObject StatCardPrefab;
    private object[] xmlAssets;
    private List<GameObject> m_unitCards = new List<GameObject>();
    private Canvas m_canvas;

    // Start is called before the first frame update
    void Start()
    {
        m_canvas = GameObject.FindObjectOfType<Canvas>();
        if (m_canvas != null)
        {
            xmlAssets = Resources.LoadAll("UnitData", typeof(TextAsset));

            foreach (object obj in xmlAssets)
            {
                GameObject tempUnit = Instantiate(StatCardPrefab, m_canvas.transform);
                tempUnit.GetComponent<UnitCard>().SetUnitdata(obj as TextAsset);
                tempUnit.SetActive(false);
                m_unitCards.Add(tempUnit);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void parseXML(string xmlData)
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(xmlData);
        XmlNodeList xmlUnit = xmlDoc.GetElementsByTagName("BaseStats");

        //foreach (XmlNode stat in xmlUnit[0].ChildNodes)
        //{
        //    switch (stat.Name)
        //    {
        //        case "Name": Name = stat.InnerText; break;
        //        case "Cost": int.TryParse(stat.InnerText, out Cost); break;
        //        case "Alignment": Alignment = stat.InnerText; break;
        //        case "MaxHealth": int.TryParse(stat.InnerText, out MaxHelath); break;
        //        case "Energy": int.TryParse(stat.InnerText, out Energy); break;
        //        case "Defense": int.TryParse(stat.InnerText, out Defense); break;
        //        case "Attack": int.TryParse(stat.InnerText, out Attack); break;
        //        case "Damage": int.TryParse(stat.InnerText, out Damage); break;
        //        default: Debug.Log("Unexpected Node: " + stat.Name); break;
        //    }

        //}
    }
}
