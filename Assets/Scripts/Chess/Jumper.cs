using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : Cell
{

    public void Jump(Piece piece)
    {
        int jumpRange = piece.cost <= 4 ? piece.cost / 2 : (piece.cost / 4) + 2;

        Cell destination = FindJumpDestination(piece, piece.direction.x, piece.direction.y, jumpRange);


    }

    private Cell FindJumpDestination(Piece piece, int xDirection, int yDirection, int range)
    {
        bool IsValid = true;

        List<Cell> positions = new List<Cell>();

        Cell currentCell = this;

        int iteration = 0;

        while (iteration <= range && IsValid)
        {
            iteration++;

            int x = position.x + (xDirection * iteration);
            int y = position.y + (yDirection * iteration);

            currentCell = Board.instance.GetCell(x, y);

            IsValid = currentCell;

            if (currentCell.type == CellType.IndestructibleWall) break;
        }

        return currentCell;
    }
}
