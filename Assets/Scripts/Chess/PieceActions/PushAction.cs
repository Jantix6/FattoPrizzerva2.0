using System;
using System.Collections;
using Assets.Scripts.Chess.Pieces;
using UnityEngine;

namespace Assets.Scripts.Chess.PieceActions
{
    public class PushAction : IPieceAction
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
}