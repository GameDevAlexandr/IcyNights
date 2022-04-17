using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SpawnerBiom))]
public class BiomGeneratorGUI : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        SpawnerBiom sB = (SpawnerBiom)target;
        if (GUILayout.Button("Ganarate"))
        {
            sB.Generate();
        }
        if (GUILayout.Button("Clear"))
        {
            sB.ClearEnvironment();
        }
    }
}
