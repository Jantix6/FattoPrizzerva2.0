using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Dialogues 
{
    [CustomEditor(typeof(CharacterData))]
public class CharacterDataEditor : Editor
{

    CharacterData characterData;

    public override void OnInspectorGUI() 
    {
        characterData = target as CharacterData;

        // base.OnInspectorGUI();
        DrawDefaultInspector();   

        characterData.GetSprite();
    }
}
}


