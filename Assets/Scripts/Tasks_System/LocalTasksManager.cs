using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Tasks
{
    public class LocalTasksManager : MonoBehaviour
    {
        private List<TasKInterface> objectTaskInterfaces;

        private void Awake()
        {
            objectTaskInterfaces = new List<TasKInterface>();
            objectTaskInterfaces.AddRange(GetComponents<TasKInterface>());
            InitializeAllInterfaces();
        }


        // --------------------------------------------------------------------------------------- //

        private void InitializeAllInterfaces()
        {
            foreach (TasKInterface inter in objectTaskInterfaces)
            {
                // Only initialize the master parents cause they initilize their children
                if (inter.TargetTask.GetIsChildOftask() == false)
                {
                    inter.InitializeTask();
                    Debug.LogError("INITIALIZING " + inter.TargetTask.name);
                } 
            }
        }

    }
}

