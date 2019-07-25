using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Piece : MonoBehaviour
{
    protected Board board;

    public int healthPoints;

    public bool AI_Controlled;
    public bool selected;
    public bool Moved = false;

    public Cell boardPosition;
    public List<Cell> MovePositions;
    public List<Cell> PortalPassedPositions;

    [Header("Decision Making ||DO NOT ASSIGN||")]

    public Piece targetPiece = null;
    public Cell targetCell = null;
    public Vector2Int direction = Vector2Int.zero;
    public int cost = 0;

    public void Selected(bool selected)
    {
        GetComponent<MeshRenderer>().material.color = selected ? Color.red : Color.grey;

        this.selected = selected;

        GetPossibleMoves();
        ShowPossibleMoves(selected);
    }

    public void GetBoardPosition()
    {
        boardPosition = Board.instance.GetCell((int)transform.position.x, (int)transform.position.z);
        if (boardPosition) boardPosition.piecePlaced = this;
    }

    public void GetPushed(Vector2Int direction, int forceAmount)
    {
        Vector2Int pushedPosition = new Vector2Int(boardPosition.position.x + (direction.x * forceAmount),
                                                   boardPosition.position.y + (direction.y * forceAmount));

        if (!board.ValidIndex(pushedPosition)) return;

        MoveToCell(board.GetCell(pushedPosition.x, pushedPosition.y));
    }

    public void PushPiece(int cost, Piece pieceToPush, Vector2Int direction)
    {
        IHealth pieceHealth = pieceToPush.GetComponent<IHealth>();

        cost = Mathf.Clamp(cost, 0, 5);

        switch (cost)
        {
            case 2:
                pieceToPush.GetPushed(direction, 1);
                break;
            case 3:
                pieceToPush.GetPushed(direction, 1);
                pieceHealth.GetDamage(0.5f);
                break;

            case 4:
                pieceToPush.GetPushed(direction, 2);
                pieceHealth.GetDamage(0.5f);
                break;

            case 5:
                pieceHealth.GetDamage(1);
                break;
        }

    }

    public abstract void Move(Cell cell);
    public abstract void MoveToCell(Cell cell);
    public abstract void ShowPossibleMoves(bool show);
    public abstract void GetPossibleMoves();

    public abstract int CalculateCost(Cell nextPosition);
}
