using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Dialogues
{
    public class DialogueManager : MonoBehaviour
    {
        [SerializeField] private Dialogs_GameController gameController;

        [SerializeField] private Canvas dialogueCanvas;
        [SerializeField] private EventSystem dialogueNavitagionEventSystem;           
        [SerializeField] private CanvasedDialogue canvasedDialoguePrefab;
        [SerializeField] private CanvasedSpeach canvasedSpeachPrefab;

        [SerializeField] private List<SO_DialogStructure> dialogStructures;
        [SerializeField] private int currentStructureIndex;

        private CanvasedDialogElement activeDialogueElement;

        [Header("Debug")]
        //[SerializeField] private KeyCode previousStrcture_Key = KeyCode.F1;
        //[SerializeField] private KeyCode nextStrcutre_Key = KeyCode.F2;
        public Language selectedLanguage = Language.CATALAN;

        // Contrlol
        [SerializeField] public bool IsEnabled { get; private set; }            // are we on a dialogue? 


        #region (Internal)
        private void StartDialogue(int _index)
        {
            ShowDialogueStructure(_index);
            IsEnabled = true;
        }
        public void StartDialogue(SO_DialogStructure _dialogStructure)
        {
            if (dialogStructures.Contains(_dialogStructure))
            {
                // busca el indice del objeto donde a (parametro) es igual al objeto que estamos buscando 
                // iteramos por los elementos de la lista y devolvemos el indice de aquel qu ecumple la equivalencia 
                // it returns -1 if not found
                int index = dialogStructures.FindIndex(a => a == _dialogStructure);
                 
                if (index != -1)
                {
                    ShowDialogueStructure(index);
                    gameController.FreezeGame();
                }
                else
                    Debug.LogError("The dialog you want to make active was not found on the dialogs list");

            }
        }

        public void EndDialogue()
        {
            if (activeDialogueElement != null)
            {
                DestroyDialogueElement(activeDialogueElement);

                // we do set the current index to -1 to make the dialog restart after completing it (just for testing porposes)
                currentStructureIndex = -1;

                IsEnabled = false;
                gameController.UnFreezeGame();
            }
        }
        private void DestroyDialogueElement (CanvasedDialogElement _target)
        {
            Destroy(_target.gameObject);
        }
        private void ShowDialogueStructure(int _index)
        {

            currentStructureIndex = _index;

            // destroy the previous shown element
            if (activeDialogueElement != null)
                DestroyDialogueElement(activeDialogueElement);

            // DANI REFACTORIZA ESTO, NO NOS GUSTAN LOS IFs ----------------------------------------------------------------------------- //
            // Is it a question?
            if (dialogStructures[_index] is SO_QuestionAnswerStructure)
            {
                activeDialogueElement = Instantiate(canvasedDialoguePrefab, dialogueCanvas.transform);
                (activeDialogueElement as CanvasedDialogue).Initialize(dialogStructures[_index] as SO_QuestionAnswerStructure, this, gameController, LanguageManager.GetGameLanguage());               
            }
            // Is it a speach element?
            else if (dialogStructures[_index] is SO_SpeachStructure)
            {
                activeDialogueElement = Instantiate(canvasedSpeachPrefab,dialogueCanvas.transform);
                (activeDialogueElement as CanvasedSpeach).Initialize(dialogStructures[_index] as SO_SpeachStructure,this, gameController, LanguageManager.GetGameLanguage());             
            }
            // ---------------------------------------------------------------------------------------------------------------------------- //   

            // setup the navigation using the keyboard (default axis)
            dialogueNavitagionEventSystem.firstSelectedGameObject = null;
            (activeDialogueElement as I_DialogElement).InitializeDefaultKeyboardNavigation(dialogueNavitagionEventSystem);

            Debug.LogWarning("showing as active the object " + dialogueNavitagionEventSystem.firstSelectedGameObject.GetType());
        }
        public void ShowDialogueStructure(SO_DialogStructure _targetStructure)
        {
            if (dialogStructures.Contains(_targetStructure) )
            {
                int _objectIndex = dialogStructures.IndexOf(_targetStructure);
                ShowDialogueStructure(_objectIndex);
            } else
            {
                Debug.LogWarning("The target Structure is not set on the managers list" + _targetStructure);
            }

        }

        /*
        public void GoToNextStructure()
        {
            int desiredIndex;
            desiredIndex = currentStructureIndex + 1;

            if (desiredIndex < dialogStructures.Count  )
            {
                Debug.Log("Proceed to the next structure");
                ShowDialogueStructure(desiredIndex);
            } else
            {
                Debug.LogWarning("Unable to go to next structure, you are the last (current : "  + currentStructureIndex + ")");
                EndDialogue();
            }
        }
        public void GoToPreviousStructure()
        {
            int desiredIndex;
            desiredIndex = currentStructureIndex - 1;

            if (desiredIndex >= 0)
            {
                Debug.Log("Proceed to the previous structure");
                ShowDialogueStructure(desiredIndex);
            }
            else
            {
                Debug.LogWarning("Unable to go to previous structure, you are the first (current : " + currentStructureIndex + ")");
                EndDialogue();
            }
        }
        */
        #endregion


        #region (Engine)


        private void Awake()
        {
            if (gameController == null)
                Debug.LogError("Gamecontroller is not set");
            if (dialogueCanvas == null)
                Debug.LogError("Dialoguecanvas is not set");
            if (dialogueNavitagionEventSystem == null)
                Debug.LogError("dialogueNavitagionEventSystem is not set");
            if (canvasedDialoguePrefab == null)
                Debug.LogError("canvasedDialoguePrefab is not set");
            if (canvasedSpeachPrefab == null)
                Debug.LogError("canvasedSpeachPrefab");

            

            if (dialogStructures is null || dialogStructures.Count == 0)
                Debug.LogError("ERROR: dialogStructures is not set or it is empty");
            currentStructureIndex = 0;
        }

        private void Start()
        {
            DebugSetLenguage();
        }


        [ContextMenu("SetLanguage")]
        private void DebugSetLenguage()
        {
            // El idioma no se seteara desde aqui aunque lo hago asi para hacer pruebas
            LanguageManager.SetGameLanguage(selectedLanguage);
        }
        #endregion;
    }
}


