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
            Debug.LogError("Requested lenguage" + _targetLanguage);

            if (questions.Count == 0)
            {
                Debug.LogError("No titles found on object " + this.name);
                return null;
            }

            for (int i = 0; i < questions.Count; i++)
            {
                if (questions[i].language == _targetLanguage)
                    return questions[i].text;         
            }

            LanguageBasedString.CheckIfLanguageSet(_targetLanguage, questions, this.name);
            return null;


        }

        public List<SO_Answer> GetAnswers()
        {
            return answers_Lst;
        }

    }
}

