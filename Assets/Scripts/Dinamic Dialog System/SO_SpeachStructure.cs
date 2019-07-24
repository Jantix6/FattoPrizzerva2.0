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
            if (LanguageBasedString.CheckListIntegrity(titles,_targetLanguage,this.name)) {

                for (int i = 0; i < titles.Count; i++)
                {
                    if (titles[i].language == _targetLanguage)
                        return titles[i].text;
                }

            }

            return null;
        }
        internal string GetSpeachBody(Language _targetLanguage)
        {
            if (LanguageBasedString.CheckListIntegrity(bodies,_targetLanguage,this.name))
            {
                for (int i = 0; i < bodies.Count; i++)
                {
                    if (bodies[i].language == _targetLanguage)
                        return bodies[i].text;
                }
            }
     
            return null;

        }




    }

}

