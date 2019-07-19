using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogues
{
    public abstract class CanvasedDialogElement : MonoBehaviour
    {
        [Header("Global refernces")]
        protected DialogueManager dialogueManager;
        [SerializeField] protected CanvasGroup canvasGroup;


        public void EnableVisibility()
        {
            canvasGroup.alpha = 1f;
        }
        public void DisableVisibilty()
        {
            canvasGroup.alpha = 0f;
        }
    }
}


