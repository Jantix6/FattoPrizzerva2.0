using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Dialogues
{
    [CreateAssetMenu(fileName = "SpeachStructure", menuName = "QAStructure/New SPEACH Structure")]
    public class SO_SpeachStructure : SO_DialogStructure 
    {
        // [SerializeField] private string speachTitle;
        // [SerializeField] [TextArea] private string speachBody;

        [Header("Object data by langauge")]
        public List<LanguageBasedString> titles;
        public List<LanguageBasedString> bodies;           // contienen idioma y texto pero es necesario tambien poder ver en que idioma esta desde el inspector

        internal string GetSpeachTitle(Language _targetLanguage)
        {
            Debug.LogError("Requested lenguage" + _targetLanguage);

            if (titles.Count == 0)
            {
                Debug.LogError("No titles found on object " + this.name);
                return null;
            }

            for (int i = 0; i < titles.Count; i++)
            {
                if (titles[i].language == _targetLanguage)
                    return titles[i].text;
            }

            LanguageBasedString.CheckListIntegrity(_targetLanguage, titles, this.name);
            return null;
        }
        internal string GetSpeachBody(Language _targetLanguage)
        {
            Debug.LogError("Requested lenguage" + _targetLanguage);

            if (bodies.Count == 0)
            {
                Debug.LogError("No bodies found on object " + this.name);
                return null;
            }

            for (int i = 0; i < bodies.Count; i++)
            {
                if (bodies[i].language == _targetLanguage)
                    return bodies[i].text;
            }

            //Console.WriteLine("The language {0} is not defined on the object {1}", nameof(_targetLanguage), this.name);
            Debug.LogError("The language " + nameof(_targetLanguage) + " is not defined on the object " + this.name);
            foreach (LanguageBasedString lString in bodies)
            {
                Debug.LogWarning("REPORT OF " + lString.name + ": --> " + lString.language);
            }
            return null;

        }


    }

}

