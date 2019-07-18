using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Cell : MonoBehaviour
{
    public enum CellType
    {
        Normal,
        Jumper,
        Portal,
        DestructibleWall,
        IndestructibleWall,
        Storm,
    }


    public CellType type;

    public Vector2Int position;
    public Piece piecePlaced;

    public SpriteRenderer availableCell;

    public void SetPosition(Vector2 position)
    {
        this.position = new Vector2Int((int)position.x, (int)position.y);
        transform.position = new Vector3(position.x, 0, position.y);
    }

    public void ShowAvailable(bool available)
    {
        availableCell.enabled = available;

        if (Board.instance.validPositions.Contains(this) && !available) Board.instance.validPositions.Remove(this);
        if (!Board.instance.validPositions.Contains(this) && available) Board.instance.validPositions.Add(this);
    }
}
