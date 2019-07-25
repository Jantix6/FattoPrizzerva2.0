using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : Piece, IHealth
{
    [SerializeField] private float health;
    [SerializeField] private float maxHealth;

    public float Health { get => health; set => health = value; }

    private PieceNavigation pieceNavigation;

    private void Start()
    {
        pieceNavigation = GetComponent<PieceNavigation>();
        board = Board.instance;

        //Testing Purposes
        transform.position = new Vector3(UnityEngine.Random.Range(0, Board.instance.size),
                                         1,
                                         UnityEngine.Random.Range(0, Board.instance.size));
        GetBoardPosition();
    }

    public override void Move(Cell cell)
    {
        //Moved = true; //Testing, Uncomment When done

        ShowPossibleMoves(false);

        targetPiece = null;
        targetCell = null;

        targetPiece = cell.piecePlaced;
        targetCell = cell.type == Cell.CellType.Normal ? null : cell;
        direction = pieceNavigation.GetDirection(boardPosition, cell);

        cost = CalculateCost(cell);

        StartCoroutine(pieceNavigation.MoveTo(this, cell, MovementDone));
    }

    public void MovementDone()
    {
        if (targetPiece && targetPiece.AI_Controlled != AI_Controlled) PushPiece(cost, targetPiece, direction);

        if (targetCell)
        {
            switch (targetCell.type)
            {
                case Cell.CellType.Jumper:
                    targetCell.GetComponent<Jumper>().Jump(this);
                    break;
                case Cell.CellType.Portal:
                    targetCell.GetComponent<Portal>().TeleportPiece(this);
                    break;
                case Cell.CellType.DestructibleWall:
                    break;
                case Cell.CellType.IndestructibleWall:
                    break;
                case Cell.CellType.Storm:
                    break;
            }
        }

        //Testing Purposes, delete when done
        // GetPossibleMoves();
        // ShowPossibleMoves(true);
    }

    public override void GetPossibleMoves()
    {
        MovePositions.Clear();
        PortalPassedPositions.Clear();
        board.ClearValidPositions();

        MovePositions.AddRange(FindPossibleMoves(boardPosition, 1, 1));
        MovePositions.AddRange(FindPossibleMoves(boardPosition, 1, -1));
        MovePositions.AddRange(FindPossibleMoves(boardPosition, -1, 1));
        MovePositions.AddRange(FindPossibleMoves(boardPosition, -1, -1));
    }

    public override void ShowPossibleMoves(bool show)
    {
        for (int i = 0; i < MovePositions.Count; i++)
        {
            MovePositions[i].ShowAvailable(show);
        }

        for (int i = 0; i < PortalPassedPositions.Count; i++)
        {
            PortalPassedPositions[i].ShowAvailable(show);
        }
    }

    public override void MoveToCell(Cell cell)
    {
        boardPosition.piecePlaced = null;
        transform.position = new Vector3(cell.position.x, 1, cell.position.y);
        boardPosition = cell;
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

    public List<Cell> FindPossibleMoves(Cell initialCell, int xDirection, int yDirection)
    {
        bool IsValid = true;

        List<Cell> positions = new List<Cell>();

        Cell currentCell = initialCell;

        int iteration = 0;

        while (IsValid)
        {
            iteration++;

            int x = initialCell.position.x + (xDirection * iteration);
            int y = initialCell.position.y + (yDirection * iteration);

            currentCell = board.GetCell(x, y);

            IsValid = currentCell;

            if (!IsValid) break;

            if (CalculateCost(currentCell) <= ChessPlayer.instance.movements)
            {
                if (currentCell.piecePlaced != null)
                {
                    if (currentCell.piecePlaced.AI_Controlled && CalculateCost(currentCell) >= 2) positions.Add(currentCell);
                    break;
                }
                else if (currentCell.type != Cell.CellType.Normal)
                {
                    positions.Add(currentCell);

                    if (currentCell.type == Cell.CellType.Portal)
                    {
                        Portal portal = currentCell.GetComponent<Portal>();
                        List<Cell> portalCells = FindPossibleMoves(portal.connectedPortal, xDirection, yDirection);

                        PortalPassedPositions.AddRange(portalCells);
                    }
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
