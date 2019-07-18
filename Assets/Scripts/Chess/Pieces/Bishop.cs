using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : Piece, IHealth
{
    [SerializeField] private float health;
    [SerializeField] private float maxHealth;

    public float Health { get => health; set => health = value; }

    private PieceNavigation pieceNaviation;

    private void Start()
    {
        pieceNaviation = GetComponent<PieceNavigation>();
        board = Board.instance;

        //Testing Purposes
        transform.position = new Vector3(UnityEngine.Random.Range(0, Board.instance.size),
                                         1,
                                         UnityEngine.Random.Range(0, Board.instance.size));
        GetBoardPosition();
    }

    public override void Move(Cell cell)
    {
        StartCoroutine(pieceNaviation.MoveTo(this, cell, MovementDone));
    }

    public void MovementDone(PieceNavigation.MovementData data)
    {
        if (data.pieceInTargetCell) PushPiece(data.cost, data.pieceInTargetCell, data.direction);

        MoveToCell(data.currentCell);
        GetPossibleMoves();
        ShowPossibleMoves(true);
    }

    public override void GetPossibleMoves()
    {
        MovePositions.Clear();
        board.ClearValidPositions();

        MovePositions.AddRange(FindPossibleMoves(1, 1));
        MovePositions.AddRange(FindPossibleMoves(1, -1));
        MovePositions.AddRange(FindPossibleMoves(-1, 1));
        MovePositions.AddRange(FindPossibleMoves(-1, -1));
    }

    public override void ShowPossibleMoves(bool show)
    {
        for (int i = 0; i < MovePositions.Count; i++)
        {
            MovePositions[i].ShowAvailable(show);
        }
    }

    public override void MoveToCell(Cell cell)
    {
        boardPosition = cell;
        transform.position = new Vector3(boardPosition.position.x, 1, boardPosition.position.y);
        boardPosition.piecePlaced = this;
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

    private List<Cell> FindPossibleMoves(int xDirection, int yDirection)
    {
        bool IsValid = true;

        List<Cell> positions = new List<Cell>();

        Cell currentCell = boardPosition;

        int iteration = 0;

        while (IsValid)
        {
            iteration++;

            int x = boardPosition.position.x + (xDirection * iteration);
            int y = boardPosition.position.y + (yDirection * iteration);

            currentCell = board.GetCell(x, y);

            IsValid = currentCell;

            if (!IsValid) break;

            if (CalculateCost(currentCell) <= ChessPlayer.instance.movements)
            {
                if (currentCell.piecePlaced != null)
                {
                    if (currentCell.piecePlaced.AI_Controlled) positions.Add(currentCell);
                    break;
                }
                else
                {
                    positions.Add(currentCell);
                }
            }
        }

        return positions;
    }
}
