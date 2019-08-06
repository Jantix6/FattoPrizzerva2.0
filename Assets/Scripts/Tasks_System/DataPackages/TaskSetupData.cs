using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Tasks
{
    // Dani you are not using this yet but take it into acount if the complexity of the data passed to each task grows higger than just 5 or 6 objects on initialization
    public struct TaskInitializationData
    {
        public Task taskToInitialize;                      // what task Am I?

        public TaskData taskSpecificData;                  // data spefific and only usable for this task
        public CanvasHosts canvasForHostingTask;           // which is the canvas that will be responsible of hosting me?
        public bool addToActiveTasks;                      // do we want to add this task to the active game tasks or not?
    }
}

