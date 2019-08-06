using System;
using System.Collections;
using Assets.Scripts.Chess.Pieces;
using UnityEngine;

namespace Assets.Scripts.Chess.PieceActions
{
    public class DestroyAction : IPieceAction
    {
        public int Cost { get; set; }

        private Action callBack;
        private Cell wall;
        private Piece piece;
        private float speed;

        public DestroyAction(Cell wall, int cost, float speed, Piece piece)
        {
            this.Cost = cost;
            this.wall = wall;
            this.piece = piece;
            this.speed = speed;
        }

        public IEnumerator DoAction(Action callback)
        {
            this.callBack = callback;

            ApproachWall();

            yield break;
        }

        public void ApproachWall()
        {
            piece.direction = Board.GetDirection(piece.boardPosition, wall);
            piece.StartCoroutine(new MovementAction(wall, 0, speed, piece).DoAction(AttackWall));
        }

        private void AttackWall()
        {

            float damage = CalculateDamage();
            float wallHealth = wall.health;

            wallHealth -= damage;
            wallHealth = Mathf.Clamp(wallHealth, 0, 1);

            if (!piece.dummy)
            {
                wall.health = wallHealth;

                if (wall.health == 0)
                {
                    wall.type = Cell.CellType.Normal;
                    wall.GetComponent<Material>().color = Color.white;
                }
                else piece.MoveToCell(GetCell(wall, new Vector2Int(-piece.direction.x, -piece.direction.y)));
            }
            else
            {
                if (wallHealth != 0) piece.MoveToCell(GetCell(wall, new Vector2Int(-piece.direction.x, -piece.direction.y)));
            }

            callBack();
        }

        public float CalculateDamage()
        {
            if (Cost == 3) return 0.5f;
            if (Cost == 4) return 0.5f;
            if (Cost == 5) return 1;

            return 0;
        }

        private Cell GetCell(Cell currentCell, Vector2Int direction)
        {
            return Board.instance.GetCell(currentCell.position.x + direction.x, currentCell.position.y + direction.y);
        }
    }
}