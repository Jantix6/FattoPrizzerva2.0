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
            if (title != null)
            {
                SO_LanguageBasedString desiredLBS = null;
                desiredLBS = title.GetLanguageBasedString(_targetLanguage, this.name);

                return desiredLBS.text;
            }
            return null;

        }
        public string GetSpeachBody(Language _targetLanguage)
        {
            if (body != null)
            {
                SO_LanguageBasedString desiredLBS = null;
                desiredLBS = body.GetLanguageBasedString(_targetLanguage, this.name);

                return desiredLBS.text;
            }
            return null;


        }
        public void SetSpeachBody(Language _targetLanguage, string _newText)
        {
            SO_LanguageBasedString desiredLBS = null;
            desiredLBS = body.GetLanguageBasedString(_targetLanguage, this.name);

            desiredLBS.text = _newText;

        }
        public SO_DialogStructure GetTargetStructure()
        {
            return nextDialogStructure;
        }

        public void SetTitleBody(Language _targetLanguage, string text)
        {
            SO_LanguageBasedString desiredLBS = null;
            desiredLBS = title.GetLanguageBasedString(_targetLanguage, this.name);

            desiredLBS.text = text;
        }
        public void SetTitle(SO_langaugeBasedStringContainer _title)
        {
            title = _title;
        }
        public void SetBody(SO_langaugeBasedStringContainer _body)
        {
            body = _body;
        }
    }

}

