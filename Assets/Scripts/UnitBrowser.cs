using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBrowser : MonoBehaviour
{
    public GameObject StatCardPrefab;
    private object[] xmlAssets;
    private List<GameObject> m_unitCards = new List<GameObject>();
    private GameObject m_contentObj;
    //private Canvas m_canvas;

    // Start is called before the first frame update
    void Start()
    {
        m_contentObj = transform.Find("Content").gameObject;
        if (m_contentObj != null)
        {
            xmlAssets = Resources.LoadAll("UnitData", typeof(TextAsset));

            foreach (object obj in xmlAssets)
            {
                GameObject tempUnit = Instantiate(StatCardPrefab, m_contentObj.transform);
                tempUnit.GetComponent<UnitCard>().SetUnitdata(obj as TextAsset);
                tempUnit.SetActive(false);
                m_unitCards.Add(tempUnit);
                //this.GetComponent<UnityEngine.UI.ScrollRect>().content.
            }
        }
    }

    public void BrowseUnits()
    {
        foreach(GameObject obj in m_unitCards)
        {
            obj.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
