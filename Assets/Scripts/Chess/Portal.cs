using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : Cell
{
    public Cell connectedPortal;

    public void TeleportPiece(Piece piece)
    {
        Vector2Int pieceNextPosition = connectedPortal.position + piece.direction;

        if (Board.instance.ValidIndex(pieceNextPosition))
        {
            piece.MoveToCell(connectedPortal);
            piece.Move(Board.instance.GetCell(pieceNextPosition.x, pieceNextPosition.y));

          //  piece.MovePositions.Clear();
          //  Board.instance.ClearValidPositions();

          //  piece.MovePositions.AddRange(piece.GetComponent<Bishop>().FindPossibleMoves(piece.direction.x, piece.direction.y));
          //  piece.ShowPossibleMoves(true);
        }

    }
}
