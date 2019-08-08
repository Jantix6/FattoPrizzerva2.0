using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Dialogues
{
    public class LocalDialogController : MonoBehaviour
    {
        [SerializeField] DialogueManager dialogueManager;
        [SerializeField] DialogEventsManager dialogueEventsManager;

        [SerializeField] private SO_DialogStructure startingDialogStructure;

        private void Start()
        {
            StartDialog(startingDialogStructure);
        }

        public void StartDialog(SO_DialogStructure _dialogToInvoke)
        {
            if (_dialogToInvoke)
            {
                FrezeGame();
                dialogueManager.StartDialogue(_dialogToInvoke);
            }
        }

        private void FrezeGame()
        {
            if (dialogueEventsManager)
                dialogueEventsManager.FrezeGame();
            else
                throw new MissingReferenceException(dialogueEventsManager.ToString());
        }

        public void UnFrezeGame()
        {
            if (dialogueEventsManager)
                dialogueEventsManager.UnFreezeGame();
            else
                throw new MissingReferenceException(dialogueEventsManager.ToString());
        }


    }
}

