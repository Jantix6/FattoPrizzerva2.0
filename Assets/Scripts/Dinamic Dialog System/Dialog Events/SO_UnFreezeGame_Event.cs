using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogues
{
    [CreateAssetMenu(fileName = "UNFreeze game Event", menuName = "Dialog events/UNFreeze Game")]
    public class SO_UnFreezeGame_Event : DialogEvent
    {
        public override void Execute()
        {
            base.Execute();
            eventsManager.UnFreezeGame();
        }

    }
}
