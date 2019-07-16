using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Dialogues
{
    [CreateAssetMenu(fileName = "Answer", menuName = "Answers/New Answer")]
    public class Answer : ScriptableObject
    {
        [SerializeField] [TextArea] private string answerBody;
        [SerializeField] private QuestionAnswerStructure targetStructure;
        
        public string GetAnswerBody()
        {
            return answerBody;
        }
        public QuestionAnswerStructure GetTargetStructure()
        {
            return targetStructure;
        }


    }

}

