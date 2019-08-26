using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogues
{
    public abstract class CSVObject_ImportSettings : ScriptableObject
    {
        // Do not touch this unless is extremelly necesary (future changes on the engine)
        private string assetsFolderName = "Assets";

        [Header("Path for object to instantiate on")]
        [SerializeField] protected string objectInstantiationPath;

        public string GetMainFolderPath()
        {
            if (string.IsNullOrEmpty(objectInstantiationPath) == false)
            {
                ProcessGlobalPath();

                return objectInstantiationPath;
            }
            else
                Debug.LogError("ERROR_ YOu should define a path in order to create the speach objects and save them");
            return null;
        }

        private void ProcessGlobalPath()
        {
            // get the local path from the global path 
            int indexOfAssets = objectInstantiationPath.IndexOf(assetsFolderName);
            string localPath = objectInstantiationPath.Substring(indexOfAssets);

            objectInstantiationPath = localPath;

            // swap the \ with / to unity to understand the path
            objectInstantiationPath = objectInstantiationPath.Replace(@"\", "/");
        }
    }
}
