using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogues
{
    [CreateAssetMenu(fileName = "QAStructure", menuName = "QAStructure/New QA Structure")]
    public class SO_QuestionAnswerStructure : SO_DialogStructure
    {
        //[SerializeField] [TextArea] private string question;
        // [SerializeField] private List<LanguageBasedString> questions;
        [SerializeField] private List<SO_Answer> answers_Lst;

        [SerializeField] private SO_langaugeBasedStringContainer question;

        public string GetQuestion(Language _targetLanguage)
        {
            SO_LanguageBasedString desiredLBS = null;
            desiredLBS = question.GetLanguageBasedString(_targetLanguage, this.name);
            return desiredLBS.text;
        }

        public List<SO_Answer> GetAnswers()
        {
            if (CheckAnswersIntegrity())
            {
                return answers_Lst;
            }
            else
            {
                Debug.LogError("There is some problem with the answers of the object " + this.name);
                return null;
            }

            
        }
      
        private bool CheckAnswersIntegrity()
        {
            if (answers_Lst.Count == 0 || answers_Lst == null)
            {
                Debug.LogError("No titles found on object " + this.name);
                return false;

            } else
            {
                return true;
            }
        }


    }
}

