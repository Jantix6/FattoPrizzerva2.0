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

        [SerializeField] private List<SO_DialogStructure> qAStructures;
        [SerializeField] private int currentStructureIndex;

        private CanvasedDialogElement activeDialogueElement;

        [Header("Debug")]
        [SerializeField] private KeyCode previousStrcture_Key = KeyCode.F1;
        [SerializeField] private KeyCode nextStrcutre_Key = KeyCode.F2;



        #region (Internal)
        private void StartDialogue(int _index)
        {
            ShowDialogueStructure(_index);
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
            if (qAStructures[_index] is SO_QuestionAnswerStructure)
            {
                activeDialogueElement = Instantiate(canvasedDialoguePrefab, dialogueCanvas.transform);
                (activeDialogueElement as CanvasedDialogue).Initialize(qAStructures[_index] as SO_QuestionAnswerStructure, this, LanguageManager.gameLanguage);
            }
            // Is it a speach element?
            else if (qAStructures[_index] is SO_SpeachStructure)
            {
                activeDialogueElement = Instantiate(canvasedSpeachPrefab,dialogueCanvas.transform);
                (activeDialogueElement as CanvasedSpeach).Initialize(qAStructures[_index] as SO_SpeachStructure,this, LanguageManager.gameLanguage);
            }
            currentStructureIndex = _index;
            // ---------------------------------------------------------------------------------------------------------------------------- //   
        }
        public void ShowDialogueStructure(SO_QuestionAnswerStructure _targetStructure)
        {
            if (qAStructures.Contains(_targetStructure) )
            {
                int _objectIndex = qAStructures.IndexOf(_targetStructure);
                ShowDialogueStructure(_objectIndex);
            } else
            {
                Debug.Log("The target Structure is not set on the managers list");
            }


        }

        public void NextStructure()
        {
            if (currentStructureIndex + 1 <= qAStructures.Count - 1)
            {
                Debug.Log("Proceed to the next structure");
                ShowDialogueStructure(currentStructureIndex + 1);
            } else
            {
                Debug.LogError("Unable to go to next structure, you are the last");
                EndDialogue();
            }
        }
        public void PreviousStructure()
        {
            if (currentStructureIndex - 1 >= 0)
            {
                Debug.Log("Proceed to the previous structure");
                ShowDialogueStructure(currentStructureIndex - 1);
            }
            else
            {
                Debug.LogError("Unable to go to previous structure, you are the first");
                EndDialogue();
            }
        }

        #endregion


        #region (Engine)


        private void Awake()
        {
            // El idioma no se seteara desde aqui aunque lo hago asi para hacer pruebas
            LanguageManager.gameLanguage = Language.ENGLISH;

            if (qAStructures is null || qAStructures.Count == 0)
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
                NextStructure();
            }
            if (Input.GetKeyDown(previousStrcture_Key))
            {
                PreviousStructure();
            }
        }
#endregion;
    }
}


