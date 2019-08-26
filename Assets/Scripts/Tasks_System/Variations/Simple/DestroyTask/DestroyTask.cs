using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Tasks
{
    /// <summary>
    /// Task used to check if you destroyed x OBJECT 
    /// </summary>
    [CreateAssetMenu(fileName = "Destroy_Task", menuName = "Tasks/Destroy_Entity")]
    public class DestroyTask : SimpleTask , I_RequiereInitialization
    {
        private GameObject objectToDestroy;

        //[SerializeField] private UnityEvent OnDestruction;          // event called once the object is destroyed (add to the event yor kill or destroy method)
        private bool isObjectDestroyed;
       

        protected override void OnEnable()
        {
            base.OnEnable();
            isObjectDestroyed = false;
            //OnDestruction.AddListener(SetObjectAsDestroyed);        // when the event is called it makes the variable true and then the task is marked as completed
            TaskInterface_Type = typeof(DestroyTaskInterface);

        }

        public override TaskStatus Check()
        {
            if (objectToDestroy == null)
            {
                isObjectDestroyed = true;
            }

            if (isObjectDestroyed)
                //CurrentTaskState = TaskStatus.ACHIEVED;
                SetTaskState(TaskStatus.ACHIEVED);
            else
                //CurrentTaskState = TaskStatus.IN_PROGRESS;
                SetTaskState(TaskStatus.IN_PROGRESS);

            //PreviousState = CurrentTaskState;

            return CurrentTaskState;

        }

        //private void SetObjectAsDestroyed ()
        //{
        //    isObjectDestroyed = true;
        //}

        //public override void ForceTaskFinish(TaskStatus _desiredEndStatus)
        //{
        //    // base.ForceTaskFinish(_desiredEndStatus);
        //    isObjectDestroyed = true;
        //}

        public void SetTaskData(TaskData _dataPackage)
        {
            objectToDestroy = (_dataPackage as DestroyTaskData).GetEntityToDestroy();
        }
    }

     
}


