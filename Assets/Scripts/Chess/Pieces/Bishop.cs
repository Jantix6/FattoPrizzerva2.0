using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Chess.Pieces
{
    public class Bishop : Piece, IHealth
    {
        [SerializeField] private float health;
        [SerializeField] private float maxHealth;

        public float Health { get => health; set => health = value; }

        private void Start()
        {
            board = Board.instance;

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

            int amount = 0;
            Piece lastPiece = null;

            var pieces = FindObjectsOfType<Piece>();

            foreach (var piece in pieces)
            {
                if (piece.player == player && piece != this)
                {
                    lastPiece = piece;
                    amount++;
                }
            }

            if (amount == 1 && lastPiece) lastPiece.omnidirectional = false;

            Destroy(gameObject);
        }

        public List<Cell> FindPossibleMoves(Cell initialCell, int xDirection, int yDirection)
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

                if ((currentCell.type == Cell.CellType.Portal && currentCell.unlocked) ||
                    currentCell.type == Cell.CellType.Jumper ||
                    (currentCell.type == Cell.CellType.DestructibleWall && CalculateCost(currentCell) >= 2))
                {
                    positions.Add(currentCell);
                    break;
                }

                positions.Add(currentCell);
            }

            return positions;
        }
    }
}
