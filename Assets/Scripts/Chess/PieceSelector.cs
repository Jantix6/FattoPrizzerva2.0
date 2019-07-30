using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceSelector : MonoBehaviour
{
    public ChessPlayer player;

    public MovementPreview previewMovement;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) Select();
        if (Input.GetMouseButtonDown(1)) previewMovement.SelectPositionToMove();

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

    private void Select()
    {
        Piece piece = GetFromRay<Piece>("Piece");

        if (!piece) return;
        if (piece.teamNumber != player.playerNumber) return;
        if (piece.Moved) return;

        previewMovement.Select(piece);
    }
}