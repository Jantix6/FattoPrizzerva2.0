using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogues
{
    public enum DialogEventStatus
    {
        ON_HOLD,
        IN_PROGRESS,
        PERFORMED,
        NONE,       // used by the events that are instantaneous
    }

    public abstract class SO_DialogEvent : ScriptableObject
    {
        protected Dialogs_GameController dialogsGameController;
        protected DialogEventStatus eventStatus = DialogEventStatus.NONE;

        protected bool isInstantEvent;                                          // does de event play just once (no update)
        bool isEventFinished;

        private void OnEnable()
        {
            isEventFinished = false;
        }

        public virtual void Initialize(Dialogs_GameController _dialogGameController)
        {
            dialogsGameController = _dialogGameController;
                 
        }

        // what the button calls ONCE
        public virtual void Execute()
        {
            // override
            Debug.Log("Calling excecute of " + this.name);

        }
        
    }
}


