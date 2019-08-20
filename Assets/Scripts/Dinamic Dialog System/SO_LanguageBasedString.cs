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
        private static Dictionary<string   , Language> languagesDictionary;


        private static void InitializeLanguagesDictinary()
        {
            languagesDictionary = new Dictionary<string, Language>();
            languagesDictionary.Add("CAT", Language.CATALAN);
            languagesDictionary.Add("ESP", Language.SPANISH);
            languagesDictionary.Add("ENG", Language.ENGLISH);
        }
        public static Dictionary<string, Language> GetLanguagesDictioanry()
        {
            if (languagesDictionary == null)
                InitializeLanguagesDictinary();

            return languagesDictionary;
        }
        public void OnEnable()
        {
            // add language relations
            InitializeLanguagesDictinary();

            GuesLanguage();

            // ATENCIÓN ------------------------------------------------------------------------------------------------------ //
            if (text is null)
                text = "";              // SO- si no le asignas un valor a la variable string en este SO esta es nula!!!!!!!!!!!!
            // Mucho ojo con los SO ------------------------------------------------------------------------------------------ //
        }

        public void GuesLanguage()
        {
            //Debug.LogError("Calling gues language" + this.name);
            Debug.LogWarning("2_Calling gues language " + this.name);

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
            Debug.LogWarning("2-1_Calling gues AutoLanguageIdentification " + this.name);

            string identifiedLanguage = null;
            identifiedLanguage = ReadLanguageFromName();
            Language returnLanguage = Language.NONE;

            if (identifiedLanguage != null)
            {
                languagesDictionary.TryGetValue(identifiedLanguage, out returnLanguage);
            }
            
            return returnLanguage;

        }

        private string ReadLanguageFromName()
        {
            Debug.LogWarning("2-2_Calling ReadLanguageFromName " + this.name);

            string _languageExpresion = "";
            string separation = "_";
            string objectName = this.name;

            if (objectName.Length > 0)
            {
                foreach (char character in objectName)
                {
                    if (character.ToString() == separation.ToString())
                    {
                        return _languageExpresion;
                    }
                    _languageExpresion += character;
                }

            } else
            {
                Debug.LogWarning("No name set on object " + this);
                return null;
            }

            Debug.LogError("No language identified by the name of the object (is MANDATORY to use the structure: LLL_NameOfTheObject" + objectName + " " + _languageExpresion);
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
