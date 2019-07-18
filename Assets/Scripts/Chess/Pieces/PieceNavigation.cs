using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PieceNavigation : MonoBehaviour
{
    [SerializeField] private float speed;

    public IEnumerator MoveTo(Piece piece, Cell cell, UnityAction<Cell> MovementDone)
    {
        Board.instance.ClearValidPositions();

        piece.boardPosition.piecePlaced = null;

        Piece pieceInNextPosition = cell.piecePlaced;
        Vector2Int direction = Vector2Int.zero;

        if (pieceInNextPosition)
        {
            direction = GetDirection(piece.boardPosition, pieceInNextPosition.boardPosition);

            cell = Board.instance.GetCell(pieceInNextPosition.boardPosition.position.x - direction.x,
                                          pieceInNextPosition.boardPosition.position.y - direction.y);
        }

        Vector3 destination = new Vector3(cell.position.x, piece.transform.position.y, cell.position.y);

        while ((piece.transform.position - destination).magnitude > 0.1f)
        {
            piece.transform.position = Vector3.MoveTowards(piece.transform.position, destination, speed * Time.deltaTime);

            yield return null;
        }

        if (pieceInNextPosition) piece.PushPiece(piece.CalculateCost(cell), pieceInNextPosition, direction);

        MovementDone(cell);
    }

    private Vector2Int GetDirection(Cell origin, Cell destination)
    {
        Vector2Int direction = (destination.position - origin.position);

        if (direction.x != 0) direction.x = direction.x > 1 ? 1 : -1;
        if (direction.y != 0) direction.y = direction.y > 1 ? 1 : -1;

        return direction;
    }
}
