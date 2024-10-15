using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


// This script adds buttons in the LevelBuilder script inspector to trigger the read json method and the build level method during Editor mode
[CustomEditor(typeof(LevelBuilder))]
public class LevelBuilderEditorScript : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        LevelBuilder levelBuilder = (LevelBuilder)target;
        if (GUILayout.Button("Read Json")) // Creates Read Json button
        {
            levelBuilder.ReadJson();
        }

        if (GUILayout.Button("Build Level")) // Places enemies based on the informations read in the json
        {
            levelBuilder.BuildLevel();
        }
    }
}
