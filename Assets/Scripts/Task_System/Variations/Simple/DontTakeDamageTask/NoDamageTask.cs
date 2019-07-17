using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tasks
{
    [CreateAssetMenu(fileName = "NoDamageTask", menuName = "Tasks/NoDamageTask")]
    public class NoDamageTask : SimpleTask
    {
        bool damageTaken;

        protected override void OnEnable()
        {
            base.OnEnable();
            damageTaken = false;
            TaskInterface_Type = typeof(NoDamageTaskInterface);
        }


        public override TaskStatus Check()
        {
            base.Check();

            if (damageTaken)
                CurrentTaskState = TaskStatus.FAILED;
            else if (damageTaken == false)
                CurrentTaskState = TaskStatus.IN_PROGRESS;

            PreviousState = CurrentTaskState;

            return CurrentTaskState;

        }

    }
}

