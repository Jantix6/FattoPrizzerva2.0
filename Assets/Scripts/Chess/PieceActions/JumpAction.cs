using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Chess.Pieces;
using UnityEngine;

namespace Assets.Scripts.Chess.PieceActions
{
    public class JumpAction : IPieceAction
    {
        public int Cost { get; set; }

        private Action callBack;
        private Cell jumperCell;
        private Piece piece;
        private float speed;

        public JumpAction(Cell jumperCell, float speed, Piece piece)
        {
            this.Cost = piece.CalculateCost(jumperCell);
            this.jumperCell = jumperCell;
            this.speed = speed;
            this.piece = piece;
        }

        public IEnumerator DoAction(Action callback)
        {
            callBack = callback;

            ApproachJumper();

            yield return null;
        }

        private void ApproachJumper()
        {
            piece.direction = Board.GetDirection(piece.boardPosition, jumperCell);
            piece.StartCoroutine(new MovementAction(jumperCell, 0, speed, piece).DoAction(Fly));
        }

        private void Fly()
        {
            piece.transform.GetChild(0).transform.localPosition = Vector3.up * 0.5f;
            piece.StartCoroutine(ProcessCell(FindJumpDestination(piece.direction.x, piece.direction.y)).DoAction(End));
        }

        private void End()
        {
            if (piece.boardPosition.type == Cell.CellType.Void && !piece.dummy)
            {
                piece.ActionHandler.Actions.Add(new DieAction(piece));
            }

            piece.transform.GetChild(0).transform.localPosition = Vector3.zero;
            callBack();
        }

        private Cell FindJumpDestination(int xDirection, int yDirection)
        {
            int range = Cost <= 4 ? Cost / 2 : (Cost / 4) + 2;

            bool IsValid = true;

            List<Cell> positions = new List<Cell>();

            Cell currentCell = jumperCell;

            int iteration = 0;

            while (iteration <= range && IsValid)
            {
                iteration++;

                int x = jumperCell.position.x + (xDirection * iteration);
                int y = jumperCell.position.y + (yDirection * iteration);

                currentCell = Board.instance.GetCell(x, y);

                IsValid = currentCell;

                if (currentCell && currentCell.type == Cell.CellType.IndestructibleWall) break;
            }

            return currentCell;
        }

        private IPieceAction ProcessCell(Cell cell)
        {
            IPieceAction actionToDo = null;

            if (cell.piecePlaced)
            {
                if (piece.dummy) actionToDo = new MovementAction(cell, 0, speed, piece);
                else actionToDo = new PushAction(cell.piecePlaced, speed, piece);
            }
            else
            {
                switch (cell.type)
                {
                    case Cell.CellType.Normal:
                        actionToDo = new MovementAction(cell, 0, speed, piece);
                        break;

                    case Cell.CellType.Portal:
                        actionToDo = new TeleportAction(cell, 0, speed, piece);
                        break;
                    case Cell.CellType.DestructibleWall:

                        actionToDo = new DestroyAction(cell, Cost, speed, piece);
                        break;
                }
            }

            return actionToDo;
        }
    }
}