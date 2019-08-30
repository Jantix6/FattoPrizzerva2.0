using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;
using UnityEditor;

namespace Dialogues
{

    public class DialogObjectCreator : MonoBehaviour
    {

        [SerializeField] private TextAsset speachData_CSV;
        [SerializeField] private TextAsset qaData_CSV;

        [Header("Config data")]
        [SerializeField] private SO_DialogObjectCreatorConfig configScriptableObject;


        // called by a button
        public void StartObjectGeneration()
        {
            GenerateCSVObjects();
        }

        private void GenerateCSVObjects()
        {
            GenerateQAObjects();
            // GenerateSpeachObjects(); // bug
        }

        public void GenerateSpeachObjects()
        {
            SO_SpeachStructureCSVImportSettings settings;
            string _dataBasePath;

            _dataBasePath = GetBasePath(speachData_CSV);
            settings = configScriptableObject.GetSPeachCSVSettings();

            // parse the data and separate it on words
            List<string> separatedWords = new List<string>();
            // get the data from the csv
            List<string> dataLoaded = new List<string>();
            dataLoaded.AddRange(CSVReader.ReadData(_dataBasePath));

            if (dataLoaded.Count == 0)
            {
                Debug.LogError("No data found on the selected CSV archive");
                return;
            }

            // get starting index
            int startingIndex;
            startingIndex = settings.DataStartLine;

            for (int index = startingIndex; index < dataLoaded.Count; index++)
            {
                string line;
                line = dataLoaded[index];

                List<string> fields = new List<string>();
                fields.AddRange(SeparateOnFields(line, configScriptableObject.Separators));

                // if the line does not have a object name defined then we skip it because we understand it is not yet filled
                int objectNamePosition = settings.ObjectNamePosition;
                if (string.IsNullOrEmpty(fields[objectNamePosition]) == true)
                    continue;         // early exit

                // queremos setear los valores
                DC_ImportedSpeachStructure tempMapedSpeach = new DC_ImportedSpeachStructure(configScriptableObject);

                if (fields.Count != 0)
                {
                    // iterate thorugh the elements of the line
                    for (int fieldIndex = 0; fieldIndex < fields.Count; fieldIndex++)
                    {
                        string currentField = fields[fieldIndex];
                        tempMapedSpeach.SetValue(fieldIndex, currentField);             // add the values to the temporal data container
                    }

                    separatedWords.AddRange(fields);
                }

                tempMapedSpeach.PrintStoredData();
                CreateSO_SpeachStructure(tempMapedSpeach);
            }

        }

        public void GenerateQAObjects()
        {
            SO_QuestionAnswerStructureCSVImportSettings questionSettings;
            string _dataBasePath;

            _dataBasePath = GetBasePath(qaData_CSV);
            questionSettings = configScriptableObject.GetQACSVSettings();

            // parse the data and separate it on words
            List<string> separatedWords = new List<string>();
            // get the data from the csv
            List<string> dataLoaded = new List<string>();
            dataLoaded.AddRange(CSVReader.ReadData(_dataBasePath));

            if (dataLoaded.Count == 0)
            {
                Debug.LogError("No data found on the selected CSV archive");
                return;
            }

            // get starting index
            int startingIndex;
            startingIndex = questionSettings.DataStartLine;

            for (int index = startingIndex; index < dataLoaded.Count; index++)
            {
                string line;
                line = dataLoaded[index];

                List<string> lineFields = new List<string>();
                lineFields.AddRange(SeparateOnFields(line, configScriptableObject.Separators));

                // if the line does not have a object name defined then we skip it because we understand it is not yet filled
                int objectNamePosition = questionSettings.ObjectName;
                int answerNamePosition = questionSettings.AnswerIdPosition;
                if (string.IsNullOrEmpty(lineFields[objectNamePosition]) && 
                    string.IsNullOrEmpty(lineFields[answerNamePosition]))
                {
                    continue;
                }



                if (lineFields.Count != 0)
                {
                    // is there a question? ------------------------------------------------------------------------------------------------------- //
                    if (CheckField(lineFields[objectNamePosition])) 
                    {
                        Debug.LogError("Creating quetion structure");

                        // create the question
                        DC_ImportedQuestionAnswerStructure tempMapedSpeach = new DC_ImportedQuestionAnswerStructure(configScriptableObject);
                        // from quetion id to before arriving to answer id
                        for (int fieldIndex = questionSettings.QuestionIDPosition; fieldIndex < questionSettings.AnswerIdPosition; fieldIndex++)
                        {
                            string currentField = lineFields[fieldIndex];
                            tempMapedSpeach.SetValue(fieldIndex, currentField);             // add the values to the temporal data container
                        }
                        // separatedWords.AddRange(lineFields);

                        tempMapedSpeach.PrintStoredData();
                        CreateSO_QAStructure(tempMapedSpeach);
                    }
                    // ---------------------------------------------------------------------------------------------------------------------------- //

                    // is there an answer?  ------------------------------------------------------------------------------------------------------- //
                    if (CheckField(lineFields[answerNamePosition]))
                    {
                        Debug.LogError("Creating answer structure");

                        DC_ImportedAnswerStructure tempMappedAnswer = new DC_ImportedAnswerStructure(questionSettings);

                        for (int fieldIndex = questionSettings.AnswerIdPosition; fieldIndex < lineFields.Count; fieldIndex++)
                        {
                            string currentField = lineFields[fieldIndex];
                            tempMappedAnswer.SetValue(fieldIndex, currentField);
                        }

                        tempMappedAnswer.PrintStoredData();
                        CreateSO_AnswerStructure(tempMappedAnswer);


                    }
                    // ---------------------------------------------------------------------------------------------------------------------------- //


                }



                /*
                if (lineFields.Count != 0)
                {
                    // iterate thorugh the elements of the line
                    for (int fieldIndex = 0; fieldIndex < lineFields.Count; fieldIndex++)
                    {
                        string currentField = lineFields[fieldIndex];

                        // because this object does not remove the spaces
                        if (CheckField(currentField))
                        {
                            if (fieldIndex == objectNamePosition)
                            {

                            }

                            tempMapedSpeach.SetValue(fieldIndex, currentField);             // add the values to the temporal data container
                        }

                    }
                    separatedWords.AddRange(lineFields);
                }

                tempMapedSpeach.PrintStoredData();
                CreateSO_QAStructure(tempMapedSpeach);
                */
            }


        }

