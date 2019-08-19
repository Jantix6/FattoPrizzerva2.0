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

        //private List<DC_ImportedSpeachStructure> speachStructures_Lst;
       // private List<DC_ImportedQuestionAnswerStructure> qaStructures_Lst;

        // called by a button
        public void StartObjectGeneration()
        {
            // load the data
            // List<string> dataLoaded = CSVProcesser.ReadDataFromPath(_dataBasePath);




        }

        private void GenerateCSVObjects()
        {
           
            GenerateSpeachObjects();
            GenerateQAObjects();
        }

        public void GenerateSpeachObjects()
        {
             string _dataBasePath;

            if (speachData_CSV)
                _dataBasePath = AssetDatabase.GetAssetPath(speachData_CSV);               // probar si funciona tambien fuera del editor
            else
                throw new NullReferenceException("Speach structure is not set");

            // parse the data and separate it on words
            List<string> separatedWords = new List<string>();
            // get the data from the csv
            List<string> dataLoaded = new List<string>();
            dataLoaded.AddRange(CSVReader.ReadData(_dataBasePath));
            foreach (string line in dataLoaded)
            {
                List<string> lineSeparatedWords = new List<string>();
                lineSeparatedWords.AddRange(SeparateOnWords(line, configScriptableObject.Separators));

                // queremos setear los valores
                DC_ImportedSpeachStructure tempMapedSpeach = new DC_ImportedSpeachStructure(configScriptableObject);

                if (lineSeparatedWords.Count != 0)
                {
                    // iterate thorugh the elements of the line
                    for (int fieldIndex = 0; fieldIndex < lineSeparatedWords.Count; fieldIndex++)
                    {
                        string currentField = lineSeparatedWords[fieldIndex];
                        tempMapedSpeach.SetValue(fieldIndex, currentField);             // add the values to the temporal data container
                    }

                    separatedWords.AddRange(lineSeparatedWords);
                }

                tempMapedSpeach.PrintStoredData();
                CreateSO_SpeachStructure(tempMapedSpeach);

            }
        }

        public void GenerateQAObjects()
        {
            string _dataBasePath;

            if (qaData_CSV)
                _dataBasePath = AssetDatabase.GetAssetPath(speachData_CSV);               // probar si funciona tambien fuera del editor
            else
                throw new NullReferenceException("QA structure is not set");

            // parse the data and separate it on words
            List<string> separatedWords = new List<string>();
            // get the data from the csv
            List<string> dataLoaded = new List<string>();
            dataLoaded.AddRange(CSVReader.ReadData(_dataBasePath));

            foreach (string line in dataLoaded)
            {
                List<string> lineSeparatedWords = new List<string>();
                lineSeparatedWords.AddRange(SeparateOnWords(line, configScriptableObject.Separators));

                // queremos setear los valores
                DC_ImportedQuestionAnswerStructure tempMapedSpeach = new DC_ImportedQuestionAnswerStructure(configScriptableObject);

                if (lineSeparatedWords.Count != 0)
                {
                    // iterate thorugh the elements of the line
                    for (int fieldIndex = 0; fieldIndex < lineSeparatedWords.Count; fieldIndex++)
                    {
                        string currentField = lineSeparatedWords[fieldIndex];
                        tempMapedSpeach.SetValue(fieldIndex, currentField);             // add the values to the temporal data container
                    }
                    separatedWords.AddRange(lineSeparatedWords);
                }

                tempMapedSpeach.PrintStoredData();
                CreateSO_QAStructure(tempMapedSpeach);
            }

        }

        private string[] SeparateOnWords(string _dataLine, char[] _separators)
        {
            if (_separators.Length != 0)
            {
                string[] separatedWords = _dataLine.Split(_separators, StringSplitOptions.RemoveEmptyEntries);        // split and remove empty spaces
                return separatedWords;
            }

            return null;
        }

        private void CreateSO_SpeachStructure(DC_ImportedSpeachStructure _mappedImportedStructure)
        {
            SO_SpeachStructure newSpeachStructure = ScriptableObject.CreateInstance<SO_SpeachStructure>();

            string mainFolderPath = configScriptableObject.GetSPeachCSVSettings().GetMainFolderPath();
            string extension = ".asset";
            string objectName = "SpeachStructure";
            string finalPath = mainFolderPath + '/' + objectName + extension;

            AssetDatabase.CreateAsset(newSpeachStructure, finalPath);

            Debug.LogWarning("Created Spech Object");
        }

        private void CreateSO_QAStructure(DC_ImportedQuestionAnswerStructure _mappedImprtedStructure)
        {
            SO_QuestionAnswerStructure newQAStrcuture = ScriptableObject.CreateInstance<SO_QuestionAnswerStructure>();

            string mainFolderPath = configScriptableObject.GetSPeachCSVSettings().GetMainFolderPath();
            string extension = ".asset";
            string objectName = "QAStructure";
            string finalPath = mainFolderPath + '/' + objectName + extension;

            AssetDatabase.CreateAsset(newQAStrcuture, finalPath);

            Debug.LogWarning("Created QA Object");

        }


    }
}

