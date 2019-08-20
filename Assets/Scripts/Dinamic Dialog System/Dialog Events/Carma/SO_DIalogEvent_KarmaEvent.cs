using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogues
{
    // mother event class fot the carma related events
    [CreateAssetMenu(fileName = "Create Karma Event", menuName = "Dialog events/Carma/Modify Karma")]
    public class SO_DIalogEvent_KarmaEvent : SO_DialogEvent
    {
        [SerializeField] protected int karmaDelta;
        [SerializeField] protected DC_Karma karmaObject;

        private void OnEnable()
        {
            if (karmaObject == null)
                Debug.LogError("Set the KarmaObject for karmaObject");
        }

        public override void Initialize(Dialogs_GameController _dialogGameController)
        {
            base.Initialize(_dialogGameController);
        }

        public override void Execute()
        {
            base.Execute();
            karmaObject.ModifyLocalKarma(+karmaDelta);
        }

    }
}
