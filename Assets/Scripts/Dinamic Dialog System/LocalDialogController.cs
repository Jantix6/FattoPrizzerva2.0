using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Dialogues
{

    public class LocalDialogController : MonoBehaviour
    {
        [SerializeField] DialogueManager dialogueManager;
        [SerializeField] Dialogs_GameController gameController;

        [SerializeField] private SO_DialogConfig dialogsConfig;


        private void Start()
        {
            if (!dialogsConfig)
                throw new MissingReferenceException("Dialogs config not set");


            // for testing porposes
            StartDialog(DialogTrigger.PRESENTATION);
        }

        // Utilizamos una "key"  para llamar a una estructura concreta
        public void StartDialog(DialogTrigger _dialogTrigger)
        {
            SO_DialogStructure dialogStructure;
            dialogStructure = dialogsConfig.GetDialogStructure(_dialogTrigger);

            if (dialogStructure)
            {
                FrezeGame();
                dialogueManager.StartDialogue(dialogStructure);
            }          
        }

        private void FrezeGame()
        {
            if (gameController)
                gameController.FreezeGame();
            else
                throw new MissingReferenceException(gameController.ToString());
        }

        public void UnFrezeGame()
        {
            if (gameController)
                gameController.UnFreezeGame();
            else
                throw new MissingReferenceException(gameController.ToString());
        }


    }
}

