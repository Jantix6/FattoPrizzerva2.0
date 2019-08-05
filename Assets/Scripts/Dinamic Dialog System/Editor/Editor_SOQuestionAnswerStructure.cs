using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEditor;
using UnityEngine;


namespace Dialogues
{
    [CustomEditor(typeof(SO_QuestionAnswerStructure))]
    public class Editor_SOQuestionAnswerStructure : Editor
    {
        SO_QuestionAnswerStructure structure;
        Language previewLanguage = Language.ENGLISH;

        public override bool RequiresConstantRepaint()
        {
            return true;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            structure = (SO_QuestionAnswerStructure)target;

            // draw separation line from rest of editor
            GUILayout.Space(40);
            GUILayout.Label("Debug view");

            // Draw question
            DrawQuestion(previewLanguage);

            // Draw Answers




        }

        private void DrawQuestion(Language _desiredLanguage)
        {
            string question;
            question = structure.GetQuestion(_desiredLanguage);

            if (question != default)
                GUILayout.Label("QUESTION: " + question);
            else
                GUILayout.Label("QUESTION: " + " NOT FOUND ");
        }

        private void DrawAnswers(Language _desiredLanguage)
        {
            List<SO_Answer> answers = null;

            answers = structure.GetAnswers();

            if (answers != null)
            {

            }
        }


    }
}

