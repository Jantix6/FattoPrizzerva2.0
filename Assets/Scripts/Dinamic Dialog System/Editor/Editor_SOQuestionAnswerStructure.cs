using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;


// I know it is ugly and not very "performance friendly" but it will do the trick (at least for now)

namespace Dialogues
{
    [CustomEditor(typeof(SO_QuestionAnswerStructure))]
    public class Editor_SOQuestionAnswerStructure : Editor
    {
        SO_QuestionAnswerStructure structure;
        public Language previewLanguage = Language.ENGLISH;
        private Language previousLanguage;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            structure = (SO_QuestionAnswerStructure)target;

            // draw separation line from rest of editor
            GUILayout.Space(40);
            GUILayout.Label("Debug view");

            // Preview language selection
            previewLanguage = (Language)EditorGUILayout.EnumPopup("Language to preview: ", previewLanguage);
            if (previewLanguage != previousLanguage)
                SaveProject();
            previousLanguage = previewLanguage;

            if (previewLanguage != Language.NONE)
            {
                // Draw question
                GUILayout.Space(5);
                DrawQuestion(previewLanguage);
                // Draw Answers
                DrawAnswers(previewLanguage);
                GUILayout.Space(5);
            }

            // save changes button
            if (GUILayout.Button("Save changes"))
            {
                SaveProject();
            }
            
            /*
            int enumLenght = Enum.GetNames(typeof(Language)).Length - 1;            // - NONE
            foreach (Language language in Enum.GetValues(typeof(Language)))
            {
                previewLanguage = language;

                if (previewLanguage != Language.NONE)
                {
                    // Draw question
                    GUILayout.Space(5);
                    DrawQuestion(previewLanguage);
                    // Draw Answers
                    DrawAnswers(previewLanguage);
                    GUILayout.Space(5);
                }
            }
            */
 


        }

        private void SaveProject()
        {
            AssetDatabase.SaveAssets();
            Debug.LogWarning("Project saved");
        }

        private void DrawQuestion(Language _desiredLanguage)
        {
            string question;
            question = structure.GetQuestion(_desiredLanguage);

            GUILayout.Label("QUESTION: ");

            if (question != default)
            {
                question = GUILayout.TextArea(question);
                structure.SetQuestion(_desiredLanguage, question);
            }
            else
                GUILayout.Label(" NOT FOUND ");
        }

        private void DrawAnswers(Language _desiredLanguage)
        {
            List<SO_Answer> answers = null;
            SO_Answer answer = null;
            string answerBody;
            GUIStyle style = new GUIStyle();

            answers = structure.GetAnswers();
            if (answers != null)
            {
                
                for (int index = 0; index < answers.Count; index++)
                {
                    GUILayout.BeginVertical();
                    answer = answers[index];

                    // BODY
                    answerBody = answer.GetAnswerBody(_desiredLanguage);

                    GUILayout.Label("A " + index + ": ");
                    answerBody = GUILayout.TextArea( answerBody);

                    answer.SetAnswerBody(_desiredLanguage, answerBody);     // edit the answer body via editor


                    // NEXT STRUCTURE
                    SO_DialogStructure nextStructure;
                    nextStructure = answers[index].GetTargetStructure();

                    string labelText = "";
                    labelText += "TO --> ";

                    if (nextStructure != null)
                    {
                        labelText += "\t " + nextStructure.name;
                        style.normal.textColor = Color.blue;
                    }
                    else
                    {
                        labelText += "\t NOWHERE";
                        style.normal.textColor = Color.red;
                    }
                    GUILayout.Label(labelText, style);
                    style.normal.textColor = Color.black;

                    // events to invoke
                    string eventText;

                    List<SO_DialogEvent> dialogEvents = new List<SO_DialogEvent>();

                    List<SO_DialogEvent> answerEvents = answers[index].GetListOfEvents();
                    if (answerEvents != null)
                        dialogEvents.AddRange(answerEvents);

                    if (dialogEvents.Count != 0 && dialogEvents != null)
                    {
                        foreach (SO_DialogEvent dialogEvent in dialogEvents)
                        {
                            eventText = "CALLING EVENT -->";
                            eventText += dialogEvent.name;
                            style.normal.textColor = Color.blue;
                            GUILayout.Label(eventText, style);
                            eventText = "";
                        }
                    }

                    

                    GUILayout.EndVertical();

                    GUILayout.Space(10);


                }

            }
            else
            {
                style.normal.textColor = Color.red;
                GUILayout.Label("No answers found");
                style.normal.textColor = Color.grey;

            }
        }


    }
}

