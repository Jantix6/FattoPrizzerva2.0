using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogues
{
    [CreateAssetMenu(fileName = "UNFreeze game Event", menuName = "Dialog events/UNFreeze Game")]
    public class SO_UnFreezeGame_Event : SO_DialogEvent
    {

        public override void Execute()
        {
            base.Execute();
            dialogsGameController.UnFreezeGame();
        }

    }
}
