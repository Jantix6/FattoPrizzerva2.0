using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Dialogues
{
    [CreateAssetMenu(fileName = "Test Event", menuName = "Dialog events/Test timed event")]
    public class SO_TestEvent : SO_DialogEvent
    {
        float timeForCompletion;
        float currentTime;


        private void OnEnable()
        {
            dialogExcecutionType = DialogType.UPDATE_DEPENDANT;
            timeForCompletion = 10;
            currentTime = 0;
        }

        public override void Initialize(Dialogs_GameController _dialogGameController)
        {
            base.Initialize(_dialogGameController);
        }

        public override void Execute()
        {
            base.Execute();
            Debug.Log(currentTime);
        }

        public override bool Tick(float deltaTime)
        {
            Debug.Log("Calling udpate event with " + currentTime + " / " + timeForCompletion);

            currentTime += deltaTime;
            if (currentTime >= timeForCompletion)
            {
                return true;

            } else
            {
                return false;
            }

        }

    }
}

