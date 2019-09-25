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
        public Language previewLanguage = Language.CATALAN;
        private Language previousLanguage;

        // Structure genrator variables
        [SerializeField] private int numberOfAnswers = 0;
        [SerializeField] private int startingAnswerNumber;
        private string questionFolderName = "1_Question";
        private string answersFolderName = "2_Answers";
        private string answerPrefix = "A";

        List<bool> fold_Lst = new List<bool>();

        bool foldCreateStructures;

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
                EditorGUILayout.Space();
                // Draw Answers
                DrawAnswers(previewLanguage);
                GUILayout.Space(5);

            }
            else
            {
                EditorGUILayout.HelpBox("Select a language from the drop down in order to modify the dialog data of the specified language", MessageType.Info);
            }

            // save changes button
            if (GUILayout.Button("Save changes"))
            {
                SaveProject();
            }

            // Structure creation //
            // ------------------------------------------------------------------------ //
            EditorGUILayout.Space();
            StructureCreation();

        }

        private void StructureCreation()
        {
            EditorGUILayout.HelpBox("Create all the strucutres of this object down bellow", MessageType.Info);
            numberOfAnswers = EditorGUILayout.IntField("Number of answers", numberOfAnswers);
            startingAnswerNumber = EditorGUILayout.IntField("Starting answer number", startingAnswerNumber);

            if (GUILayout.Button("Create all my structures"))
            {
                CreateQuestionAnswerFullStructure();
            }
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

            if (question != null)
            {
                question = GUILayout.TextArea(question);
                structure.SetQuestion(_desiredLanguage, question);

                // mark the question SO_LBS modified by the text change and set it up as dirty to save it's new value
                EditorUtility.SetDirty(structure.GetQuestionContainer().GetLanguageBasedString(_desiredLanguage));
            }
            else
                GUILayout.Label("Question not found");
        }


        private void DrawAnswers(Language _desiredLanguage)
        {
            List<SO_Answer> answers = null;
            SO_Answer answer = null;
            GUIStyle style = new GUIStyle();

            answers = structure.GetAnswers();

            for (int i = 0; i < answers.Count; i++)
            {
                fold_Lst.Add(false);
            }

            if (answers != null && answers.Count != 0)
            {
                for (int index = 0; index < answers.Count; index++)
                {
                    GUILayout.BeginVertical();
                    answer = answers[index];

                    fold_Lst[index] = EditorGUILayout.InspectorTitlebar(fold_Lst[index], answer);
                    if (fold_Lst[index])
                    {
                        // BODY
                        DrawAnswerBody(answer, index, _desiredLanguage);

                        EditorGUILayout.Space();

                        // NEXT STRUCTURE
                        DrawNextStructure(answer, style);

                        EditorGUILayout.Space();

                        // events to invoke
                        DrawEventsToInvoke(answer, style);
                    }


                    GUILayout.EndVertical();
                    GUILayout.Space(5);
                }
            }
            else
            {
                style.normal.textColor = Color.red;
                GUILayout.Label("No answers found");
                style.normal.textColor = Color.grey;
            }
        }

        private void DrawAnswerBody(SO_Answer _answer, int _answerIndex, Language _desiredLanguage)
        {
            string answerBody;
            answerBody = _answer.GetAnswerBody(_desiredLanguage);

            GUILayout.Label("A " + _answerIndex + ": ");
            answerBody = GUILayout.TextArea(answerBody);

            _answer.SetAnswerBody(_desiredLanguage, answerBody);     // edit the answer body via editor
            EditorUtility.SetDirty(_answer);
            EditorUtility.SetDirty(_answer.GetAnswerLBSContainer().GetLanguageBasedString(_desiredLanguage));
        }

        private void DrawNextStructure(SO_Answer _answer, GUIStyle _style)
        {
            SO_DialogStructure nextStructure;
            nextStructure = _answer.GetTargetStructure();

            nextStructure = (SO_DialogStructure)EditorGUILayout.ObjectField("Next structure : ", nextStructure, typeof(SO_DialogStructure), true);
            if (nextStructure != null)
            {
                // check for circular reference
                if (nextStructure == target)
                {
                    EditorGUILayout.HelpBox("Circular reference detected: Reference a object with its not you or the dialog will not advance", MessageType.Error);
                }

                _answer.SetTargetStructure(nextStructure);
                EditorUtility.SetDirty(_answer);
            }



        }
        private void DrawEventsToInvoke(SO_Answer _answer, GUIStyle _style)
        {
            SO_DialogEventsContainer dialogEventsContainer = _answer.GetDialogEventsContainer();

            dialogEventsContainer = (SO_DialogEventsContainer)EditorGUILayout.ObjectField("Answer events Container: ", dialogEventsContainer, typeof(SO_DialogEventsContainer), true);

            _answer.SetDialogEventsContainer(dialogEventsContainer);
            EditorUtility.SetDirty(_answer);

            if (dialogEventsContainer != null)
            {
                // draw a list of events
                List<SO_DialogEvent> dialogEvents = dialogEventsContainer.GetEventsList();

                if (dialogEvents != null && dialogEvents.Count != 0)
                {


                    for (int i = 0; i < dialogEvents.Count; i++)
                    {
                        dialogEvents[i] = (SO_DialogEvent)EditorGUILayout.ObjectField("\t\t Dialog Event: ", dialogEvents[i], typeof(SO_DialogEvent), true);

                        if (dialogEvents[i] != null)
                        {
                            EditorUtility.SetDirty(dialogEventsContainer);
                        }
                    }

                }
                else
                {
                    EditorGUILayout.HelpBox("You might wan to add some events to the event container if you are willing to use it, otherwise remove it from the object", MessageType.Warning);

                }

                EditorGUILayout.BeginHorizontal();

                // remove last event
                if (dialogEvents != null && dialogEvents.Count != 0)
                {
                    if (GUILayout.Button("Remove Last Event"))
                    {
                        dialogEventsContainer.RemoveLastDialogEvent();
                    }
                }

                // add event button
                if (GUILayout.Button("Add Event"))
                {
                    dialogEventsContainer.AddDialogEvent(null);
                }

                EditorGUILayout.EndHorizontal();


            }
            else
            {
                EditorGUILayout.HelpBox("You might want to set the events container located on the folder of this answer ", MessageType.Info);

            }

        }

        // Structure generator //
        // ----------------------------------------------------------------------------------------------- //

        private void CreateQuestionAnswerFullStructure()
        {
            string mainFolderPath;
            int lastIndexOfSeparator;

            // get the folder path in witch to create all the folders and then assets
            mainFolderPath = AssetDatabase.GetAssetPath(structure);
            lastIndexOfSeparator = mainFolderPath.LastIndexOf('/');
            mainFolderPath = mainFolderPath.Remove(lastIndexOfSeparator);

            // clean up the object before 
            structure.ResetAnswersList();

            // create folders
            CreateQuestionFolder(mainFolderPath);
            CreateAnswersFolder(mainFolderPath);
        }
        // Create question folder and all its contents
        private void CreateQuestionFolder(string mainFolderPath)
        {
            string questionNameOnStructure = "Question";
            string extension = ".asset";

            AssetDatabase.CreateFolder(mainFolderPath, questionFolderName);
            string questionFolderPath = mainFolderPath + "/" + questionFolderName;

            // create the LBS Container
            SO_langaugeBasedStringContainer LBSContainer = CreateInstance<SO_langaugeBasedStringContainer>();
            AssetDatabase.CreateAsset(LBSContainer, questionFolderPath + "/" + LBSContainer.GetFilename() + "_" + questionNameOnStructure + extension);

            // set it on the question variable of this structure
            LBSContainer.CreateLanguageBasedStrings();                  // it does create it's own strings
            structure.SetQuestion(LBSContainer);                        // add it to my question variable


        }

        // Create the main answers folder and all the necesary folders and answers (requested by the user)
        private void CreateAnswersFolder(string mainFolderPath)
        {
            string extension = ".asset";

            AssetDatabase.CreateFolder(mainFolderPath, answersFolderName);              // answers
            string answersFolderPath = mainFolderPath + "/" + answersFolderName;
            string[] folderPaths = new string[numberOfAnswers];

            // create the folders
            for (int i = 0; i < numberOfAnswers; i++)
            {
                string answerFolderName = answerPrefix + "_" + (startingAnswerNumber + i).ToString();                     // i must be the id of the answer defined on the .doc

                // create answer folder
                AssetDatabase.CreateFolder(answersFolderPath, answerFolderName);
                folderPaths[i] = answersFolderPath + "/" + answerFolderName;

                // Create answer folder content -------------------------------- //
                Debug.Log(folderPaths[i]);
                // create answer object
                SO_Answer answerObject = CreateInstance<SO_Answer>();
                AssetDatabase.CreateAsset(answerObject, folderPaths[i] + "/" + answerFolderName + extension);

                // create LBS for the answer
                SO_langaugeBasedStringContainer lbsContainer = CreateInstance<SO_langaugeBasedStringContainer>();
                AssetDatabase.CreateAsset(lbsContainer, folderPaths[i] + "/" + lbsContainer.GetFilename() + "_" + answerFolderName + extension);
                lbsContainer.CreateLanguageBasedStrings();

                // set the lbs of the answer
                answerObject.SetAnswerBody(lbsContainer);
                EditorUtility.SetDirty(answerObject);  // mark in order to make unity aware of the new status of the object (is modified) so it's saved

                // add the answer to this object
                structure.AddAnswer(answerObject);
                EditorUtility.SetDirty(structure);  // mark in order to make unity aware of the new status of the object (is modified) so it's saved

                // Create events on click container
                SO_DialogEventsContainer dialogEventsConteiner = CreateInstance<SO_DialogEventsContainer>();
                AssetDatabase.CreateAsset(dialogEventsConteiner, folderPaths[i] + "/" + dialogEventsConteiner.GetFilename() + "_" + answerFolderName + extension);
                answerObject.SetEventsOnClickContainer(dialogEventsConteiner);
                EditorUtility.SetDirty(answerObject);  // mark in order to make unity aware of the new status of the object (is modified) so it's saved


            }


        }

    }
}

