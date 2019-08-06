using System;
using System.Collections;
using Assets.Scripts.Chess.Pieces;
using UnityEngine;

namespace Assets.Scripts.Chess.PieceActions
{
    public class TeleportAction : IPieceAction
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

            if (cell.connectedPortal.portalDirection != Vector2Int.zero) piece.direction = cell.connectedPortal.portalDirection;

            var nextCell = GetNextCell(cell.connectedPortal, piece.direction);

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
}