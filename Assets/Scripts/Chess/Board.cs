using System;
using System.Collections.Generic;
using Assets.Scripts.Chess.Pieces;
using UnityEngine;

namespace Assets.Scripts.Chess
{
    public class Board : MonoBehaviour
    {
        public static Board instance;

        public int size = 8;

        public Cell[,] board;

        public BoardSettings boardSettings;

        public Vector2 Offset;

        public Cell NormalCell;

        public List<Cell> validPositions;

        private Cell pendingPortal = null;

        private void Awake()
        {
            if (instance == null) instance = this;
            if (instance != this) Destroy(gameObject);

            SetUp();
            //CreateBoard();
        }

        public void SetUp()
        {
            board = new Cell[size, size];

            var cells = FindObjectsOfType<Cell>();

            foreach (var cell in cells)
            {
                cell.SetBoard();
            }

            var pieces = FindObjectsOfType<Piece>();

            foreach (var piece in pieces)
            {
                piece.GetBoardPosition();
            }

            if (boardSettings) SpawnPieces();
        }

        public void CreateBoard()
        {
            if (boardSettings) GetBoardSettings();

            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    if (board[x, y] != null) continue;

                    board[x, y] = Instantiate(NormalCell.gameObject).GetComponent<Cell>();
                    SetCellPosition(board[x, y], x, y);
                }
            }

            if (boardSettings) SpawnPieces();
        }

        private void SpawnPieces()
        {
            foreach (PieceSetting piece in boardSettings.pieces)
            {
                Cell cellToPositionate = GetCell(piece.position.x, piece.position.y);

                if (!cellToPositionate || cellToPositionate.type != Cell.CellType.Normal) return;

                Piece currentPiece = Instantiate(piece.PiecePrefab.gameObject).GetComponent<Piece>();

                currentPiece.MoveToCell(cellToPositionate);
                currentPiece.teamNumber = piece.playerNumber;
            }
        }

        public void GetBoardSettings()
        {
            foreach (CellSetting cell in boardSettings.cells)
            {
                if (!ValidIndex(cell.position.x, cell.position.y)) return;

                Cell currentCell = Instantiate(cell.cellPrefab.gameObject).GetComponent<Cell>();

                SetCellPosition(currentCell, cell.position.x, cell.position.y);
                currentCell.position = cell.position;
                board[cell.position.x, cell.position.y] = currentCell;

                if (currentCell.type == Cell.CellType.Portal) ConnectPortal(currentCell);
            }
        }

        private void ConnectPortal(Cell portal)
        {
            if (!portal) return;

            if (pendingPortal != null && pendingPortal != portal)
            {
                portal.connectedPortal = pendingPortal;
                pendingPortal.connectedPortal = portal;
                pendingPortal = null;
            }
            else
            {
                pendingPortal = portal;
            }
        }

        public void SetCellPosition(Cell cell, int x, int y)
        {
            Vector2 positionAfterOffset = new Vector2(x + Offset.x, y + Offset.y);
            cell.SetPosition(positionAfterOffset);
        }

        public void ClearValidPositions()
        {
            List<Cell> validCells = new List<Cell>();

            foreach (Cell validCell in validPositions)
            {
                validCells.Add(validCell);
            }

            foreach (Cell cell in validCells)
            {
                cell.ShowAvailable(false);
            }
        }

        public bool ValidIndex(int x, int y) => ((x < size && y < size) && (x >= 0 && y >= 0));

        public bool ValidIndex(Vector2Int index) => ((index.x < size && index.y < size) && (index.x >= 0 && index.y >= 0));

        public Cell GetCell(int x, int y) => ValidIndex(x, y) ? (board[x, y] ? board[x, y] : null) : null;

        public static Vector2Int GetDirection(Cell origin, Cell destination)
        {
            Vector2Int direction = (destination.position - origin.position);

            if (direction.x != 0) direction.x = Math.Sign(direction.x);
            if (direction.y != 0) direction.y = Math.Sign(direction.y);

            return direction;
        }


        //Test

        public void GetBoard()
        {
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    var Cells = Physics.OverlapSphere(new Vector3(x, 0, y), 0.3f);

                    foreach (var cell in Cells)
                    {
                        if (cell.GetComponent<Cell>() != null)
                        {
                            board[x, y] = cell.GetComponent<Cell>();

                            continue;
                        }
                    }
                }
            }
        }
    }
}
