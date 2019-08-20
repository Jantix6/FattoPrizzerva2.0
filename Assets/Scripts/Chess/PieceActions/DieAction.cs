using System;
using System.Collections;
using Assets.Scripts.Chess.Pieces;
using UnityEngine;

namespace Assets.Scripts.Chess.PieceActions
{
    public class DieAction : IPieceAction
    {
        public int Cost { get; set; }
        public Piece piece;

        public DieAction(Piece piece)
        {
            this.piece = piece;
        }

        public IEnumerator DoAction(Action callback)
        {
            yield return new WaitForSeconds(0.5f);

            piece.GetComponent<IHealth>().Die();
        }
    }
}