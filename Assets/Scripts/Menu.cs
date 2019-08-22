using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject SelectionArrow;

    // Start is called before the first frame update
    protected void Start()
    {
        if(SelectionArrow == null)
        {
            Debug.Log("WARNING, NO SELECTION ARROW!");
        }
    }

    // Update is called once per frame
    protected void Update()
    {
        if (SelectionArrow.activeInHierarchy)
        {
            SelectionArrow.transform.Rotate(Vector3.up * (50.0f * Time.deltaTime));
        }
    }
}
