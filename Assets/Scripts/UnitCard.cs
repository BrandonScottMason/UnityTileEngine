using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCard : MonoBehaviour
{
    private TextAsset UnitDataTextAsset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetUnitdata(TextAsset XMLUnitData)
    {
        UnitDataTextAsset = XMLUnitData;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
