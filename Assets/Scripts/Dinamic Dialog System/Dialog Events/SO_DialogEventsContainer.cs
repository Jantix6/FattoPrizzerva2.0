using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogues
{
    /// <summary>
    ///  due to the possiblity of having more than one event happening one after the other and also having time between
    ///  their action I though we would need some kind of object to manage those events and act as the "trafic light" 
    ///  that switch them on or off
    /// </summary>

    [CreateAssetMenu(fileName = "Dialog events Container", menuName = "Dialog events/[CONTAINER]")]
    public class SO_DialogEventsContainer : ScriptableObject
    {
        public List<SO_DialogEvent> listOfObjectEvents;
        SO_DialogEvent currentEvent;


        public void Initialize(Dialogs_GameController _gameController)
        {
            if (listOfObjectEvents != null)
            {
                foreach (SO_DialogEvent dialogEvent in listOfObjectEvents)
                {
                    dialogEvent.Initialize(_gameController);
                }
            }
        }

        public void Excecute()
        {
            foreach (SO_DialogEvent dialogEvent in listOfObjectEvents)
            {
                dialogEvent.Execute();       
            }
        }




    }
}


