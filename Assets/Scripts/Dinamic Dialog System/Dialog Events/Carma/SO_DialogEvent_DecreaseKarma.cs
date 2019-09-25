using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogues
{
    // [CreateAssetMenu(fileName = "Carma Decrease Event", menuName = "Dialog events/Carma/Decrease Carma")]
    public class SO_DialogEvent_DecreaseKarma : SO_DIalogEvent_KarmaEvent
    {
        public override void Initialize(Dialogs_GameController _dialogGameController)
        {
            base.Initialize(_dialogGameController);
        }

        public override void Execute()
        {
            base.Execute();

            // Decrease the Karma of the player by carmaDelta
            karmaObject.ModifyLocalKarma(-(Mathf.Abs( karmaDelta)));
        }

    }
}
