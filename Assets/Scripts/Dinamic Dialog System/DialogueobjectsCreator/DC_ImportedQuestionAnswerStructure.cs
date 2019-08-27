using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogues
{
    public sealed class DC_ImportedQuestionAnswerStructure : I_ImportedDialogStructure
    {
        private SO_DialogObjectCreatorConfig config;

        private string id;
        private string objectName;

        private string catQuestion;
        private string espQuestion;
        private string engQuestion;

        private List<DC_ImportedAnswerStructure> answers;
        private DC_ImportedAnswerStructure answer;

        public string GetName()
        {
            return objectName;
        }

        public DC_ImportedQuestionAnswerStructure(SO_DialogObjectCreatorConfig configScriptableObject)
        {
            this.config = configScriptableObject;
        }

        public void PrintStoredData()
        {
            string data = string.Empty;

            data += "id " + id + "\n" +
                    "catQuestion " + catQuestion + "\n" +
                    "espQuestion " + espQuestion + "\n" +
                    "engQuestion " + engQuestion + "\n"
                    ;

            Debug.Log(data);
        }

        public void SetValue(int _indexOnLine, string _fieldData)
        {
            SO_QuestionAnswerStructureCSVImportSettings qaConfig = config.GetQACSVSettings();

            if (!qaConfig)
                throw new MissingReferenceException("SO_QuestionAnswerStructureCSVImportSettings not assigned");

            // question id
            if (_indexOnLine == qaConfig.QuestionIDPosition)
            {
                id = _fieldData;
            }
            // QA Name
            else if (_indexOnLine == qaConfig.ObjectName)
            {
                objectName = _fieldData;
            }
            // cat question
            else if (_indexOnLine == qaConfig.CatQuestionPosition)
            {
                catQuestion = _fieldData;
            }
            // esp question
            else if (_indexOnLine == qaConfig.EspQuestionPosition)
            {
                espQuestion = _fieldData;
            }
            // eng question;
            else if (_indexOnLine == qaConfig.EngQuestionPosition)
            {
                engQuestion = _fieldData;
            }
            // answers
            else if (_indexOnLine >= qaConfig.AnswerIdPosition)                 // equal or more than the position of the answer id
            {
                if (answer == null) 
                    answer = new DC_ImportedAnswerStructure(qaConfig);

                answer.SetValue(_indexOnLine, _fieldData);

                if (answers == null)
                    answers = new List<DC_ImportedAnswerStructure>();

                answers.Add(answer);
            }
      
        }
    }


}



