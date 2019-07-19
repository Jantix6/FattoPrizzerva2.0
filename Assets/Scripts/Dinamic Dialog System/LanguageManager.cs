using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Language
{
    NONE = 0,
    SPANISH = 1,
    ENGLISH = 2,
    CATALAN = 3
}


public  static class LanguageManager 
{
    [SerializeField] public static Language gameLanguage = Language.CATALAN;


    public static void SetGameLanguage(Language _selectedLanguage)
    {
        if (_selectedLanguage != gameLanguage)
        {
            ProcessLanguageChange(_selectedLanguage);
        }

    }

    // If we need to perform some actions after the lenguage change
    private static void ProcessLanguageChange(Language _newLanguage)
    {
        gameLanguage = _newLanguage;
    }

}
