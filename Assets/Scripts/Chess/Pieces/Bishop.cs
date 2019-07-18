using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : Piece, IHealth
{
    [SerializeField] private float health;
    [SerializeField] private float maxHealth;

    public float Health { get => health; set => health = value; }

    private PieceNavigation nav;

    private void Start()
    {
        nav = GetComponent<PieceNavigation>();
        board = Board.instance;

        //Testing Purposes
        transform.position = new Vector3(UnityEngine.Random.Range(0, Board.instance.size),
                                         1,
                                         UnityEngine.Random.Range(0, Board.instance.size));
        GetBoardPosition();
    }

    public override void Move(Cell cell)
    {
        StartCoroutine(nav.MoveTo(this, cell, MovementDone));
    }

    public void MovementDone(Cell cell)
    {
        MoveToCell(cell);
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
            board.GetCell(MovePositions[i].x, MovePositions[i].y).ShowAvailable(show);
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

    private List<Vector2Int> FindPossibleMoves(int xDirection, int yDirection)
    {
        bool IsValid = true;

        List<Vector2Int> positions = new List<Vector2Int>();

        Cell currentCell = new Cell();

        currentCell.position = boardPosition.position;

        while (IsValid)
        {
            currentCell.position += new Vector2Int(xDirection, yDirection);

            IsValid = board.ValidIndex(currentCell.position.x, currentCell.position.y);

            if (!IsValid) break;
            if (CalculateCost(currentCell) <= ChessPlayer.instance.movements) positions.Add(currentCell.position);
        }

        return positions;
    }
}
