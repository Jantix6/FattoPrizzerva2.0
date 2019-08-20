using System.Collections;
using System.Collections.Generic;
using UnityEditor;
/*using UnityEditor.Experimental.UIElements;*/
using UnityEngine;
using UnityEngine.Experimental.UIElements;

namespace Dialogues
{
    [SerializeField]
    [CreateAssetMenu(fileName = "Text", menuName = "new Language Constrained text")]
    public class SO_LanguageBasedString : ScriptableObject
    {
        // Control
        public bool Usable { get; set; }
        public Language language;
        [TextArea(4, 228)] public string text;


        private void OnEnable()
        {
            // nomenclature based lenguage
            Language expectedLanguage = AutoLanguageIdentification();
            if (language == Language.NONE)
            {
                // auto asignation of language if none is set
                language = expectedLanguage;
            }
            else
            {
                // notifiation of a wrong nomenclature on the object name
                if (language != expectedLanguage && expectedLanguage != Language.NONE)
                {
                    Debug.LogError("The object " + this.name + "might have a nomenclature problem LANGUAGE != NAME LANGUAGE");
                }
            }


        }


        private Language AutoLanguageIdentification()
        {
            string identifiedLanguage = ReadLanguageFromName();
            if (identifiedLanguage != "")
            {
                if (identifiedLanguage == "CAT")
                    return Language.CATALAN;
                else if (identifiedLanguage == "ESP" || identifiedLanguage == "CAST")
                    return Language.SPANISH;
                else if (identifiedLanguage == "ENG")
                    return Language.ENGLISH;
            }
            else
            {
                return Language.NONE;
            }

            return Language.NONE;

        }

        private string ReadLanguageFromName()
        {
            string _languageExpresion = "";
            string separation = "_";

            foreach (char character in this.name)
            {
                if (character.ToString() == separation.ToString())
                {
                    return _languageExpresion;
                }

                _languageExpresion += character;
            }

            Debug.LogError("No language identified by the name of the object (is MANDATORY to use the structure: LLL_NameOfTheObject");
            return null;
        }

        // PROCESS ---------------------------------------------------------------------------------------------------------- //
        public static bool CheckListIntegrity(List<SO_LanguageBasedString> _languageBasedStrings, Language _targetLanguage, string _callerName)
        {
            if (_languageBasedStrings.Count == 0 || _languageBasedStrings == null)
            {
                Debug.Log("The list of language Based Strings is empty or null on " + _callerName);
                return false;
            }
            else if (!CheckIfLanguageSet(_targetLanguage, _languageBasedStrings, _callerName))
            {
                Debug.LogError("The language " + _targetLanguage + " is not set on a list of " + _callerName);
                return false;
            }
            else
            {
                return true;
            }


        }
        private static bool CheckIfLanguageSet(Language _targetLanguage, List<SO_LanguageBasedString> _list, string _caller)
        {
            bool _isLanguagePresent = false;
            if (_list != null)
            {
                foreach (SO_LanguageBasedString stri in _list)
                {
                    if (stri.language == _targetLanguage)
                    {
                        _isLanguagePresent = true;
                    }
                }

            }
            else
            {
                Debug.LogError(_caller + "Passed an empty list to check for languages");
            }

            return _isLanguagePresent;

        }
    }
}
