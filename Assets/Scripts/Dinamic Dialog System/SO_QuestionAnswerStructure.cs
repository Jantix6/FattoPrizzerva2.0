using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogues
{
    [CreateAssetMenu(fileName = "QAStructure", menuName = "QAStructure/New QA Structure")]
    public class SO_QuestionAnswerStructure : SO_DialogStructure
    {
        [SerializeField] [TextArea] private string question;
        [SerializeField] private List<SO_Answer> answers_Lst;



        public string GetQuestion()
        {
            return question;
        }
        public List<SO_Answer> GetAnswers()
        {
            return answers_Lst;
        }

    }
}

