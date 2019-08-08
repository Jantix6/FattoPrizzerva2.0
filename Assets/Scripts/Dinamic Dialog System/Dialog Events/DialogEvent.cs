using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogues
{
    public abstract class DialogEvent : ScriptableObject
    {
        protected DialogEventsManager eventsManager;

        protected DialogEvent()
        {

        }

        public virtual void Initialize(DialogEventsManager _eventsManager)
        {
            eventsManager = _eventsManager;
        }

        public virtual void Execute()
        {


        }



    }
}


