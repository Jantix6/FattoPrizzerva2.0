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

        // Structure genrator variables
        [SerializeField] private int numberOfAnswers = 0;
        [SerializeField] private int startingAnswerNumber;
        private string questionFolderName = "1_Question";
        private string answersFolderName = "2_Answers";
        private string answerPrefix = "A";


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

            } else
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
            StructureCreation();
        }

        private void StructureCreation()
        {
            EditorGUILayout.HelpBox("Create all the strucutres of this object down bellow", MessageType.Info);
            numberOfAnswers = EditorGUILayout.IntField("Number of answers", numberOfAnswers);
            startingAnswerNumber = EditorGUILayout.IntField("Starting answer number" ,startingAnswerNumber);

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
            }
            else
                GUILayout.Label("Question not found");
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

                    string labelText;
                    labelText = "TO --> ";

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

        // Structure generator //
        // ---------------------------------------------------------------------------------- //

        private void CreateQuestionAnswerFullStructure()
        {
            string mainFolderPath;
            int lastIndexOfSeparator;

            // get the folder path in witch to create all the folders and then assets
            mainFolderPath = AssetDatabase.GetAssetPath(structure);
            lastIndexOfSeparator = mainFolderPath.LastIndexOf('/');
            mainFolderPath = mainFolderPath.Remove(lastIndexOfSeparator);

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
                string answerFolderName = answerPrefix + "_" +  (startingAnswerNumber + i).ToString();                     // i must be the id of the answer defined on the .doc

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

                // add the answer to this object
                structure.AddAnswer(answerObject);
            }


        }

    }
}

