using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Chess.Pieces;
using UnityEngine;

namespace Assets.Scripts.Chess
{
    [CreateAssetMenu(fileName = "BoardSetting")]
    public class BoardSettings : ScriptableObject
    {
        public List<CellSetting> cells = new List<CellSetting>();
        public List<PieceSetting> pieces = new List<PieceSetting>();
    }

    [Serializable]
    public struct CellSetting
    {
        public Cell cellPrefab;

        public Vector2Int position;
    }

    [Serializable]
    public struct PieceSetting
    {
        public Piece PiecePrefab;
        public bool AI_Controlled;
        public int playerNumber;
        public Vector2Int position;
    }
}