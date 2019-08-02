using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : Piece, IHealth
{
    [SerializeField] private float health;
    [SerializeField] private float maxHealth;

    public float Health { get => health; set => health = value; }

    private void Start()
    {
        board = Board.instance;

        //Testing Purposes
        if (!boardPosition) transform.position = new Vector3(UnityEngine.Random.Range(0, Board.instance.size),
                                                             1,
                                                             UnityEngine.Random.Range(0, Board.instance.size));
        GetBoardPosition();
    }

    public override void GetPossibleMoves(bool omnidirectional)
    {
        if (!board) board = Board.instance;

        MovePositions.Clear();
        board.ClearValidPositions();

        if (omnidirectional)
        {
            MovePositions.AddRange(FindPossibleMoves(boardPosition, 1, 1));
            MovePositions.AddRange(FindPossibleMoves(boardPosition, 1, -1));
            MovePositions.AddRange(FindPossibleMoves(boardPosition, -1, 1));
            MovePositions.AddRange(FindPossibleMoves(boardPosition, -1, -1));
        }
        else
        {
            MovePositions.AddRange(FindPossibleMoves(boardPosition, direction.x, direction.y));
        }
    }

    public override int CalculateCost(Cell nextPosition)
    {
        return Math.Abs(nextPosition.position.x - boardPosition.position.x);
    }

    public void GetDamage(float damage)
    {
        if (Health <= 0) return;

        Health -= damage;
        Health = Mathf.Clamp(Health, 0, maxHealth);

        if (Health == 0) Die();
    }

    public void Die()
    {
        boardPosition.piecePlaced = null;
        Destroy(gameObject);
    }

    public override List<Cell> FindPossibleMoves(Cell initialCell, int xDirection, int yDirection)
    {
        var IsValid = true;

        var positions = new List<Cell>();
        var currentCell = initialCell;

        var iteration = 0;

        while (IsValid)
        {
            iteration++;

            var x = initialCell.position.x + xDirection * iteration;
            var y = initialCell.position.y + yDirection * iteration;

            currentCell = board.GetCell(x, y);

            IsValid = currentCell;

            if (!IsValid) break;

            if (CalculateCost(currentCell) > player.movements) break;

            if (currentCell.piecePlaced != null)
            {
                if (currentCell.piecePlaced.teamNumber != teamNumber && CalculateCost(currentCell) >= 2) positions.Add(currentCell);
                break;
            }

            if (currentCell.type == Cell.CellType.Portal || currentCell.type == Cell.CellType.Jumper)
            {
                positions.Add(currentCell);
                break;
            }

            positions.Add(currentCell);
        }

        return positions;
    }
}
