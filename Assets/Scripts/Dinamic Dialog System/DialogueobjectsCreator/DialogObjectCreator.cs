using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

namespace Dialogues
{

    public class DialogObjectCreator : MonoBehaviour
    {
        [SerializeField] private string _dataBasePath = @"D:\dialogsData.csv";

        // parsing
        private List<char> separators = new List<char>() { ';', };


        // generation
        // esto podria estar guardado en un struct para facilitar la modificacion
        private enum Fields
        {
            TYPE = 0,
            ID = 1,
            CAT = 2,
            CAST = 4,
            ENG = 5,
        }

        // called by a button
        public void StartObjectGeneration()
        {
            // load the data
            List<string> dataLoaded = CSVReader.ReadData(_dataBasePath);


            // parse the data and separate it on words
            foreach (string line in dataLoaded)
            {
                List<string> words = SeparateOnWords(line);

                GenerateObject(words);
            }

            GenerateObject(dataLoaded);

        }

        public List<string> SeparateOnWords(string _dataLine)
        {
            List<string> separatedWords = new List<string>();
            separatedWords = _dataLine.Split(';').ToList<string>();

            return separatedWords;
        }

        // MINIMIZA EL USO DE LOS IFS
        private void GenerateObject(List<string> _words)
        {
            Type objectType;
            string objectType_Str = "";
            int objectID = 0;
            Language language = Language.NONE;

            for (int index = 0; index < _words.Count; index++)
            {
                // set the type
                if (index == (int)Fields.TYPE)
                {
                    if (_words[index] == "Q")
                    {
                        objectType = typeof(SO_QuestionAnswerStructure);
                        objectType_Str = objectType.ToString();

                    }
                    else if (_words[index] == "A")
                    {
                        objectType = typeof(SO_Answer);
                        objectType_Str = objectType.ToString();

                    }

                }
                else if (index == (int)Fields.ID)
                {
                    objectID = int.Parse(_words[index]);
                }
                else if (index == (int)Fields.CAT || index == (int)Fields.CAST|| index == (int)Fields.ENG) 
                {
                    language = Language.NONE;
                }


            string _debugMesage = "";
            _debugMesage += "The object that is going to be created have\n " +
                            "OBJECT TYPE: " + objectType_Str + "\n" +
                            "OBJECT ID: " + objectID + "\n" +
                            "LANGUAGE: " + language.ToString() ;
            Debug.LogWarning(_debugMesage);
            
        }


    }

}
}

