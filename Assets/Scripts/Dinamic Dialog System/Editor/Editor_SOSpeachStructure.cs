using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace Dialogues
{
    [CustomEditor(typeof(SO_SpeachStructure))]
    public class Editor_SOSpeachStructure : Editor
    {
        SO_SpeachStructure structure ;

        Language previewLanguage = Language.ENGLISH;
        Language previousLanguage = Language.NONE;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            structure = (SO_SpeachStructure)target;

            // draw separation line from rest of editor
            GUILayout.Space(40);
            GUILayout.Label("Debug view");

            previewLanguage = (Language)EditorGUILayout.EnumPopup("Language to preview: ", previewLanguage);
            if (previewLanguage != previousLanguage)
                SaveProject();
            previousLanguage = previewLanguage;

            if (previewLanguage != Language.NONE)
            {
                DrawTitle(previewLanguage);
                GUILayout.Space(5);
                DrawBody(previewLanguage);
                GUILayout.Space(5);
            }

            GUILayout.Space(10);
            StructureCreation();
        }

        private void SaveProject()
        {
            AssetDatabase.SaveAssets();
            Debug.LogWarning("Project saved");
        }

        private void StructureCreation()
        {
            EditorGUILayout.HelpBox("Create all the strucutres of this object", MessageType.Info);
            if (GUILayout.Button("Create all my structures"))
            {
                CreateStructures();
                SaveProject();
            }
        }

        private void CreateStructures()
        {
            string bodyKeyword = "Body";
            string titleKeyword = "Title";
            string assetExtension = ".asset";
            string thisPath;
            string targetPath;
            string hostStructureName;
            int lastIndexOfSeparator;

            thisPath = AssetDatabase.GetAssetPath(structure);
            lastIndexOfSeparator = thisPath.LastIndexOf('/');
            targetPath = thisPath.Remove(lastIndexOfSeparator);
            hostStructureName = target.name;

            // create title and body
            SO_langaugeBasedStringContainer titleLBS;
            SO_langaugeBasedStringContainer bodyLBS;

            titleLBS = (CreateLBSStructure(hostStructureName, assetExtension, targetPath, titleKeyword));
            structure.SetTitle(titleLBS);
            bodyLBS = (CreateLBSStructure(hostStructureName, assetExtension, targetPath, bodyKeyword));
            structure.SetBody(bodyLBS);

            //// create the title
            //string titleLBSPath;

            //SO_langaugeBasedStringContainer titleLBSContainer = CreateInstance<SO_langaugeBasedStringContainer>();
            //titleLBSPath = targetPath + "/" + titleLBSContainer.GetFilename() + "_" + hostStructureName + "_" + titleKeyword;

            //AssetDatabase.CreateAsset(titleLBSContainer, titleLBSPath + assetExtension);
            //titleLBSContainer.CreateLanguageBasedStrings("_" + titleKeyword);

            //structure.SetTitle(titleLBSContainer);

            // create the body
            //string bodyLBSPath;

            //SO_langaugeBasedStringContainer bodyLBSContainer = CreateInstance<SO_langaugeBasedStringContainer>();
            //bodyLBSPath = targetPath + "/" + bodyLBSContainer.GetFilename() + "_" + hostStructureName + "_" + bodyKeyword;
            
            //AssetDatabase.CreateAsset(bodyLBSContainer, bodyLBSPath + assetExtension);
            //bodyLBSContainer.CreateLanguageBasedStrings("_" + bodyKeyword);

            //structure.SetBody(bodyLBSContainer);
            
        }

        private SO_langaugeBasedStringContainer CreateLBSStructure(string _hostStructureName, string _assetExtension, string _targetPath, string _objectKeyword)
        {
            string refinedPath;

            SO_langaugeBasedStringContainer lBSContainer = CreateInstance<SO_langaugeBasedStringContainer>();
            refinedPath = _targetPath + "/" + lBSContainer.GetFilename() + "_" + _hostStructureName + "_" + _objectKeyword;

            AssetDatabase.CreateAsset(lBSContainer, refinedPath + _assetExtension);
            lBSContainer.CreateLanguageBasedStrings("_" + _objectKeyword);

            return lBSContainer;
        }

        private void DrawBody(Language previewLanguage)
        {
            string text;
            text = structure.GetSpeachBody(previewLanguage);

            GUILayout.Label("Body: ");
            if (text != null)
            {
                text = GUILayout.TextArea(text);
                structure.SetSpeachBody(previewLanguage, text);         // it allows you to modyfy the text from the editor preview
            }       
            else
            {
                GUILayout.Label(" TEXT IS NULL ");
            }
        }

        private void DrawTitle(Language previewLanguage)
        {
            string text;
            text = structure.GetSpeachTitle(previewLanguage);

            GUILayout.Label("Title: ");
            if (text != null)
            {
                text = GUILayout.TextArea(text);
                structure.SetTitleBody(previewLanguage, text);
            }
            else
            {
                GUILayout.Label(" TEXT IS NULL ");
            }
                
        }
    }
}

