using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Dialogues
{

    [RequireComponent(typeof(DialogueManager))]
    // manager que se encargara de gestionar que cosas pueden pasar al seleccionar cada respuesta en los dialogos
    public class DialogEventsManager : MonoBehaviour
    {


        internal void UnFreezeGame()
        {
            // throw new NotImplementedException( "TRYING TO UNFREZE GAME");
        }

        internal void FrezeGame()
        {
            // throw new NotImplementedException("TRYING TO FREEZE GAME");
        }

        internal void TakeDamage(TestPlayer _target, float damageBaseOne)
        {
            _target.TakeDamage(damageBaseOne);
        }

    }
}

