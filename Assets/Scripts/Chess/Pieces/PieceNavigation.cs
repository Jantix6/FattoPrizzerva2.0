using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class PieceNavigation : MonoBehaviour
{
    [SerializeField] private float speed;

    public IEnumerator MoveTo(Piece piece, Cell cell, UnityAction MovementDone)
    {
        Vector3 destination = new Vector3(cell.position.x, piece.transform.position.y, cell.position.y);
        Vector3 direction = new Vector3(piece.direction.x, 0, piece.direction.y).normalized;

        while ((piece.transform.position - destination).magnitude > 0.1f)
        {
            piece.transform.position += direction * speed * Time.deltaTime;

            yield return null;
        }

        piece.MoveToCell(cell);

        MovementDone();
    }

    public Vector2Int GetDirection(Cell origin, Cell destination)
    {
        Vector2Int direction = (destination.position - origin.position);

        if (direction.x != 0) direction.x = Math.Sign(direction.x);
        if (direction.y != 0) direction.y = Math.Sign(direction.y);

        //print("Origin: " + origin.position + "  Destination: " + destination.position + "  Direction: " + direction);

        return direction;
    }
}
