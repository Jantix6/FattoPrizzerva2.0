using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class PieceAction
{
    public int cost;
    public abstract IEnumerator DoAction(Action callback);

}

public class MovementAction : PieceAction
{
    private Cell destination;

    public MovementAction(Cell destination, int cost)
    {
        this.destination = destination;
        this.cost = cost;
    }

    public override IEnumerator DoAction(Action callback)
    {
        throw new NotImplementedException();
    }
}

