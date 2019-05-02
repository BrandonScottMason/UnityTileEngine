using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType
{
    OPEN,
    WALL,
    PIT,
    DOOR
}

public class Tile : MonoBehaviour
{
    public TileType TerrainType;

    public Material OpenMaterial;
    public Material DoorMaterial;
    public Material WallMaterial;
    public Material PitMaterial;

    public int gridX;
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

    private void UpdateTileType()
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
            default:
                {
                    mR.sharedMaterial = OpenMaterial;
                    break;
                }
        }
    }
}
