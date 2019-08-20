using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Chess;
using UnityEngine;
using UnityEngine.UI;

public class PawnPlayer : ChessPlayer
{
    public int selectedNumber;
    public int selectCellOne = -1;
    public int selectCellTwo = -1;

    public bool deletedCells;

    public int spawnRow = 1;

    public GameObject selectNumberUI;

    public new void Update()
    {
        if (!deletedCells)
        {
            turn = 0;
        }
    }

    public new void StartTurn()
    {
        if (deletedCells) turn++;
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

        if (selectedCellOne > 8) selectedCellOne -= 8;
        if (selectedCellTwo > 8) selectedCellTwo -= 8;

        Cell cellOne = Board.instance.GetCell(selectedCellOne, spawnRow);
        Cell cellTwo = Board.instance.GetCell(selectedCellTwo, spawnRow);

        var player = PieceSelector.instance.player as PawnPlayer;

        player.deletedCells = true;

    }

    public void SelectNumber(int number)
    {
        selectedNumber = number;
        selectNumberUI.SetActive(false);

      //  PieceSelector.instance.EndTurn();
    }

    public void SelectCellNumber(int number)
    {
        if (selectCellOne != -1)
        {
            if (selectCellTwo != -1) selectCellTwo = number;
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
}
