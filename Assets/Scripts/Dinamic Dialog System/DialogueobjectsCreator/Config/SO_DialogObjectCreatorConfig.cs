using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Dialogues
{
    [CreateAssetMenu(fileName = "Dialogues object creator Config", menuName = "Dialogues object creator Config")]
    public class SO_DialogObjectCreatorConfig : ScriptableObject
    {
        [SerializeField] private char[] separators;                       // object that separate each cell from each other (currently is a ;)
        [SerializeField] private string creationFolder;

        // data related to the string parsed (so there are neither blanck fields nor separators)
        [Header("Configurators")]
        [SerializeField] private SO_SpeachStructureCSVImportSettins speachCSVConfiguration;
        [SerializeField] private SO_QuestionAnswerStructureCSVImportSettings qaCSVConfiguration;


        private void OnEnable()
        {
            if (string.IsNullOrEmpty(creationFolder))
                Debug.LogError("Creation folder for the objects should be defined");
            if (separators == null || separators.Length == 0)
                Debug.LogError("Separators are needed in order to the correct performance of the Dialog objects generator");
        }

        public char[] Separators => separators;

        public int CatTitlePosition { get => speachCSVConfiguration.catTitlePosition; set => speachCSVConfiguration.catTitlePosition = value; }
        public int ObjectIdentifierPosition { get => speachCSVConfiguration.objectIdentifierPosition; set => speachCSVConfiguration.objectIdentifierPosition = value; }
        public int EspTitlePosition { get => speachCSVConfiguration.espTitlePosition; set => speachCSVConfiguration.espTitlePosition = value; }
        public int EngTitlePosition { get => speachCSVConfiguration.engTitlePosition; set => speachCSVConfiguration.engTitlePosition = value; }
        public int CatBodyPosition { get => speachCSVConfiguration.catBodyPosition; set => speachCSVConfiguration.catBodyPosition = value; }
        public int EspBodyPosition { get => speachCSVConfiguration.espBodyPosition; set => speachCSVConfiguration.espBodyPosition = value; }
        public int EngBodyPosition { get => speachCSVConfiguration.engBodyPosition; set => speachCSVConfiguration.engBodyPosition = value; }

        public SO_SpeachStructureCSVImportSettins GetSPeachCSVSettings()
        {
            if (speachCSVConfiguration)
                return speachCSVConfiguration;
            else
                Debug.LogError("ERROR; YOu need to set up the Speach Structure CSV import settinggs");

            return null;
        }

    }




}


