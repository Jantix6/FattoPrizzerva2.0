using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogues
{
    [CreateAssetMenu(fileName = "Config Speach CSV Object Config", menuName = "Config Speach CSV Object Config")]
    public sealed class SO_SpeachStructureCSVImportSettins : ScriptableObject
    {
        [SerializeField] private string speachStructuresMainFolderPath;

        [SerializeField] public int objectIdentifierPosition = 0;
        [SerializeField] public int catTitlePosition = 1;
        [SerializeField] public int espTitlePosition = 2;
        [SerializeField] public int engTitlePosition = 3;
        [SerializeField] public int catBodyPosition = 4;
        [SerializeField] public int espBodyPosition = 5;
        [SerializeField] public int engBodyPosition = 6;

        public string GetMainFolderPath()
        {
            if (string.IsNullOrEmpty(speachStructuresMainFolderPath) == false)
                return speachStructuresMainFolderPath;
            else
                Debug.LogError("ERROR_ YOu should define a path in order to create the speach objects and save them");
            return null;
        }
    }
}


