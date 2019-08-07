using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Dialogues
{
    public class CanvasedAnswer : MonoBehaviour 
    {
        [SerializeField] protected DialogueManager dialogueManager;
        [SerializeField] private TMP_Text answerText;
        [SerializeField] private SO_DialogStructure nextDialogStructure;
        [SerializeField] private Button answerButton;


        public void Initialize(SO_Answer _answer, DialogueManager _dialogueManager, Language _targetLanguage)
        {
            answerText.text = _answer.GetAnswerBody(_targetLanguage);
            dialogueManager = _dialogueManager;
            nextDialogStructure = _answer.GetTargetStructure();

            // button click listeners
            answerButton.onClick.AddListener(_answer.GetEventActionToPerform());
            answerButton.onClick.AddListener(ShowStructure);
        }


        private void ShowStructure()
        {
            if (nextDialogStructure)
                dialogueManager.ShowDialogueStructure(nextDialogStructure);
            else
            {
                Debug.LogWarning("The target Structure is not set , EXITING DIALOG");
                dialogueManager.EndDialogue();
            }
        }
    }
}


