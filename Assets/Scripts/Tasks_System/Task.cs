using System;
using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Collections;

namespace Tasks
{
    public enum TaskStatus
    {
        NONE = -3,                  // ERROR AND INITIAL STATE
        ACHIEVED = 1,               // resultado positivo
        IN_PROGRESS = 0,            // en progreso (sigue haciendo el check)
        FAILED = -1                 // resultado negativo (fallaste wey)
    }

    public abstract class Task : ScriptableObject
    {
        protected TasksBlackboard blackboard;
        private Type taskInterface_Type;                    // interface the task uses in order to comunicate with
        System.Random rnd;

        [SerializeField] protected string taskName = "NONE";
        [SerializeField] [TextArea] private string taskDescription = "NONE";

        [Header("Identification")]
        [SerializeField] private int taskId;
        private static List<int> tasksIdOnUse;              // ID EN USO
        protected bool isChildOfTask;                       // is this part of a complex task?

        [Header("Events")]
        [SerializeField] private List<Reward> rewards_Lst;          // do no use this method, it is better to use a manager or something to be called and then manage the action

        //[SerializeField] protected UnityEvent OnAchivement;
        //[SerializeField] protected UnityEvent OnRun;
        //[SerializeField] protected UnityEvent OnFail;

        // Events
        // ------------------------------------------------------------------------------// On Status change
        [SerializeField] protected delegate void StatusChangeEventManager();
        [SerializeField] protected event StatusChangeEventManager OnStatusChange;
        
        // ------------------------------------------------------------------------------// On Achive Status
        [SerializeField] protected delegate void AchivementDelegateManager();
        [SerializeField] protected event AchivementDelegateManager OnAchiveEvent;
        // ------------------------------------------------------------------------------// In progress
        [SerializeField] protected delegate void InProgressDelegateManager();
        [SerializeField] protected event InProgressDelegateManager InProgressEvent;
        // ------------------------------------------------------------------------------// On fail
        [SerializeField] protected delegate void FailDelegateManager();
        [SerializeField] protected event FailDelegateManager OnFailEvent;



        private TaskStatus currentTaskState;
        protected TaskStatus CurrentTaskState { get => currentTaskState; private set => currentTaskState = value; }
        private TaskStatus previousState;
        protected TaskStatus PreviousState { get => previousState; private set => previousState = value; }
        public Type TaskInterface_Type { get => taskInterface_Type; protected set => taskInterface_Type = value; }

        private void SetRandomId()
        {
            if (tasksIdOnUse == null)
            {
                tasksIdOnUse = new List<int>();
            }

            // dani mejora esto
            taskId = tasksIdOnUse.Count + 1;

            //Debug.LogError(taskId);
            tasksIdOnUse.Add(taskId);
        }
        public void SetIsChildOfTask()
        {
            isChildOfTask = true;
        }
        protected virtual void SetTaskState(TaskStatus _newTaskState)
        {
            previousState = currentTaskState;
            currentTaskState = _newTaskState;

            OnStatusChange?.Invoke();                   // call the event ON Status Change

            switch (currentTaskState)
            {
                case TaskStatus.ACHIEVED:
                    OnAchiveEvent?.Invoke();
                    break;

                case TaskStatus.IN_PROGRESS:
                    InProgressEvent?.Invoke();
                    break;

                case TaskStatus.FAILED:
                    OnFailEvent?.Invoke();
                    break;

                default:
                    Debug.LogError("Someting wront just happendeds because the desired state is not valid " + currentTaskState);
                    break;
            }
        }

        public TaskStatus GetPreviousTaskState()
        {
            return PreviousState;
        }
        public TaskStatus GetCurrentTaskState()
        {
            return CurrentTaskState;
        }
        public string GetName ()
        {
            return taskName;
        }
        public string GetDescription()
        {
            return taskDescription;
        }
        public int GetTaskId()
        {
            return taskId;
        }
        public bool GetIsChildOftask()
        {
            return isChildOfTask;
        }


        protected virtual void OnEnable()
        {
            rnd = new System.Random();

            if (taskName == "") taskName = "UNNNAMED_TASK";
            if (taskDescription == "") taskDescription = "NODESCRIPTION_SET";

            isChildOfTask = false;

        }



        // setup para tareas que REQUIERAN DE BLACKBOARD (datos generales)
        public virtual void Setup (TasksBlackboard _blackboard)
        {
            blackboard = _blackboard;
            SetRandomId();
            // Override this method

            OnStatusChange += NotifyStatusChange;
            OnAchiveEvent += OnAchivementNotify;
            InProgressEvent += InProgressNotify;
            OnFailEvent += FailedTaskNotify;
        }
        

        // funcion de update (en el caso de ser necesrio)
        public virtual void Tick(float _deltaTime)
        {
            // override IF necesary
        }
        // Funcion de check
        public virtual TaskStatus Check()
        {
            return TaskStatus.NONE;
        }


        public void ApplyReward ()
        {
            if (rewards_Lst != null && rewards_Lst.Count > 0)
            {
                foreach (Reward reward in rewards_Lst)
                {
                    reward.Apply();
                }
            }
            
        }

        // external way of making a task end BUT YOU SHOULD NOT USE SO ERASE IT AND USE INSTEAD SETCURRENTTASKSTATUS
        public virtual void ForceTaskFinish(TaskStatus _desiredEndStatus = TaskStatus.ACHIEVED)
        {
            if (_desiredEndStatus != TaskStatus.NONE) 
                currentTaskState = _desiredEndStatus;
        }



        protected virtual void NotifyStatusChange()
        {
            Debug.Log("Notifi you are now on " + currentTaskState);    
        }
        protected virtual void OnAchivementNotify()
        {
            Debug.Log("You achieved " + this.name + " task! ");
        }
        protected virtual void InProgressNotify()
        {
            Debug.Log("You are in progress of achiveing " + this.name + " task! ");
        }
        protected virtual void FailedTaskNotify()
        {
            Debug.Log("You failed " + this.name + " task! ");
        }


    }

    



}


