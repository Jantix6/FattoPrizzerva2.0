using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Chess;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Assets.Scripts.Chess.Pieces;

public class PawnPlayer : ChessPlayer
{
    public int selectedNumber;
    public int selectCellOne = -1;
    public int selectCellTwo = -1;

    public bool deletedCells;

    public int spawnRow = 1;

    public GameObject selectNumberUI;

    public int neededRespawnPoints = 6;
    public int respawnPoints = 0;

    private List<PawnSpawnCell> spawCells = new List<PawnSpawnCell>();
    public List<Piece> pawns = new List<Piece>();
    public GameObject pawn;

    public Text spawnPoints;

    private void Start()
    {
        AddRespawnPoints(0);
        var allCells = FindObjectsOfType<PawnSpawnCell>();

        foreach (var cell in allCells)
        {
            if (cell.team == playerNumber) spawCells.Add(cell);
        }
    }

    public new void Update()
    {
        if (!deletedCells)
        {
            turn = 0;
        }
    }

    public override void StartTurn()
    {
        if (deletedCells)
        {
            turn++;

            foreach (var pawn in pawns)
            {
                if (pawn) pawn.Moved = false;
            }
        }
        else
        {
            selectNumberUI.SetActive(true);
        }

        movements = 10000;
        maxMovements = 100000;
    }

    public new void UpdateUI()
    {
        playerText.text = "Player: " + (playerNumber + 1);
        turnText.text = "Turn: " + turn;
    }

    public void EliminateSpawnCells(int selectedCellOne, int selectedCellTwo)
    {
        selectedCellOne += selectedNumber;
        selectedCellTwo += selectedNumber;

        if (selectedCellOne >= 8) selectedCellOne -= 8;
        if (selectedCellTwo >= 8) selectedCellTwo -= 8;

        Cell cellOne = Board.instance.GetCell(selectedCellOne, spawnRow);
        Cell cellTwo = Board.instance.GetCell(selectedCellTwo, spawnRow);

        var player = PieceSelector.instance.player as PawnPlayer;

        cellOne.GetComponent<PawnSpawnCell>().spawnable = false;
        cellTwo.GetComponent<PawnSpawnCell>().spawnable = false;

        cellOne.gameObject.SetActive(false);
        cellTwo.gameObject.SetActive(false);

        player.deletedCells = true;
        player.SpawnLineOfPawns();
    }

    public void SelectNumber(int number)
    {
        selectedNumber = number;
        selectNumberUI.SetActive(false);

        PieceSelector.instance.EndTurn();
    }

    public void SelectCellNumber(int number)
    {
        if (selectCellOne != -1)
        {
            if (selectCellTwo == -1) selectCellTwo = number;
        }
        else
        {
            selectCellOne = number;
        }

        if (selectCellOne != -1 && selectCellTwo != -1)
        {
            deletedCells = true;
            selectNumberUI.SetActive(false);

            PieceSelector.instance.EndTurn();

            var player = PieceSelector.instance.player as PawnPlayer;

            player.EliminateSpawnCells(selectCellOne, selectCellTwo);
        }
    }

    public void AddRespawnPoints(int amount)
    {
        respawnPoints += amount;

        if (respawnPoints >= neededRespawnPoints)
        {
            respawnPoints = 0;

            SpawnLineOfPawns();

            neededRespawnPoints++;
        }

        spawnPoints.text = "RespawnPoints: " + respawnPoints.ToString();
    }

    public void SpawnLineOfPawns()
    {
        foreach (var cell in spawCells)
        {
            if (cell.spawnable)
            {
                if (cell.cell.piecePlaced)
                {
                    int beforeIndex = spawnRow == 1 ? 0 : 7;

                    Cell beforeCell = Board.instance.GetCell(cell.cell.position.x, beforeIndex);

                    if (beforeCell && !beforeCell.piecePlaced)
                    {
                        Piece currentPiece = Instantiate(pawn).GetComponent<Piece>();

                        currentPiece.boardPosition = beforeCell;
                        currentPiece.MoveToCell(beforeCell
);
                        currentPiece.teamNumber = playerNumber;
                        currentPiece.player = this;

                        pawns.Add(currentPiece);
                    }
                }
                else
                {
                    Piece currentPiece = Instantiate(pawn).GetComponent<Piece>();

                    currentPiece.boardPosition = cell.cell;
                    currentPiece.MoveToCell(cell.cell);
                    currentPiece.teamNumber = playerNumber;
                    currentPiece.player = this;

                    pawns.Add(currentPiece);
                }

            }
        }
    }

    public int AmountOfPiecesInSpawn()
    {
        int amount = 0;

        foreach (PawnSpawnCell cell in spawCells)
        {
            if (cell.cell.piecePlaced && cell.cell.piecePlaced.teamNumber == playerNumber) amount++;
        }

        return amount;
    }
}
