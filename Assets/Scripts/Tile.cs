using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType
{
    OPEN,
    WALL,
    PIT,
    DOOR,
    PATH
}

[System.Serializable]
[DisallowMultipleComponent]
public class Tile : MonoBehaviour
{
    public TileType TerrainType;

    public Material OpenMaterial;
    public Material DoorMaterial;
    public Material WallMaterial;
    public Material PitMaterial;
    public Material PathMaterial;

    [SerializeField]
    public int gridX;
    [SerializeField]
    public int gridZ;

    // Start is called before the first frame update
    void Start()
    {
        //UpdateTileType();
    }

    private void OnValidate()
    {
        UpdateTileType();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateTileType()
    {
        MeshRenderer mR = this.GetComponent<MeshRenderer>();

        switch (TerrainType)
        {
            case TileType.OPEN:
                {
                    mR.sharedMaterial = OpenMaterial;
                    break;
                }
            case TileType.WALL:
                {
                    mR.sharedMaterial = WallMaterial;
                    break;
                }
            case TileType.PIT:
                {
                    mR.sharedMaterial = PitMaterial;
                    break;
                }
            case TileType.DOOR:
                {
                    mR.sharedMaterial = DoorMaterial;
                    break;
                }
            case TileType.PATH:
                {
                    mR.sharedMaterial = PathMaterial;
                    break;
                }
            default:
                {
                    mR.sharedMaterial = OpenMaterial;
                    break;
                }
        }
    }
}
