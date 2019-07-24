using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogues
{
    [CreateAssetMenu(fileName = "QAStructure", menuName = "QAStructure/New QA Structure")]
    public class SO_QuestionAnswerStructure : SO_DialogStructure
    {
        //[SerializeField] [TextArea] private string question;
        [SerializeField] private List<LanguageBasedString> questions;
        [SerializeField] private List<SO_Answer> answers_Lst;



        public string GetQuestion(Language _targetLanguage)
        {
            if (LanguageBasedString.CheckListIntegrity(questions,_targetLanguage, this.name))
            {
                // buamos el texto de las pregutnas y nos quedamos con el que queremos
                for (int i = 0; i < questions.Count; i++)
                {
                    if (questions[i].language == _targetLanguage)
                        return questions[i].text;                   // encontramos el texto en el idioma que nos intersa
                }

            } 

            // we make sure the language seleted is set at the language based string
            //LanguageBasedString.CheckIfLanguageSet(_targetLanguage, questions, this.name);
            return null;


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

