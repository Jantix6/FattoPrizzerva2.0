using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace Dialogues
{
    [CustomEditor(typeof(SO_Answer))]
    public class Editor_Answer : Editor
    {
        SO_Answer answer;

        SO_langaugeBasedStringContainer LBSContainer;


        GUIStyle standardStyle = new GUIStyle();
        GUIStyle errorStyle = new GUIStyle();
        GUIStyle linkedStyle = new GUIStyle();

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            answer = (SO_Answer)target;

            errorStyle.normal.textColor = Color.red;
            linkedStyle.normal.textColor = Color.blue;

            EditorGUILayout.Space();

            // Answer Bodies
            DrawAnswers();

            GUILayout.Space(15);

            // Event to perform
            DrawEvent();

            GUILayout.Space(15);

            // target structure
            DrawTargetStructure();

        }

        private void DrawTargetStructure()
        {
            EditorGUILayout.LabelField("Target Structure: ");

            string targetStructure;
            SO_DialogStructure targetDialogStructure;
            targetDialogStructure = answer.GetTargetStructure();

            if (targetDialogStructure != null)
            {
                targetStructure = targetDialogStructure.ToString();
                GUILayout.Label("\t " + targetStructure,linkedStyle);
            } else
            {
                GUILayout.Label("\t " + "NOT SET", errorStyle);
            }

        }

        private void DrawEvent()
        {
            string eventOnClickName;
            UnityAction actionToPerform = answer.GetEventActionToPerform();
            if (actionToPerform != null)
            {
                eventOnClickName = actionToPerform.Target.ToString();
                EditorGUILayout.LabelField("Event to perform: ");
                EditorGUILayout.LabelField(eventOnClickName, linkedStyle);
            }
            else
            {
                // EditorGUILayout.LabelField("\t Event not set");
            }
        }

        private void DrawAnswers()
        {
            GUILayout.Label("Answer Bodies ", standardStyle);
            LBSContainer = answer.GetLanguageBasedStringContainer();
            if (LBSContainer != null)
            {
                List<SO_LanguageBasedString> LBSStrings;
                LBSStrings = new List<SO_LanguageBasedString>();
                LBSStrings = LBSContainer.GetLanguageBasedStrings();

                if (LBSStrings.Count != 0)
                {
                    foreach (SO_LanguageBasedString lbs in LBSStrings)
                    {
                        string language;
                        string body;

                        language = lbs.language.ToString();
                        body = lbs.text;

                        GUILayout.Label("Language: ", standardStyle);
                        GUILayout.Label("\t " + language);

                        GUILayout.TextArea(body);
                    }
                }

            }
            else
            {
                EditorGUILayout.HelpBox("You need to setup a Language Based String Container in order to use this Object succesfully", MessageType.Error);
            }
        }

    }
}

