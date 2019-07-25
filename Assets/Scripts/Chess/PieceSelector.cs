using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceSelector : MonoBehaviour
{
    public ChessPlayer player;

    public Piece selectedPiece;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) SelectPiece();
        if (Input.GetMouseButtonDown(1)) MovePiece();
    }

    private void MovePiece()
    {
        if (selectedPiece == null) return;

        Cell selectedCell = GetFromRay<Cell>("Cell");

        if (selectedCell == null) return;

        for (int i = 0; i < selectedPiece.MovePositions.Count; i++)
        {
            if (selectedCell.position != selectedPiece.MovePositions[i].position) continue;

            Cell cellToGo = selectedPiece.MovePositions[i];

            player.movements -= selectedPiece.CalculateCost(cellToGo);
            selectedPiece.Move(cellToGo);


            //Deselect(); //Testing, Uncomment When done
            break;
        }

        for (int i = 0; i < selectedPiece.PortalPassedPositions.Count; i++)
        {
            if (selectedCell.position != selectedPiece.PortalPassedPositions[i].position) continue;

            Cell cellToGo = selectedPiece.PortalPassedPositions[i];

            player.movements -= selectedPiece.CalculateCost(cellToGo);
            selectedPiece.Move(cellToGo);

        }
    }

    public void SelectPiece()
    {
        Deselect();

        selectedPiece = GetFromRay<Piece>("Piece");

        if (!selectedPiece) return;

        if (selectedPiece.AI_Controlled || selectedPiece.Moved) selectedPiece = null;
        else selectedPiece.Selected(true);
    }

    private void Deselect()
    {
        if (selectedPiece) selectedPiece.Selected(false);
        selectedPiece = null;
    }

    public static T GetFromRay<T>(string layerMaskName)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        int layer_mask = LayerMask.GetMask(layerMaskName);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer_mask))
        {
            if (hit.transform.GetComponent<T>() != null)
            {
                return hit.transform.GetComponent<T>();
            }
        }

        return default;
    }
}
