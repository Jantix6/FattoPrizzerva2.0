using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Tasks
{
    [RequireComponent(typeof(LocalTasksManager))]
    public abstract class TasKInterface : MonoBehaviour
    {
        protected System.Random rnd;
        private bool isChildOfComplexTask;

        [SerializeField] protected Color parentingReferenceColor;     // color we use to make easier to see with simple task interfaces form part of a complex task interface
        [SerializeField] protected CanvasHosts canvasHostingTask;
        [SerializeField] private TaskStatus currentTaskStatus;
        [SerializeField] private Task targetTask;                               // task we are controling
        public Task TargetTask { get => targetTask; set => targetTask = value; }

        protected virtual void OnValidate()
        {
            if (isChildOfComplexTask == false)
            {
                SetParentingColor(GetRandomColor());
            }
                
        }


        public abstract void InitializeTask();
        public virtual void SetAsChildOfComplexTask(bool _value)
        {
            isChildOfComplexTask = _value;
            if (isChildOfComplexTask == false)
                SetParentingColor(GetRandomColor());
        }
        public virtual void SetTaskAsCompleted(TaskStatus _desiredTaskStatus)
        {
            Debug.LogWarning("Updating task!");
            TargetTask.ForceTaskFinish(_desiredTaskStatus);
        }

        private void FixedUpdate()
        {
            currentTaskStatus = targetTask.GetCurrentTaskState();
        }

        public void SetParentingColor(Color _desiredColor)
        {
            Debug.LogError("set parenting color " + this.name);
            parentingReferenceColor = _desiredColor;
        }
        public bool GetIsChildOfComplexTask()
        {
            return isChildOfComplexTask;
        }

        // Processing
        protected Color GetRandomColor()
        {
            Color color;
            float red, green, blue;

            if (rnd == null)
                rnd = new System.Random();

            red = rnd.Next(100) / 100.00f;
            green = rnd.Next(100) / 100.00f;
            blue = rnd.Next(100) / 100.00f;

            Debug.Log(red + " " + green + " " + blue);

            color = new Color(red, green, blue, 1f);

            return color;
        }
    }
}

