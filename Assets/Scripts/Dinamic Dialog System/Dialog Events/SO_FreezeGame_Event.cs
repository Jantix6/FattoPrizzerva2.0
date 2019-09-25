using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogues
{
    [CreateAssetMenu(fileName = "Freeze game Event", menuName = "Dialog events/Freeze Game")]
    public class SO_FreezeGame_Event : SO_DialogEvent
    {
        public override void Execute()
        {
            base.Execute();
            dialogsGameController.FreezeGame();

            // Seria interesante que la funcion freeze game no estuviera en el events manager sino mejor en el game controller
            // al ser algo parecido a un pausa que puede usarse para mas cosas a parte de dialogos
        }

    }
}
