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

            //Console.WriteLine("The language {0} is not defined on the object {1}", nameof(_targetLanguage), this.name);
            Debug.LogError("The language " + nameof(_targetLanguage) + " is not defined on the object " + this.name);
            foreach (LanguageBasedString lString in questions)
            {
                Debug.LogWarning("REPORT OF " + lString.name + ": --> " + lString.language);
            }
            return null;

        }
        /*
        public string GetQuestion()
        {
            return question;
        }
        */
        public List<SO_Answer> GetAnswers()
        {
            return answers_Lst;
        }

    }
}

