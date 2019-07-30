using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Chess.Pieces;
using UnityEngine;

public class ActionHandler : MonoBehaviour, IActionHandler
{
    public List<PieceAction> Actions { get; set; }

    public void AddAction(PieceAction action)
    {
        Actions.Add(action);
    }

    public void ExecuteActions()
    {
        PieceAction actionToDo = Actions[0];

        Actions.Remove(Actions[0]);

        StartCoroutine(actionToDo.DoAction(ExecuteActions));
    }
}