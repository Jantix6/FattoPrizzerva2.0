using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Dialogues
{
    [CreateAssetMenu(fileName = "Dialogues object creator Config", menuName = "Dialogues object creator Config")]
    public class SO_DialogObjectCreatorConfig : ScriptableObject
    {
        [SerializeField] private char[] separators;                       // object that separate each cell from each other (currently is a ;)

        // data related to the string parsed (so there are neither blanck fields nor separators)
        [Header("Configurators")]
        [SerializeField] private SO_SpeachStructureCSVImportSettings speachCSVConfiguration;
        [SerializeField] private SO_QuestionAnswerStructureCSVImportSettings qaCSVConfiguration;


        private void OnEnable()
        {
            if (separators == null || separators.Length == 0)
                Debug.LogError("Separators are needed in order to the correct performance of the Dialog objects generator");
        }

        public char[] Separators => separators;


        public SO_SpeachStructureCSVImportSettings GetSPeachCSVSettings()
        {
            if (speachCSVConfiguration)
                return speachCSVConfiguration;
            else
                Debug.LogError("ERROR; YOu need to set up the Speach Structure CSV import settinggs");

            return null;
        }

        public SO_QuestionAnswerStructureCSVImportSettings GetQACSVSettings()
        {
            if (qaCSVConfiguration)
                return qaCSVConfiguration;
            else
                Debug.LogError("ERROR; YOu need to set up the Question Answer Structure CSV import settinggs");

            return null;
        }

    }




}


