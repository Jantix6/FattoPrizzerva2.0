using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogues
{
    public class DialogueManager : MonoBehaviour
    {

        [SerializeField] private Canvas dialogueCanvas;
        [SerializeField] private CanvasedDialogue canvasedDialoguePrefab;
        [SerializeField] private CanvasedSpeach canvasedSpeachPrefab;

        [SerializeField] private List<SO_DialogStructure> dialogStructures;
        [SerializeField] private int currentStructureIndex;

        private CanvasedDialogElement activeDialogueElement;

        [Header("Debug")]
        [SerializeField] private KeyCode previousStrcture_Key = KeyCode.F1;
        [SerializeField] private KeyCode nextStrcutre_Key = KeyCode.F2;
        public Language selectedLanguage = Language.CATALAN;



        #region (Internal)
        private void StartDialogue(int _index)
        {
            ShowDialogueStructure(_index);
        }
        private void StartDialogue(SO_DialogStructure _dialogStructure)
        {
            if (dialogStructures.Contains(_dialogStructure))
            {
                // busca el indice del objeto donde a (parametro) es igual al objeto que estamos buscando 
                // iteramos por los elementos de la lista y devolvemos el indice de aquel qu ecumple la equivalencia 
                int index = dialogStructures.FindIndex(a => a == _dialogStructure);
                 
                if (index != -1)
                    ShowDialogueStructure(index);
                else
                    Debug.LogError("The dialog you want to make active was not found on the dialogs list");

            }
        }

        private void EndDialogue()
        {
            if (activeDialogueElement != null)
                DestroyDialogueElement(activeDialogueElement);
        }
        private void DestroyDialogueElement (CanvasedDialogElement _target)
        {
            Destroy(_target.gameObject);
        }
        private void ShowDialogueStructure(int _index)
        {
            // destroy the previous shown element
            if (activeDialogueElement != null)
            {
                DestroyDialogueElement(activeDialogueElement);
            }

            // DANI REFACTORIZA ESTO, NO NOS GUSTAN LOS IFs ----------------------------------------------------------------------------- //
            // Is it a question?
            if (dialogStructures[_index] is SO_QuestionAnswerStructure)
            {
                activeDialogueElement = Instantiate(canvasedDialoguePrefab, dialogueCanvas.transform);
                (activeDialogueElement as CanvasedDialogue).Initialize(dialogStructures[_index] as SO_QuestionAnswerStructure, this, LanguageManager.gameLanguage);
            }
            // Is it a speach element?
            else if (dialogStructures[_index] is SO_SpeachStructure)
            {
                activeDialogueElement = Instantiate(canvasedSpeachPrefab,dialogueCanvas.transform);
                (activeDialogueElement as CanvasedSpeach).Initialize(dialogStructures[_index] as SO_SpeachStructure,this, LanguageManager.gameLanguage);
            }
            currentStructureIndex = _index;
            // ---------------------------------------------------------------------------------------------------------------------------- //   
        }
        public void ShowDialogueStructure(SO_DialogStructure _targetStructure)
        {
            Debug.LogError(_targetStructure);

            if (dialogStructures.Contains(_targetStructure) )
            {
                int _objectIndex = dialogStructures.IndexOf(_targetStructure);
                ShowDialogueStructure(_objectIndex);
            } else
            {
                Debug.Log("The target Structure is not set on the managers list" + _targetStructure);
            }

        }

        public void GoToNextStructure()
        {
            if (currentStructureIndex + 1 <= dialogStructures.Count - 1)
            {
                Debug.Log("Proceed to the next structure");
                ShowDialogueStructure(currentStructureIndex + 1);
            } else
            {
                Debug.LogWarning("Unable to go to next structure, you are the last");
                EndDialogue();
            }
        }
        public void GoToPreviousStructure()
        {
            if (currentStructureIndex - 1 >= 0)
            {
                Debug.Log("Proceed to the previous structure");
                ShowDialogueStructure(currentStructureIndex - 1);
            }
            else
            {
                Debug.LogWarning("Unable to go to previous structure, you are the first");
                EndDialogue();
            }
        }

        #endregion


        #region (Engine)


        private void Awake()
        {
            DebugSetLenguage();

            if (dialogStructures is null || dialogStructures.Count == 0)
                Debug.LogError("QAStructures ERROR");
            currentStructureIndex = 0;
        }

        private void Start()
        {
            StartDialogue(0);
        }
        private void Update()
        {
            if (Input.GetKeyDown(nextStrcutre_Key))
            {
                GoToNextStructure();
            }
            if (Input.GetKeyDown(previousStrcture_Key))
            {
                GoToPreviousStructure();
            }
        }

        private void OnValidate()
        {
            DebugSetLenguage();
        }

        private void DebugSetLenguage()
        {
            // El idioma no se seteara desde aqui aunque lo hago asi para hacer pruebas
            LanguageManager.gameLanguage = selectedLanguage;
            Debug.Log("lenguage set to " + selectedLanguage);
        }
        #endregion;
    }
}


