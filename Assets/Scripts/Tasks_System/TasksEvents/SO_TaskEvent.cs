using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Tasks
{
    /// <summary>
    /// Seria intersante que todos los eventos resultantes de el completado de las tareas heredaran de este objeto aunque todo dependera de la aproximacion
    /// que se quiere hacer a las posibles """"""recompensas"""""" que aparecen tras cada evento resuelto de forma satisfactoria
    /// </summary>
    public class SO_TaskEvent : ScriptableObject
    {


        public virtual void Initialize()
        {

        }

        public virtual void Execute()
        {

        }


    }
}


