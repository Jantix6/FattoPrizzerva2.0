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
        int currentIndex;

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

        // we do use a corrutine because the object will not be destroyed until the corrutine ends 
        // if i used a simple method called repeatedlty it couold be destroyed before doing all the desired cycles
        public IEnumerator UpdateCorrutine()
        {
            while (currentIndex != listOfObjectEvents.Count - 1)
            {
                Debug.LogError("Inside the corrutine")

                float deltatime = Time.deltaTime;
                currentEvent = listOfObjectEvents[currentIndex];

                if (currentEvent)
                {
                    DialogType dialogType;
                    dialogType = currentEvent.GetDialogExcecutionType();

                    switch (dialogType)
                    {
                        case DialogType.UPDATE_DEPENDANT:

                            bool isFinished;
                            isFinished = currentEvent.Tick(deltatime);

                            if (isFinished)
                            {
                                if (currentIndex + 1 < listOfObjectEvents.Count)
                                {
                                    //Debug.LogWarning("Corrutine " + currentEvent.ToString() + " " + dialogType + " is now finished");
                                    currentIndex++;
                                }
                                else
                                    yield return null;
                            }

                            break;
                        case DialogType.INSTANT:
                            yield return null;

                            break;
                        default:
                            yield return null;

                            break;
                    }
                }

                

            }

            yield return null;


        }


    }
}


