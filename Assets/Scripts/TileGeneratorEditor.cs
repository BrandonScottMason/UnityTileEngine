using System.Collections;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TileGenerator))]
public class TileGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        TileGenerator tileGen = (TileGenerator)target;
        if(GUILayout.Button("Build Map"))
        {
            if(tileGen.transform.childCount > 0)
            {
                if (!EditorUtility.DisplayDialog("Build Map", "Are you sure you want to override the exisitng tiles?", "Yes", "No"))
                {
                    return;
                }
            }

            tileGen.BuildMap();
        }

        if(GUILayout.Button("Destroy Map"))
        {
            if(EditorUtility.DisplayDialog("Destroy Map", "Are you sure you want to destroy all of the tiles?", "Yes", "No"))
            {
                tileGen.DestroyMap();
            }
        }
    }
}
