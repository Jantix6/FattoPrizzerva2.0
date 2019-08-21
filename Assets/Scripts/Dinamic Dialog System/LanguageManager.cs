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


public static class LanguageManager 
{
    [SerializeField]
    private static Language gameLanguage = Language.NONE;

    public delegate Language LanguageChangeEventManger();
    public static event LanguageChangeEventManger OnLanguageChange;         // event called when language is changed
    
    // Class Constructor
    static LanguageManager()
    {
        
    }

    public static Language GetGameLanguage()
    {
        return gameLanguage;
    }
    public static void SetGameLanguage(Language _selectedLanguage)
    {
        if (_selectedLanguage != gameLanguage && _selectedLanguage != Language.NONE)
        {
            ProcessLanguageChange(_selectedLanguage);

        } else
        {
            //Debug.LogError("You need to define a valid language (selected language = " + _selectedLanguage + ")");
        }

    }

    // If we need to perform some actions before or after the lenguage change we use this method
    private static void ProcessLanguageChange(Language _newLanguage)
    {
        gameLanguage = _newLanguage;
        Debug.Log("lenguage set to " + gameLanguage);
    }



}
