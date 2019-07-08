using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    public GameObject Tile;
    public int NumberOfXTiles;
    public int NumberOfZTiles;

    public void BuildMap()
    {
        float tileSize = Tile.GetComponent<BoxCollider>().size.x; // *Requirement* These tile sizes need to have the same X and Z sizes
        for (int z = 0; z < NumberOfZTiles; z++)
        {
            for (int x = 0; x < NumberOfXTiles; x++)
            {
                float newXpos = this.transform.position.x + (tileSize * x) + (tileSize / 2);
                float newZpos = this.transform.position.z + (tileSize * z) + (tileSize / 2);
                GameObject inst = Instantiate(Tile, new Vector3(newXpos, this.transform.position.y, newZpos), Quaternion.identity, this.transform);
                inst.GetComponent<Tile>().gridX = x;
                inst.GetComponent<Tile>().gridZ = z;
            }
        }
    }

    public void DestroyMap()
    {
        var children = new GameObject[this.transform.childCount];
        for(int i = 0; i < children.Length; i++)
        {
            children[i] = this.transform.GetChild(i).gameObject;
        }

        foreach (GameObject child in children)
        {
            DestroyImmediate(child);
        }
    }

    public GameObject[] GetTileMap()
    {
        var children = new GameObject[this.transform.childCount];
        if (this.transform.childCount > 0)
        {
            for (int i = 0; i < children.Length; i++)
            {
                children[i] = this.transform.GetChild(i).gameObject;
            }
        }
        return children;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
