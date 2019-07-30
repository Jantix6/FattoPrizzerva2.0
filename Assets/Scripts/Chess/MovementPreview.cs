using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Chess.Pieces;
using UnityEngine;

public class MovementPreview : MonoBehaviour
{
    private IActionHandler _actionHandler;

    [SerializeField] private Piece movementExecutor;
    [SerializeField] private Piece selectedPiece;

    private bool moving = false;

    private void Start()
    {
        _actionHandler = GetComponent<IActionHandler>();
    }

    private void MoveDummy(Cell cell)
    {
        if (!selectedPiece || moving) return;

        movementExecutor.ShowPossibleMoves(false);

        PieceAction actionToDo = null;

        switch (cell.type)
        {
            case Cell.CellType.Normal:
                actionToDo = new MovementAction(cell, 0, 5, movementExecutor);
                break;

            case Cell.CellType.Portal:
                actionToDo = new TeleportAction(cell as Portal, 0, 5, movementExecutor);
                break;
        }

        if (actionToDo == null) return;

        StartCoroutine(actionToDo.DoAction(OnMovedDummy));

        moving = true;
    }

    private void OnMovedDummy()
    {
        moving = false;
        movementExecutor.GetPossibleMoves();
        movementExecutor.ShowPossibleMoves(true);
    }

    public void Move()
    {
        if (!movementExecutor) return;

        var selectedCell = PieceSelector.GetFromRay<Cell>("Cell");

        if (selectedCell == null) return;

        for (var i = 0; i < movementExecutor.MovePositions.Count; i++)
        {
            if (selectedCell.position != movementExecutor.MovePositions[i].position) continue;

            var cellToGo = movementExecutor.MovePositions[i];

            movementExecutor.player.movements -= movementExecutor.CalculateCost(cellToGo);

            MoveDummy(cellToGo);
            break;
        }
    }

    public void Select(Piece piece)
    {
        if (movementExecutor) return;

        selectedPiece = piece ? piece : null;
        movementExecutor = piece ? CreateMovementExecutor(piece) : null;

        if (!movementExecutor) return;

        movementExecutor.GetPossibleMoves();
        movementExecutor.ShowPossibleMoves(true);
    }

    private Piece CreateMovementExecutor(Piece original)
    {
        var clone = Instantiate(original.gameObject).GetComponent<Piece>();

        clone.MoveToCell(original.boardPosition);

        return clone;
    }
}
