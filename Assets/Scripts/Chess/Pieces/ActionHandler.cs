using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Chess.Pieces;
using UnityEngine;

public class ActionHandler : MonoBehaviour, IActionHandler
{
    public List<PieceAction> Actions { get; set; } = new List<PieceAction>();

    public void AddAction(PieceAction action)
    {
        Actions.Add(action);
    }

    public void ExecuteActions()
    {
        if (Actions.Count == 0) return;

        PieceAction actionToDo = Actions[0];

        Actions.Remove(actionToDo);

        StartCoroutine(actionToDo.DoAction(ExecuteActions));
    }
}