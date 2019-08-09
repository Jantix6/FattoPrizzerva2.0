using System.Collections;
using System.Collections.Generic;
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
