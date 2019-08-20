using System;
using System.Collections.Generic;
using Assets.Scripts.Chess.Pieces;
using UnityEngine;
using UnityEngine.Events;
namespace Assets.Scripts.Chess.PieceActions
{
    public class ActionHandler : MonoBehaviour, IActionHandler
    {
        public List<IPieceAction> Actions { get; set; } = new List<IPieceAction>();
        public Action Callback { get; set; }

        public void AddAction(IPieceAction action)
        {
            Actions.Add(action);
        }

        public void ExecuteActions()
        {
            if (Actions.Count == 0)
            {
                Callback?.Invoke();
                return;
            }

            IPieceAction actionToDo = Actions[0];

            Actions.Remove(actionToDo);

            StartCoroutine(actionToDo.DoAction(ExecuteActions));
        }
    }
}