using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tasks
{
    public class NoDamageTaskInterface : TasKInterface
    {
        [SerializeField] private bool damageTaken;

        public override void InitializeTask()
        {
            TasksManager.Instance.SetupTask(TargetTask);
        }

        private void OnValidate()
        {
            if (!(TargetTask is NoDamageTask) && TargetTask != null)
            {
                Debug.LogError("The assigned task is not from the expected type " + typeof(DestroyTask));
                TargetTask = null;
            }
        }
    }

}

