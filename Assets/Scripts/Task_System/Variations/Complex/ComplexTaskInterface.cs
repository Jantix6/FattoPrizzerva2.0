using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace Tasks
{
    public class ComplexTaskInterface : TasKInterface
    {
        [SerializeField] private List<TasKInterface> requiredInterfaces;
        [SerializeField] private bool is_Ready = false;


        private void OnEnable()
        {
            is_Ready = false;
        }

        private void OnValidate()
        {
            if (is_Ready)
            {
                if (TargetTask == null || !(TargetTask is ComplexTask))
                {
                    RemoveComponents();
                    TargetTask = null;

                } else
                {
                    
                }

            }
            else
            {
                Debug.LogWarning("SETUP IS WAITING TO BE PERFORMED");

                if (TargetTask != null )
                {
                    if (TargetTask is ComplexTask)
                    {
                        Debug.LogWarning("CHANGING COMPLEX TASK");
                        RemoveComponents();
                        AddComponents();
                    }
                    else if (TargetTask is SimpleTask)
                    {
                        Debug.LogError("The assigned task is not from the expected type " + typeof(ComplexTask));
                        TargetTask = null;
                        RemoveComponents();
                        
                    }

                }
                else
                {
                    RemoveComponents();
                }
            }
        }

        private void AddComponents()
        {
            // FOR EVERY TASK INTO THE COMPLEX TASK (at the moment just one level of deph) we create an interface to set them up)
            List<Task> childTasks;
            Type type;

            childTasks = (TargetTask as ComplexTask).GetTasksList();
            if (childTasks.Count > 0)
            {
                foreach (Task childTask in childTasks)
                {
                    type = childTask.TaskInterface_Type;

                    if (type != null)
                    {
                        TasKInterface _addedComponent = gameObject.AddComponent(type) as TasKInterface;
                        _addedComponent.TargetTask = childTask;

                        // add the comoponent
                        if (requiredInterfaces == null)
                            requiredInterfaces = new List<TasKInterface>();

                        requiredInterfaces.Add(_addedComponent);
                    }
                    else
                        Debug.LogError("COMPLEX TASK INTERFACE: A type of interface ouuld not be found, you need to define it at the ONENABLE of the task");

                }
                is_Ready = true;
            } else
            {
                is_Ready = false;
            }



        }
        private void RemoveComponents()
        {
            if (requiredInterfaces != null)
            {
                foreach (TasKInterface tasKInterface in requiredInterfaces)
                {
                    // aplicamos un delay a la destruccion del objeto (debido a que estamos en edit mode y de otro modo unity no puede destruir el objeto al estar
                    // en un OnValidate)
                    UnityEditor.EditorApplication.delayCall += () =>
                    {
                        Debug.LogWarning("Destroying " + tasKInterface);
                        DestroyImmediate(tasKInterface);
                    };
                }
            }

            is_Ready = false;
        }


        public override void InitializeTask()
        {
            TasksManager.Instance.SetupTask(TargetTask);
        }

        private void OnDestroy()
        {
            RemoveComponents();
        }

    }
}


