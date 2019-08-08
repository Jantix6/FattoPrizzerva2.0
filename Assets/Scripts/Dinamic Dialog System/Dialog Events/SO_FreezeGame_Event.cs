using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogues
{
    [CreateAssetMenu(fileName = "Freeze game Event", menuName = "Dialog events/Freeze Game")]
    public class SO_FreezeGame_Event : DialogEvent
    {
        public override void Execute()
        {
            base.Execute();
            eventsManager.FrezeGame();
        }

    }
}
