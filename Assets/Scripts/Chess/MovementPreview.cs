using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Assets.Scripts.Chess.Pieces;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class MovementPreview : MonoBehaviour
{
    [SerializeField] private Piece movementExecutor;
    [SerializeField] private Piece selectedPiece;

    [SerializeField] private GameObject UI_Panel;

    private bool moving;
    private int accumulatedCost;

    public bool previewing;

    /// <summary>
    /// TEST
    /// </summary>

    private Cell OnMouseCell;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) ExitPreview();
    }

    private void FixedUpdate()
    {
        if (previewing && !moving) ShowCells();
    }

    private void ShowCells()
    {
        if (Input.GetAxis("Mouse X") == 0 && Input.GetAxis("Mouse Y") == 0) return;

        var temp = PieceSelector.GetFromRay<Cell>("Cell");

        if (temp)
        {
            if (OnMouseCell) OnMouseCell.GetComponent<Renderer>().material.color = Color.white;
            OnMouseCell = temp;

            Color colorToChange = movementExecutor.MovePositions.Contains(OnMouseCell) ? Color.green : Color.red;

            OnMouseCell.GetComponent<Renderer>().material.color = colorToChange;
        }
        else if (OnMouseCell)
        {
            OnMouseCell.GetComponent<Renderer>().material.color = Color.white;
        }
    }

    private void StartPreview()
    {
        selectedPiece.actionHandler.Actions.Clear();

        movementExecutor.GetPossibleMoves(true);

        previewing = true;
        UI_Panel.SetActive(true);
    }

    private void ExecutePreview()
    {
        moving = false;

        movementExecutor.GetPossibleMoves(movementExecutor.canChangeDirection);
    }

    public void ExitPreview()
    {
        Destroy(movementExecutor.gameObject);

        selectedPiece.actionHandler.ExecuteActions();
        selectedPiece.Moved = true;

        selectedPiece = null;
        movementExecutor = null;

        moving = false;
        previewing = false;
    }

    public void CancelPreview()
    {
        Destroy(movementExecutor.gameObject);
        movementExecutor = null;

        selectedPiece.player.movements += accumulatedCost;
        selectedPiece = null;

        moving = false;
        previewing = false;
    }

    private void MoveDummy(Cell cell)
    {
        if (!selectedPiece || moving) return;

        PieceAction actionToDo = null;

        bool lastAction = false;

        if (cell.piecePlaced)
        {
            actionToDo = new MovementAction(cell, 0, Mathf.Infinity, movementExecutor);
            selectedPiece.actionHandler.Actions.Add(new PushAction(cell.piecePlaced, 5, selectedPiece));
            lastAction = true;
        }
        else
        {
            switch (cell.type)
            {
                case Cell.CellType.Normal:
                    actionToDo = new MovementAction(cell, 0, Mathf.Infinity, movementExecutor);
                    selectedPiece.actionHandler.Actions.Add(new MovementAction(cell, 0, 5, selectedPiece));
                    break;

                case Cell.CellType.Portal:
                    actionToDo = new TeleportAction(cell, 0, Mathf.Infinity, movementExecutor);
                    selectedPiece.actionHandler.Actions.Add(new TeleportAction(cell, 0, 5, selectedPiece));

                    break;
            }
        }

        if (actionToDo == null) return;

        StartCoroutine(lastAction ? actionToDo.DoAction(ExitPreview) : actionToDo.DoAction(ExecutePreview));

        moving = true;
    }

    public void SelectPositionToMove()
    {
        if (!movementExecutor) return;

        var selectedCell = PieceSelector.GetFromRay<Cell>("Cell");

        if (selectedCell == null) return;

        if (movementExecutor.MovePositions.Contains(selectedCell))
        {
            accumulatedCost += movementExecutor.CalculateCost(selectedCell);
            movementExecutor.player.movements -= movementExecutor.CalculateCost(selectedCell);

            MoveDummy(selectedCell);
        }
    }

    public void Select(Piece piece)
    {
        if (movementExecutor) return;

        selectedPiece = piece ? piece : null;
        movementExecutor = piece ? CreateDummy(piece) : null;

        if (movementExecutor && selectedPiece) StartPreview();
    }

    public static Piece CreateDummy(Piece original)
    {
        var clone = Instantiate(original.gameObject).GetComponent<Piece>();

        clone.dummy = true;
        clone.MoveToCell(original.boardPosition);

        return clone;
    }
}
