using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tasks
{
    public class DestroyTaskData : TaskData
    {
        [SerializeField] GameObject entityToDestroy;


        public DestroyTaskData (GameObject _entityToKill) 
        {
            entityToDestroy = _entityToKill;
        }

        public GameObject GetEntityToDestroy()
        {
            return entityToDestroy;
        }
    }
}


