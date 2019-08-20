using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;

public class UnitCard : MonoBehaviour
{
    private TextAsset UnitDataTextAsset;

    private string m_name, m_alignment;
    private int m_cost, m_maxHealth, m_currentHealth, m_energy, m_defense, m_attack, m_damage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetUnitdata(TextAsset XMLUnitData)
    {
        UnitDataTextAsset = XMLUnitData;
        parseXML(XMLUnitData.text);
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

        foreach (XmlNode stat in xmlUnit[0].ChildNodes)
        {
            switch (stat.Name)
            {
                case "Name":
                    {
                        m_name = stat.InnerText;
                        this.name = m_name + "_StatCard";
                        transform.Find("Name").GetComponent<Text>().text = m_name;
                        break;
                    }
                case "Cost":
                    {
                        int.TryParse(stat.InnerText, out m_cost);
                        transform.Find("Cost").GetComponent<Text>().text = m_cost.ToString();
                        break;
                    }
                case "MaxHealth":
                    {
                        int.TryParse(stat.InnerText, out m_maxHealth);
                        m_currentHealth = m_maxHealth;
                        string health = m_currentHealth.ToString() + '/' + m_maxHealth.ToString();
                        transform.Find("Health").GetComponent<Text>().text = health;
                        break;
                    }
                case "Energy":
                    {
                        int.TryParse(stat.InnerText, out m_energy);
                        transform.Find("Energy").GetComponent<UnityEngine.UI.Text>().text = m_energy.ToString();
                        break;
                    }
                case "Defense":
                    {
                        int.TryParse(stat.InnerText, out m_defense);
                        transform.Find("Defense").GetComponent<UnityEngine.UI.Text>().text = m_defense.ToString();
                        break;
                    }
                case "Attack":
                    {
                        int.TryParse(stat.InnerText, out m_attack);
                        transform.Find("Attack").GetComponent<UnityEngine.UI.Text>().text = m_attack.ToString();
                        break;
                    }
                case "Damage":
                    {
                        int.TryParse(stat.InnerText, out m_damage);
                        transform.Find("Damage").GetComponent<UnityEngine.UI.Text>().text = m_damage.ToString();
                        break;
                    }
                default: Debug.Log("Unexpected Node: " + stat.Name); break;
            }
        }
    }
}
