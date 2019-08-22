using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBrowser : MonoBehaviour
{
    public GameObject StatCardPrefab;
    private List<GameObject> m_factionScrollLists = new List<GameObject>();
    private GameObject m_activeViewPort;
    //private Canvas m_canvas;

    // Start is called before the first frame update
    void Start()
    {
        // Find all faction lists to store for quick reference later
        string scrollList = "ScrollList";
        object[] xmlAssets;
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform tempObj = transform.GetChild(i);
            if (tempObj.name.Contains(scrollList))
            {
                m_factionScrollLists.Add(tempObj.gameObject);
                string factionName = tempObj.name;
                factionName = factionName.Replace(scrollList, "");
                xmlAssets = Resources.LoadAll("UnitData/" + factionName, typeof(TextAsset));
                Transform factionViewport = tempObj.transform.Find(factionName + "Viewport");
                Transform factionListContent = factionViewport.transform.Find(factionName + "ListContent");
                foreach(object obj in xmlAssets)
                {
                    GameObject tempUnitCard = Instantiate(StatCardPrefab, factionListContent);
                    tempUnitCard.GetComponent<UnitCard>().SetUnitdata(obj as TextAsset);
                }
                tempObj.gameObject.SetActive(false);
            }
        }

        m_factionScrollLists[0].SetActive(true);
        m_activeViewPort = m_factionScrollLists[0];

        this.gameObject.SetActive(false);
    }

    public void BrowseUnits()
    {
    }

    public void OnBackButton()
    {
        GameObject.FindObjectOfType<MainMenu>().OnReturnToMainMenu();
        this.gameObject.SetActive(false);
    }

    public void OnFactionButtonSelected(string factionName)
    {
        if(m_activeViewPort.name.Contains(factionName))
        {
            return; // We're already viewing this faction
        }

        foreach(GameObject viewPort in m_factionScrollLists)
        {
            if(viewPort.name.Contains(factionName))
            {
                m_activeViewPort.SetActive(false);
                viewPort.SetActive(true);
                m_activeViewPort = viewPort;
                return;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
