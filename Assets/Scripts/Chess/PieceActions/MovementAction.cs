using System;
using System.Collections;
using Assets.Scripts.Chess.Pieces;
using UnityEngine;

namespace Assets.Scripts.Chess.PieceActions
{
    public class MovementAction : IPieceAction
    {
        public int Cost { get; set; }

        private Cell cell;
        private Piece piece;
        private float speed;

        private float acceleration = 1.5f;

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
                speed += acceleration * Time.deltaTime; //Test pending
                yield return null;
            }

            piece.MoveToCell(cell);

            callback();
        }
    }
}