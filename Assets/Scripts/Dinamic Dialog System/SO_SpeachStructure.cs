using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Dialogues
{
    [CreateAssetMenu(fileName = "SpeachStructure", menuName = "QAStructure/New SPEACH Structure")]
    public class SO_SpeachStructure : SO_DialogStructure 
    {
        [Header("Object data by langauge")]
        public SO_langaugeBasedStringContainer title;
        public SO_langaugeBasedStringContainer body;

        [Header("Target structure")]
        [SerializeField] private SO_DialogStructure nextDialogStructure;

        public string GetSpeachTitle(Language _targetLanguage)
        {
            SO_LanguageBasedString desiredLBS = null;
            desiredLBS = title.GetLanguageBasedString(_targetLanguage,this.name);

            return desiredLBS.text;
        }
        public string GetSpeachBody(Language _targetLanguage)
        {
            SO_LanguageBasedString desiredLBS = null;
            desiredLBS = body.GetLanguageBasedString(_targetLanguage, this.name);

            return desiredLBS.text;
        }
        public SO_DialogStructure GetTargetStructure()
        {
            return nextDialogStructure;
        }



    }

}

