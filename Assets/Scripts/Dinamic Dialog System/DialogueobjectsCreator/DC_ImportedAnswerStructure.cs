using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogues
{
    [System.Serializable]
    public sealed class DC_ImportedAnswerStructure : I_ImportedDialogStructure
    {
        SO_QuestionAnswerStructureCSVImportSettings qaConfig;

        private string id;

        private string catAnswer;
        private string espAnswer;
        private string engAnswer;

        public DC_ImportedAnswerStructure(SO_QuestionAnswerStructureCSVImportSettings configScriptableObject)
        {
            qaConfig = configScriptableObject;
        }

        public string CatAnswer { get => catAnswer; set => catAnswer = value; }
        public string EspAnswer { get => espAnswer; set => espAnswer = value; }
        public string EngAnswer { get => engAnswer; set => engAnswer = value; }
        public string Id { get => id; set => id = value; }

        public void PrintStoredData()
        {
            string data = string.Empty;

            data += "ANSWER id " + id + "\n" +
                    "catAnswer " + catAnswer + "\n" +
                    "espAnswer " + espAnswer + "\n" +
                    "engAnswer " + engAnswer + "\n"
                    ;

            Debug.Log(data);
        }

        public void SetValue(int _indexOnLine, string _fieldData)
        {
            // answer id
            if (_indexOnLine == qaConfig.AnswerIdPosition)
            {
                this.Id = _fieldData;
            }
            else if (_indexOnLine == qaConfig.CatAnswerBodyPosition)
            {
                this.CatAnswer = _fieldData;
            }
            else if (_indexOnLine == qaConfig.EspAnswerBodyPosition)
            {
                this.EspAnswer = _fieldData;
            }
            else if (_indexOnLine == qaConfig.EngAnswerBodyPosition)
            {
                this.EngAnswer = _fieldData;
            }
        }
    }
}
