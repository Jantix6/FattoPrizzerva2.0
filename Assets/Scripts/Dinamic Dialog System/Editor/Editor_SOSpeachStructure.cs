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

            int enumLenght = Enum.GetNames(typeof(Language)).Length - 1;            // - NONE
            foreach (Language language in Enum.GetValues(typeof(Language)))
            {
                previewLanguage = language;

                if (previewLanguage != Language.NONE)
                {
                    DrawTitle(previewLanguage);
                    GUILayout.Space(5);
                    DrawBody(previewLanguage);
                    GUILayout.Space(5);
                }

                
            }

            

        }


        private void DrawBody(Language previewLanguage)
        {

            string text;
            text = structure.GetSpeachBody(previewLanguage);

            GUILayout.Label("Body: ");

            if (text != default)
            {
                text = GUILayout.TextArea(text);
                structure.SetSpeachBody(previewLanguage, text);         // it allows you to modofy the text from the editor preview
            }
                
            else
                GUILayout.Label(" NOT FOUND ");
        }

        private void DrawTitle(Language previewLanguage)
        {
            string text;
            text = structure.GetSpeachTitle(previewLanguage);

            GUILayout.Label("Title: ");

            if (text != default)
            {
                text = GUILayout.TextArea(text);
                structure.SetTitleBody(previewLanguage, text);
            }
            else
                GUILayout.Label(" NOT FOUND ");
        }
    }
}

