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
        private string _dataBasePath = @"D:\dialogsData.csv";

        [Header("Config data")]
        [SerializeField] private SO_DialogObjectCreatorConfig configScriptableObject;

        private List<DC_ImportedSpeachStructure> speachStructures_Lst;
        private List<DC_ImportedQuestionAnswerStructure> qaStructures_Lst;

        // called by a button
        public void StartObjectGeneration()
        {
            // load the data
            // List<string> dataLoaded = CSVProcesser.ReadDataFromPath(_dataBasePath);

            GenerateSpeachObjects();


        }

        public void GenerateSpeachObjects()
        {
            if (speachData_CSV)
                _dataBasePath = AssetDatabase.GetAssetPath(speachData_CSV);               // probar si funciona tambien fuera del editor

            List<string> dataLoaded = new List<string>();
            dataLoaded.AddRange(CSVReader.ReadData(_dataBasePath));

            // parse the data and separate it on words
            List<string> separatedWords = new List<string>();
            foreach (string line in dataLoaded)
            {
                List<string> lineSeparatedWords = new List<string>();
                lineSeparatedWords.AddRange(SeparateOnWords(line, configScriptableObject.Separators));

                // queremos setear los valores
                DC_ImportedSpeachStructure tempMapedSpeach = new DC_ImportedSpeachStructure(configScriptableObject);

                if (lineSeparatedWords.Count != 0)
                {
                    if (speachStructures_Lst == null)
                        speachStructures_Lst = new List<DC_ImportedSpeachStructure>();

                    // iterate thorugh the elements of the line
                    for (int fieldIndex = 0; fieldIndex < lineSeparatedWords.Count; fieldIndex++)
                    {
                        string currentField = lineSeparatedWords[fieldIndex];
                        tempMapedSpeach.SetValue(fieldIndex, currentField);
                    }

                    separatedWords.AddRange(lineSeparatedWords);
                }

                tempMapedSpeach.PrintStoredData();


                CreateSO_SpeachStructure(tempMapedSpeach);

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

            AssetDatabase.CreateAsset(newSpeachStructure, mainFolderPath + 1 + extension);

        }


    }
}

