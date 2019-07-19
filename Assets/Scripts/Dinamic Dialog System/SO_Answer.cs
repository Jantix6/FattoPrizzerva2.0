using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Dialogues
{
    [CreateAssetMenu(fileName = "Answer", menuName = "Answers/New Answer")]
    public class SO_Answer : ScriptableObject 
    {
        [SerializeField] private List<LanguageBasedString> answers;

        [SerializeField] private SO_DialogStructure targetStructure;
        
        public string GetAnswerBody(Language _targetLanguage)
        {
            //Debug.LogError("Requested lenguage" + _targetLanguage);

            if (answers.Count == 0)
            {
                Debug.LogError("No answers found on object " + this.name);
                return null;
            }

            for (int i = 0; i < answers.Count; i++)
            {
                if (answers[i].language == _targetLanguage)
                    return answers[i].text;
            }

            //Console.WriteLine("The language {0} is not defined on the object {1}", nameof(_targetLanguage), this.name);
            Debug.LogError("The language " + nameof(_targetLanguage) + " is not defined on the object " + this.name);
            return null;
        }

        /*
        public string GetAnswerBody()
        {
            return answerBody;
        }
        */
        public SO_DialogStructure GetTargetStructure()
        {
            return targetStructure;
        }


    }



}

