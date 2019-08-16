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

            LBSContainer = (SO_langaugeBasedStringContainer)target;

            GUILayout.Space(15);

            if (LBSObjects == null)
                LBSObjects = new List<SO_LanguageBasedString>();

            LBSObjects = LBSContainer.GetLanguageBasedStrings();
            if (LBSObjects != null && LBSObjects.Count != 0 )
            {
                foreach (SO_LanguageBasedString lbs in LBSObjects)
                {
                    GUILayout.BeginVertical();

                    string language = lbs.language.ToString();      
                    string body = lbs.text;

                    if (string.IsNullOrEmpty(language))
                    {
                        if (language == null)
                            language = "Language is null";
                        if (language == "")
                            language = "NOT DEFINED (language == nothing)";
                        style.normal.textColor = Color.red;
                    }
                    GUILayout.Label("LANGUAGE = " + language, style);
                    style.normal.textColor = Color.black;

                    if (string.IsNullOrEmpty(body))
                    {
                        if (body == null)
                            body = "body is null";
                        if (body == "")
                            body = "NOT DEFINED (body == nothing)";
                        style.normal.textColor = Color.red;
                    }
                    GUILayout.TextArea("\t String = " + body, style);
                    style.normal.textColor = Color.black;

                    GUILayout.EndVertical();
                    GUILayout.Space(20);

                }

            }  else
            {
                // Debug.LogError("You need to assigne the LBSs in order for this object to work as it is intended");
                EditorGUILayout.HelpBox("You need to assigne the LBSs in order for this object to work as it is intended", MessageType.Warning);
            }

            if (GUILayout.Button("Create LBS"))
            {
                LBSContainer.CreateLanguageBasedStrings();

                //debug
                foreach (SO_LanguageBasedString item in LBSContainer.GetLanguageBasedStrings())
                {
                    string len = item.language.ToString();
                    string text = item.text;

                    Debug.LogError("language : " + len + " text : " + text);
                }
            }
            
        }

    }
}

