using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Chess;
using Assets.Scripts.Chess.Pieces;
using UnityEngine;

public class Pawn : Piece
{
    private int forwardMaxMovements = 1;
    private int diagonalMaxMovements = 2;

    private void Start()
    {
        board = Board.instance;

        GetBoardPosition();
        MoveToCell(boardPosition);
    }

    public override void GetPossibleMoves(bool omnidirectional)
    {
        if (!board) board = Board.instance;

        MovePositions.Clear();
        board.ClearValidPositions();

        int forward = GetForward();

        MovePositions.AddRange(FindPossibleMoves(boardPosition, 0, forward));
        MovePositions.AddRange(FindPossibleKills(boardPosition, 1, forward));
        MovePositions.AddRange(FindPossibleKills(boardPosition, -1, forward));
    }

    public override int CalculateCost(Cell nextPosition)
    {
        return Math.Abs(nextPosition.position.x - boardPosition.position.x);
    }

    public void Die()
    {
        boardPosition.piecePlaced = null;

        Destroy(gameObject);
    }

    public List<Cell> FindPossibleMoves(Cell initialCell, int xDirection, int yDirection)
    {
        var range = 1;
        var spawnLine = initialCell.GetComponent<PawnSpawnCell>();

        if (spawnLine) range = 2;

        var positions = new List<Cell>();
        var currentCell = initialCell;

        for (int i = 1; i <= range; i++)
        {
            var x = initialCell.position.x + xDirection * i;
            var y = initialCell.position.y + yDirection * i;

            currentCell = board.GetCell(x, y);

            if (currentCell.piecePlaced == null) positions.Add(currentCell);
        }

        return positions;
    }

    public List<Cell> FindPossibleKills(Cell initialCell, int xDirection, int yDirection)
    {
        var range = 2;
        var positions = new List<Cell>();
        var currentCell = initialCell;

        for (int i = 1; i <= range; i++)
        {
            var x = initialCell.position.x + xDirection * i;
            var y = initialCell.position.y + yDirection * i;

            currentCell = board.GetCell(x, y);

            if (!currentCell) break;

            if (currentCell.piecePlaced != null)
            {
                if (currentCell.piecePlaced.teamNumber != teamNumber) positions.Add(currentCell);
                break;
            }
        }

        return positions;
    }

    private int GetForward()
    {
        return 1;
    }
}
