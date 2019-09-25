using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogues
{
    //[CreateAssetMenu(fileName = "Carma Increase Event", menuName = "Dialog events/Carma/IncreaseCarma")]
    public class SO_DialogEvent_IncreaseKarma : SO_DIalogEvent_KarmaEvent
    {
        public override void Initialize(Dialogs_GameController _dialogGameController)
        {
            base.Initialize(_dialogGameController);
        }

        public override void Execute()
        {
            base.Execute();

            // Increate the carma of the player by carmaDelta
            karmaObject.ModifyLocalKarma(+(Mathf.Abs( karmaDelta)));
        }

    }


}


