using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Dialogues
{

    [RequireComponent(typeof(DialogueManager))]
    // manager que se encargara de gestionar que cosas pueden pasar al seleccionar cada respuesta en los dialogos
    public class DialogEventsController : MonoBehaviour
    {
        [SerializeField] private bool isGameFrozen;

        private void Awake()
        {
            isGameFrozen = false;
        }

        internal void UnFreezeGame()
        {
            // throw new NotImplementedException( "TRYING TO UNFREZE GAME");
            // estaria bien que el game controller sea quien conjele y desconjele el juego

            isGameFrozen = false;
        }

        internal void FrezeGame()
        {
            // estaria bien que el game controller sea quien conjele y desconjele el juego
            // throw new NotImplementedException("TRYING TO FREEZE GAME");

            isGameFrozen = true;
        }

    }
}

