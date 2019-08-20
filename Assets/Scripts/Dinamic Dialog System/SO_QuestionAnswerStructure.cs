using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Dialogues
{
    [CreateAssetMenu(fileName = "QAStructure", menuName = "QAStructure/New QA Structure")]
    public class SO_QuestionAnswerStructure : SO_DialogStructure
    {
        [SerializeField] private List<SO_Answer> answers_Lst;
        [SerializeField] private SO_langaugeBasedStringContainer question;

        public string GetQuestion(Language _targetLanguage)
        {
            if (question)
            {
                SO_LanguageBasedString desiredLBS = null;
                desiredLBS = question.GetLanguageBasedString(_targetLanguage, this.name);
                return desiredLBS.text;
            }

            return null;
            
        }
        public void SetQuestion(Language _targetLanguage, string _text)
        {
            SO_LanguageBasedString desiredLBS = null;
            desiredLBS = question.GetLanguageBasedString(_targetLanguage, this.name);
            desiredLBS.text = _text;
        }
        public void SetQuestion(SO_langaugeBasedStringContainer _questionLBS)
        {
            question = _questionLBS;
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
      
        public void AddAnswer(SO_Answer answer)
        {
            if (CheckAnswersIntegrity() == false)
                answers_Lst = new List<SO_Answer>();

            answers_Lst.Add(answer);
        }

        private bool CheckAnswersIntegrity()
        {
            if ( answers_Lst == null || answers_Lst.Count == 0)
            {
                Debug.LogError("No answers found on object " + this.name);
                return false;

            } else
            {
                return true;
            }
        }

        
    }
}

