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

        [SerializeField] private List<QuestionAnswerStructure> qAStructures;
        [SerializeField] private int currentStructureIndex;

        [SerializeField] private CanvasedDialogue activeDialogueStructure;

        [Header("Debug")]
        [SerializeField] private KeyCode previousStrcture_Key = KeyCode.F1;
        [SerializeField] private KeyCode nextStrcutre_Key = KeyCode.F2;

        private void Awake()
        {
            if (qAStructures is null || qAStructures.Count == 0)
                Debug.LogError("QAStructures ERROR");
            currentStructureIndex = 0;
        }



        #region (Internal)
        private void StartDialogue(int _index)
        {
            ShowDialogueStructure(_index);
        }
        private void EndDialogue()
        {
            if (activeDialogueStructure != null)
                DestroyDialogueElement(activeDialogueStructure);
        }
        private void DestroyDialogueElement (CanvasedDialogue _target)
        {
            Destroy(_target.gameObject);
        }
        private void ShowDialogueStructure(int _index)
        {
            if (activeDialogueStructure != null)
            {
                DestroyDialogueElement(activeDialogueStructure);
            }

            Debug.Log("Instantiating" + qAStructures[_index].GetQuestion());

            activeDialogueStructure = Instantiate(canvasedDialoguePrefab, dialogueCanvas.transform);
            activeDialogueStructure.Initialize(qAStructures[_index],this);
            currentStructureIndex = _index;
        }
        public void ShowDialogueStructure(QuestionAnswerStructure _targetStructure)
        {
            if (qAStructures.Contains(_targetStructure) )
            {
                int _objectIndex = qAStructures.IndexOf(_targetStructure);
                ShowDialogueStructure(_objectIndex);
            } else
            {
                Debug.LogError("The target Structure is not set on the managers list");
            }


        }

        private void NextStructure()
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
        private void PreviousStructure()
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

    }
}


