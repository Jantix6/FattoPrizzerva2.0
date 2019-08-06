using System.Collections.Generic;
using Assets.Scripts.Chess.Pieces;
using UnityEngine;

namespace Assets.Scripts.Chess.PieceActions
{
    public class ActionHandler : MonoBehaviour, IActionHandler
    {
        public List<IPieceAction> Actions { get; set; } = new List<IPieceAction>();

        public void AddAction(IPieceAction action)
        {
            Actions.Add(action);
        }

        public void ExecuteActions()
        {
            if (Actions.Count == 0)
            {
                return;
            }

            IPieceAction actionToDo = Actions[0];

            Actions.Remove(actionToDo);

            StartCoroutine(actionToDo.DoAction(ExecuteActions));
        }
    }
}