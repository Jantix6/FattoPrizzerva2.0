using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tasks
{
    public abstract class TimeRelatedTask : SimpleTask
    {
        [SerializeField] protected float targetTime;
        protected float currentTime;
        protected bool autoFinalizeComplexTask;         // This bool controls if this timed task is dominant at the time of determining if a complex task is done or not (ex, if the time ends the complex task should fail)

        public float GetCurrentTime() {
            return currentTime;
        }
        public float GetTargetTime() {
            return targetTime;
        }
        

    }


}





