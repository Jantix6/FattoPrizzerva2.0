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
        NONE,       
    }
    public enum DialogType
    {
        UPDATE_DEPENDANT,
        INSTANT,
    }

    public abstract class SO_DialogEvent : ScriptableObject
    {
        protected Dialogs_GameController dialogsGameController;
        protected DialogEventStatus eventStatus = DialogEventStatus.NONE;
        protected DialogType dialogExcecutionType = DialogType.INSTANT;


        public DialogType GetDialogExcecutionType()
        {
            return dialogExcecutionType;
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
        
        public virtual bool Tick(float deltaTime)
        {
            // override
            return true;
        }

        
    }
}


