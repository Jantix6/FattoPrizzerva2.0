using System;
using System.Collections;
using System.Collections.Generic;
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

        while ((piece.transform.position - destination).magnitude > 0.1f)
        {
            piece.transform.position = Vector3.MoveTowards(piece.transform.position, destination, speed * Time.deltaTime);

            yield return null;
        }

        piece.MoveToCell(cell);

        callback();
    }
}

public class TeleportAction : PieceAction
{
    public int Cost { get; set; }

    private Cell cell;
    private Piece piece;
    private float speed;

    public TeleportAction(Cell cell, int cost, float speed, Piece piece)
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

        while ((piece.transform.position - destination).magnitude > 0.1f)
        {
            piece.transform.position = Vector3.MoveTowards(piece.transform.position, destination, speed * Time.deltaTime);

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
                piece.transform.position = Vector3.MoveTowards(piece.transform.position, destination, speed * Time.deltaTime);

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

public class PushAction : PieceAction
{
    public int Cost { get; set; }

    private Piece pieceToPush;
    private Piece piece;
    private float speed;

    public PushAction(Piece pieceToPush, float speed, Piece piece)
    {
        this.pieceToPush = pieceToPush;
        this.speed = speed;
        this.piece = piece;
    }

    public IEnumerator DoAction(Action callback)
    {
        this.Cost = piece.CalculateCost(pieceToPush.boardPosition);

        piece.direction = Board.GetDirection(piece.boardPosition, pieceToPush.boardPosition);

        var beforeCell = GetCell(pieceToPush.boardPosition, new Vector2Int(-piece.direction.x, -piece.direction.y));

        var destination = new Vector3(beforeCell.position.x, piece.transform.position.y, beforeCell.position.y);

        while ((piece.transform.position - destination).magnitude > 0.1f)
        {
            piece.transform.position = Vector3.MoveTowards(piece.transform.position, destination, speed * Time.deltaTime);

            yield return null;
        }

        piece.MoveToCell(beforeCell);
        piece.PushPiece(Cost, pieceToPush, piece.direction);

        var enemyCell = GetCell(beforeCell, piece.direction);

        if (!enemyCell.piecePlaced) piece.MoveToCell(enemyCell);

        callback();
    }

    private Cell GetCell(Cell currentCell, Vector2Int direction)
    {
        return Board.instance.GetCell(currentCell.position.x + direction.x, currentCell.position.y + direction.y);
    }
}

public class JumpAction : PieceAction
{
    public int Cost { get; set; }

    private Action callBack;
    private Cell jumperCell;
    private Piece piece;
    private float speed;

    public JumpAction(Cell jumperCell, float speed, Piece piece)
    {
        this.Cost = piece.CalculateCost(jumperCell);
        this.jumperCell = jumperCell;
        this.speed = speed;
        this.piece = piece;
    }

    public IEnumerator DoAction(Action callback)
    {
        callBack = callback;

        ApproachJumper();

        yield return null;
    }

    private void ApproachJumper()
    {
        piece.direction = Board.GetDirection(piece.boardPosition, jumperCell);
        piece.StartCoroutine(new MovementAction(jumperCell, 0, speed, piece).DoAction(Fly));
    }

    private void Fly()
    {
        piece.transform.GetChild(0).transform.localPosition = Vector3.up * 0.5f;
        piece.StartCoroutine(ProcessCell(FindJumpDestination(piece.direction.x, piece.direction.y)).DoAction(End));
    }

    private void End()
    {
        piece.transform.GetChild(0).transform.localPosition = Vector3.zero;
        callBack();
    }

    private Cell FindJumpDestination(int xDirection, int yDirection)
    {
        int range = Cost <= 4 ? Cost / 2 : (Cost / 4) + 2;

        bool IsValid = true;

        List<Cell> positions = new List<Cell>();

        Cell currentCell = jumperCell;

        int iteration = 0;

        while (iteration <= range && IsValid)
        {
            iteration++;

            int x = jumperCell.position.x + (xDirection * iteration);
            int y = jumperCell.position.y + (yDirection * iteration);

            currentCell = Board.instance.GetCell(x, y);

            IsValid = currentCell;

            if (currentCell && currentCell.type == Cell.CellType.IndestructibleWall) break;
        }

        return currentCell;
    }

    private PieceAction ProcessCell(Cell cell)
    {
        PieceAction actionToDo = null;

        if (cell.piecePlaced)
        {
            if (piece.dummy) actionToDo = new MovementAction(cell, 0, speed, piece);
            else actionToDo = new PushAction(cell.piecePlaced, speed, piece);
        }
        else
        {
            switch (cell.type)
            {
                case Cell.CellType.Normal:
                    actionToDo = new MovementAction(cell, 0, speed, piece);
                    break;

                case Cell.CellType.Portal:
                    actionToDo = new TeleportAction(cell, 0, speed, piece);
                    break;
            }
        }

        return actionToDo;
    }
}