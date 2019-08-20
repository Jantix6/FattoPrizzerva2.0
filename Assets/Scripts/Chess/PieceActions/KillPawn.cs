using System;
using System.Collections;
using Assets.Scripts.Chess.Pieces;
using UnityEngine;

namespace Assets.Scripts.Chess.PieceActions
{
    public class KillPawn : IPieceAction
    {
        public int Cost { get; set; }

        private Pawn pawnToKill;
        private Piece piece;
        private float speed;

        public KillPawn(Pawn pawnToKill, float speed, Piece piece)
        {
            this.pawnToKill = pawnToKill;
            this.speed = speed;
            this.piece = piece;
        }

        public IEnumerator DoAction(Action callback)
        {
            piece.direction = Board.GetDirection(piece.boardPosition, pawnToKill.boardPosition);

            var destination = new Vector3(pawnToKill.boardPosition.position.x, piece.transform.position.y, pawnToKill.boardPosition.position.y);

            while ((piece.transform.position - destination).magnitude > 0.1f)
            {
                piece.transform.position = Vector3.MoveTowards(piece.transform.position, destination, speed * Time.deltaTime);

                yield return null;
            }

            var enemyCell = pawnToKill.boardPosition;

            pawnToKill.Die();
            piece.MoveToCell(enemyCell);

            callback();
        }

        private Cell GetCell(Cell currentCell, Vector2Int direction)
        {
            return Board.instance.GetCell(currentCell.position.x + direction.x, currentCell.position.y + direction.y);
        }
    }
}