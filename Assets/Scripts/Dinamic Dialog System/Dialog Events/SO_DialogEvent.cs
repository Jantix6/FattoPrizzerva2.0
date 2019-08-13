using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogues
{

    public abstract class SO_DialogEvent : ScriptableObject
    {
        protected Dialogs_GameController dialogsGameController;

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


