using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Dialogues
{
    public enum DialogTrigger
    {
        PRESENTATION,
        FIRST_BOSS,
    }


    public class LocalDialogController : MonoBehaviour
    {
        [SerializeField] DialogueManager dialogueManager;
        [SerializeField] Dialogs_GameController gameController;

        [SerializeField] private SO_DialogStructure startingDialogStructure;

        [SerializeField] private Dictionary<DialogTrigger, SO_DialogStructure> dialogs;

        private void Start()
        {
            StartDialog(startingDialogStructure);

            dialogs = new Dictionary<DialogTrigger, SO_DialogStructure>();
            dialogs.Add(DialogTrigger.PRESENTATION, startingDialogStructure);
        }

        public void StartDialog(SO_DialogStructure _dialogToInvoke)
        {
            if (_dialogToInvoke)
            {
                FrezeGame();
                dialogueManager.StartDialogue(_dialogToInvoke);
            }
        }

        // Utilizamos una key del diccionario para llamar a una estructura concreta
        public void StartDialog(DialogTrigger _dialogTrigger)
        {
            SO_DialogStructure dialogStructure;

            dialogs.TryGetValue(_dialogTrigger, out dialogStructure);

            StartDialog(dialogStructure);
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

