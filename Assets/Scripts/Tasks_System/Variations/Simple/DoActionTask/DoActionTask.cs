using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

namespace Tasks
{
    [CreateAssetMenu(fileName = "DoActionTask", menuName = "Tasks/DoActionTask")]
    public class DoActionTask : SimpleTask
    {
        private bool actionPerformed;

        protected override void OnEnable()
        {
            base.OnEnable();
            actionPerformed = false;
            // falta la interficie que utilizara
        }

        public override TaskStatus Check()
        {
            base.Check();

            if (actionPerformed)
                //CurrentTaskState = TaskStatus.ACHIEVED;
            SetTaskState(TaskStatus.ACHIEVED);

            else
                //CurrentTaskState = TaskStatus.IN_PROGRESS;
            SetTaskState(TaskStatus.IN_PROGRESS);


            //PreviousState = CurrentTaskState;

            return CurrentTaskState;
        }

        protected virtual void OnActionPerformed()
        {
            actionPerformed = true; 
        }
    }

    // -------------------------------------------------------------------------- //



}

