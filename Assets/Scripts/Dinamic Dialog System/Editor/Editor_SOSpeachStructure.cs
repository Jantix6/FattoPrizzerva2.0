using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace Dialogues
{
    [CustomEditor(typeof(SO_SpeachStructure))]
    public class Editor_SOSpeachStructure : Editor
    {
        SO_SpeachStructure structure ;

        Language previewLanguage = Language.ENGLISH;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            structure = (SO_SpeachStructure)target;

            // draw separation line from rest of editor
            GUILayout.Space(40);
            GUILayout.Label("Debug view");

            DrawTitle(previewLanguage);
            GUILayout.Space(5);
            DrawBody(previewLanguage);
            GUILayout.Space(5);

        }


        private void DrawBody(Language previewLanguage)
        {

            string text;
            text = structure.GetSpeachBody(previewLanguage);

            GUILayout.Label("Body: ");

            if (text != default)
                GUILayout.TextArea(text);
            else
                GUILayout.Label(" NOT FOUND ");
        }

        private void DrawTitle(Language previewLanguage)
        {
            string text;
            text = structure.GetSpeachTitle(previewLanguage);

            GUILayout.Label("Title: ");

            if (text != default)
                GUILayout.TextArea(text);
            else
                GUILayout.Label(" NOT FOUND ");
        }
    }
}

