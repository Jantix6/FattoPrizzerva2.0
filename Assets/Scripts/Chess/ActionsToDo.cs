using System;
using System.Collections;
using UnityEngine;

public interface PieceAction
{
    int Cost { get; set; }
    IEnumerator DoAction(Action callback);
}

public class MovementAction : PieceAction
{
    public int Cost { get; set; }

    private Cell cell;
    private Piece piece;
    private float speed;

    public MovementAction(Cell cell, int cost, float speed, Piece piece)
    {
        this.cell = cell;
        this.Cost = cost;
        this.speed = speed;
        this.piece = piece;
    }

    public IEnumerator DoAction(Action callback)
    {
        piece.direction = Board.GetDirection(piece.boardPosition, cell);

        var destination = new Vector3(cell.position.x, piece.transform.position.y, cell.position.y);
        var direction = new Vector3(piece.direction.x, 0, piece.direction.y).normalized;

        while ((piece.transform.position - destination).magnitude > 0.1f)
        {
            piece.transform.position += direction * speed * Time.deltaTime;

            yield return null;
        }

        piece.MoveToCell(cell);

        callback();
    }
}

public class TeleportAction : PieceAction
{
    public int Cost { get; set; }

    private Portal cell;
    private Piece piece;
    private float speed;

    public TeleportAction(Portal cell, int cost, float speed, Piece piece)
    {
        this.cell = cell;
        this.Cost = cost;
        this.speed = speed;
        this.piece = piece;
    }

    public IEnumerator DoAction(Action callback)
    {
        piece.direction = Board.GetDirection(piece.boardPosition, cell);

        var destination = new Vector3(cell.position.x, piece.transform.position.y, cell.position.y);
        var direction = new Vector3(piece.direction.x, 0, piece.direction.y).normalized;

        while ((piece.transform.position - destination).magnitude > 0.1f)
        {
            piece.transform.position += direction * speed * Time.deltaTime;

            yield return null;
        }

        piece.MoveToCell(cell);
        piece.MoveToCell(cell.connectedPortal);

        var nextCell = GetNextCell(piece.boardPosition, piece.direction);

        if (nextCell)
        {
            destination = new Vector3(nextCell.position.x, piece.transform.position.y, nextCell.position.y);

            while ((piece.transform.position - destination).magnitude > 0.1f)
            {
                piece.transform.position += direction * speed * Time.deltaTime;

                yield return null;
            }

            piece.MoveToCell(nextCell);
        }

        callback();
    }

    private Cell GetNextCell(Cell currentCell, Vector2Int direction)
    {
        return Board.instance.GetCell(currentCell.position.x + direction.x, currentCell.position.y + direction.y);
    }
}