using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogues
{
    public class SayBye_Event : GameEvent
    {
        string textToShow;

        public override void Initialize()
        {
            base.Initialize();
            textToShow = "Bye";
        }

        public override void Excecute()
        {
            base.Excecute();
            Debug.Log(textToShow);
        }


    }

}
