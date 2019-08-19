using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Dialogues
{
    [CustomEditor(typeof(DialogObjectCreator))]
    public class Editor_DialogObjectCreator : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            DialogObjectCreator dialogObjectCreator = (DialogObjectCreator)target;

            if (GUILayout.Button("Generate objects"))
            {
                dialogObjectCreator.StartObjectGeneration();
            }

        }
    }

}

