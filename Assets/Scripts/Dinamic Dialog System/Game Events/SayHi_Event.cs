using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogues
{
    public class SayHi_Event : GameEvent
    {
        string textToShow;


        public override void Initialize()
        {
            base.Initialize();
            textToShow = "hi";
        }

        public override void Excecute()
        {
            base.Excecute();
            Debug.Log(textToShow);
        }


    }

}
