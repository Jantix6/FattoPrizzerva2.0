using System.Collections.Generic;

namespace Assets.Scripts.Chess.Pieces
{
    public interface IActionHandler
    {
        List<PieceAction> Actions { get; set; }

        void AddAction(PieceAction action);
        void ExecuteActions();
    }
}