        private string[] SeparateOnFields(string _dataLine, char[] _separators, bool _removeEmtpyEntries = true)
        {
            if (_separators.Length != 0)
            {
                string[] separatedWords;

                if (_removeEmtpyEntries)
                    separatedWords = _dataLine.Split(_separators, StringSplitOptions.RemoveEmptyEntries);       // split and remove empty spaces
                else
                    separatedWords = _dataLine.Split(_separators);                                              // split and preserve empty spaces

                Debug.LogWarning("Separated words = " + separatedWords.Length);
                return separatedWords;
            }

            return null;
        }

        private void CreateSO_SpeachStructure(DC_ImportedSpeachStructure _mappedImprtedStructure)
        {
            SO_SpeachStructure newSpeachStructure = ScriptableObject.CreateInstance<SO_SpeachStructure>();

            string mainFolderPath = configScriptableObject.GetSPeachCSVSettings().GetMainFolderPath();
            string extension = ".asset";
            string objectName = "SpeachStructure_" + _mappedImprtedStructure.GetName();
            string finalPath = mainFolderPath + '/' + objectName + extension;

            AssetDatabase.CreateAsset(newSpeachStructure, finalPath);

            Debug.LogWarning("Created Spech Object " + _mappedImprtedStructure.GetName());
        }

        private void CreateSO_QAStructure(DC_ImportedQuestionAnswerStructure _mappedImprtedStructure)
        {
            SO_QuestionAnswerStructure newQAStrcuture = ScriptableObject.CreateInstance<SO_QuestionAnswerStructure>();

            string mainFolderPath = configScriptableObject.GetQACSVSettings().GetMainFolderPath();
            string extension = ".asset";
            string objectName = "QAStructure_" + _mappedImprtedStructure.GetName();
            string finalPath = mainFolderPath + '/' + objectName + extension;

            AssetDatabase.CreateAsset(newQAStrcuture, finalPath);

            Debug.LogWarning("Created QA Object " + _mappedImprtedStructure.GetName());
        }

        private void CreateSO_AnswerStructure(DC_ImportedAnswerStructure _mappedImportedAnswerStructure)
        {



        }

        // true if correct
        private bool CheckField(string _word)
        {
            return !string.IsNullOrEmpty(_word);
        }

        private bool CheckLineValidity(string _word)
        {
            // is not empty
            if (string.IsNullOrEmpty(_word))
                return false;

            // and is not a letter (we want a character
            foreach (char character in _word)
            {
                if (char.IsNumber(character) == false)
                    return false;
            }

            return true;
        }


        private static string GetBasePath(TextAsset _textAssetCSV)
        {
            string _dataBasePath;

            if (_textAssetCSV)
                _dataBasePath = AssetDatabase.GetAssetPath(_textAssetCSV);               // probar si funciona tambien fuera del editor
            else
                throw new NullReferenceException("Speach structure is null");

            return _dataBasePath;
        }

    }
}

