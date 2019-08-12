using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Dialogues
{
    [CreateAssetMenu(fileName = "Test Event", menuName = "Dialog events/Test timed event")]
    public class SO_TestEvent : SO_DialogEvent
    {

        public override void Initialize(Dialogs_GameController _dialogGameController)
        {
            base.Initialize(_dialogGameController);
        }

        public override void Execute()
        {
            base.Execute();
        }


    }
}

