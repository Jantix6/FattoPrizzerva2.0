using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.UIElements;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

[SerializeField]
[CreateAssetMenu(fileName = "Text", menuName = "new Language Constrained text")]
public class LanguageBasedString : ScriptableObject
{
    // Control
    public bool Usable { get; set; }


    public Language language;

    [TextArea] public string text;


    private void OnEnable()
    {
        if (language == Language.NONE)
        {
            AutoLanguageIdentification();
        }
       

    }


    private void AutoLanguageIdentification()
    {
        string identifiedLanguage = ReadLanguageFromName();
        if (identifiedLanguage != "")
        {
            if (identifiedLanguage == "CAT")
                language = Language.CATALAN;
            else if (identifiedLanguage == "ESP" || identifiedLanguage == "CAST")
                language = Language.SPANISH;
            else if (identifiedLanguage == "ENG")
                language = Language.ENGLISH;
        }

        Debug.LogWarning("Lenguage identified as " + language);
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

        Debug.LogError("No language identified by the name of the object (LLL_NameOfTheObject");
        return null;
    }

    public static void CheckListIntegrity(Language _targetLanguage, List<LanguageBasedString> _list, string _caller)
    {
        string debugString = "";
        debugString += "The language " + nameof(_targetLanguage) + " is not defined on the object " + _caller + "\n";
        foreach (LanguageBasedString lString in _list)
        {
            debugString += "REPORT OF " + lString.name + ": --> " + lString.language + "\n";
        }
        Debug.LogError(debugString);

    }
}


