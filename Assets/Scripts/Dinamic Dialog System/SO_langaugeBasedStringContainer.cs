using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Dialogues
{
    [CreateAssetMenu(fileName = "LBS_Container", menuName = "NEW LBS Container")]
    [SerializeField]
    public class SO_langaugeBasedStringContainer : ScriptableObject
    {
        [SerializeField] private List<SO_LanguageBasedString> languageBasedStrings_Lst;         // LBSs


        public List<SO_LanguageBasedString> GetLanguageBasedStrings()
        {
            return languageBasedStrings_Lst;
        }
        public void CreateLanguageBasedStrings()
        {
            int _numberOfElements = Enum.GetValues(typeof(Language)).Length - 1;
            string _extension = ".asset";
            string _path;
            int _lastIndex;

            // let the path to the folder
            _path = AssetDatabase.GetAssetPath(this);
            _lastIndex = _path.LastIndexOf('/');
            _path = _path.Remove(_lastIndex);

            // get the folder name by extrracting it from the object path
            _lastIndex = _path.LastIndexOf('/');
            string[] words;
            words = _path.Split('/');
            string _folderName;
            _folderName = words[words.Length - 1];
            Debug.Log(_folderName);
            

            languageBasedStrings_Lst = new List<SO_LanguageBasedString>();

            for (int i = 0; i < _numberOfElements; i++)
            {
                SO_LanguageBasedString newLBS = SO_LanguageBasedString.CreateInstance<SO_LanguageBasedString>();
                newLBS.name = "CAT";

                // falta cambiar la lengua de cada uno para que funcione adecuadamente

                AssetDatabase.CreateAsset(newLBS, _path + '/' + newLBS.name + "_" + _folderName + _extension);
                languageBasedStrings_Lst.Add(newLBS);
                newLBS.OnEnable();
            }
            AssetDatabase.SaveAssets();
        }

        public SO_LanguageBasedString GetLanguageBasedString(Language _desiredLanguage, string _callerName)
        {
            SO_LanguageBasedString returnLBS = null;

            if (SO_LanguageBasedString.CheckListIntegrity(languageBasedStrings_Lst, _desiredLanguage, _callerName))
            {
                foreach (SO_LanguageBasedString LBS in languageBasedStrings_Lst)
                {
                    if (LBS.language == _desiredLanguage)
                    {
                        returnLBS = LBS;
                        break;
                    }
                }
            }
            else
            {
                return null;
            }

            if (returnLBS)
                return returnLBS;
            else
            {
                Debug.LogError("No LBS defined with the required language");
                return null;
            }
        }

        
       


    }
}
