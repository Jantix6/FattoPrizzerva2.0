using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


// I know it is ugly and not very "performance friendly" but it will do the trick (at least for now)

namespace Dialogues
{
    [CustomEditor(typeof(SO_QuestionAnswerStructure))]
    public class Editor_SOQuestionAnswerStructure : Editor
    {
        SO_QuestionAnswerStructure structure;
        Language previewLanguage = Language.ENGLISH;


        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            structure = (SO_QuestionAnswerStructure)target;

            // draw separation line from rest of editor
            GUILayout.Space(40);
            GUILayout.Label("Debug view");

            // Draw question
            DrawQuestion(previewLanguage);

            GUILayout.Space(5);

            // Draw Answers
            DrawAnswers(previewLanguage);

        }

        private void DrawQuestion(Language _desiredLanguage)
        {
            string question;
            question = structure.GetQuestion(_desiredLanguage);

            GUILayout.Label("QUESTION: ");

            if (question != default)
                GUILayout.TextArea(question);
            else
                GUILayout.Label(" NOT FOUND ");
        }

        private void DrawAnswers(Language _desiredLanguage)
        {
            List<SO_Answer> answers = null;
            string answer;
            GUIStyle style = new GUIStyle();



            answers = structure.GetAnswers();
            if (answers != null)
            {
                
                for (int index = 0; index < answers.Count; index++)
                {
                    GUILayout.BeginVertical();

                    // BODY
                    answer = answers[index].GetAnswerBody(_desiredLanguage);

                    GUILayout.Label("A " + index + ": ");
                    GUILayout.TextArea( answer);

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
                    style.normal.textColor = Color.grey;

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

