using System;
using System.Collections;

namespace Assets.Scripts.Chess.PieceActions
{
    public interface IPieceAction
    {
        int Cost { get; set; }
        IEnumerator DoAction(Action callback);
    }
}