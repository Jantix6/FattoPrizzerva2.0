using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Dialogues
{
    public class LocalDialogController : MonoBehaviour
    {
        DialogueManager dialogueManager;
        DialogEventsManager dialogueEventsManager;



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
                dialogueEventsManager.UnFrezeGame();
            else
                throw new MissingReferenceException(dialogueEventsManager.ToString());
        }


    }
}

