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

        private SO_Answer answer;
        [SerializeField] private bool isEventTikingEnabled = false;

        public void Initialize(SO_Answer _answer, DialogueManager _dialogueManager, Language _targetLanguage)
        {
            answer = _answer;

            answerText.text = answer.GetAnswerBody(_targetLanguage);
            dialogueManager = _dialogueManager;
            nextDialogStructure = answer.GetTargetStructure();

            // button click listeners
            UnityAction dEvent = answer.GetEventActionToPerform();
            if (dEvent != null)
                answerButton.onClick.AddListener(dEvent);

            UnityAction dEvent2 = answer.GetEventActionsToPerform();
            if (dEvent2 != null)
                answerButton.onClick.AddListener(dEvent2);

            answerButton.onClick.AddListener(ShowStructure);
        }

        /*
        // we do use the update to update the status of the events being played on the event container
        private void Update()
        {
            // Because I'm destroying the object i'm also destroying the update of this script so it is only clled once
            // corrutines time or not destroy but hide
            if (isEventTikingEnabled)
            {
                answer.TickEvents(Time.deltaTime);
                Debug.LogError("Calling update");
            }
        }
        */

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


