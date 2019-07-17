using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tasks
{
    public class DoBeforeTimeInterface : TasKInterface
    {
        private void OnValidate()
        {
            if (!(TargetTask is DoSomethingOnTargetTimeTask) && TargetTask != null)
            {
                Debug.LogError("The assigned task is not from the expected type " + typeof(DoSomethingOnTargetTimeTask));
                TargetTask = null;
            }
        }

        public override void InitializeTask()
        {
            TasksManager.Instance.SetupTask(TargetTask);
        }

    }
}


