using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Dialogues
{
    [CustomEditor(typeof(SO_langaugeBasedStringContainer))]
    public class Editor_LanguageBasedStringContainer : Editor
    {
        SO_langaugeBasedStringContainer LBSContainer;
        List<SO_LanguageBasedString> LBSObjects;
        GUIStyle style = new GUIStyle();


        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUILayout.Space(15);

            if (LBSObjects == null)
                LBSObjects = new List<SO_LanguageBasedString>();

            LBSContainer = (SO_langaugeBasedStringContainer)target;

            LBSObjects = LBSContainer.GetLanguageBasedStrings();
            if (LBSObjects != null && LBSObjects.Count != 0 )
            {
                foreach (SO_LanguageBasedString lbs in LBSObjects)
                {
                    GUILayout.BeginVertical();

                    string language;
                    string body;

                    language = lbs.language.ToString();
                    body = lbs.text;

                    if (language == "")
                    {
                        language = "NOT DEFINED";
                        style.normal.textColor = Color.red;
                    }                       
                    GUILayout.Label("LANGUAGE = " + language, style);
                    style.normal.textColor = Color.black;

                    if (body == "")
                    {
                        body = "NOT DEFINED";
                        style.normal.textColor = Color.red;
                    }
                    GUILayout.TextArea("\t String = " + body,style);
                    style.normal.textColor = Color.black;

                    GUILayout.EndVertical();

                    GUILayout.Space(20);

                }

            }  else
            {
                EditorGUILayout.HelpBox("You need to assigne the LBSs in order to this object to work as it is intended", MessageType.Warning);
            }

            if (GUILayout.Button("Create LBS"))
            {
                LBSContainer.CreateLanguageBasedStrings();
            }
            
        }
    }
}

