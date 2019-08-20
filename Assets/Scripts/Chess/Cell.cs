using Assets.Scripts.Chess.Pieces;
using UnityEngine;

namespace Assets.Scripts.Chess
{
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
            Void,
        }

        public CellType type;

        public Vector2Int position;
        public Piece piecePlaced;

        public SpriteRenderer availableCell;

        [Header("Portal")]
        public Cell connectedPortal;
        public Vector2Int portalDirection;
        public float health = 1;
        public bool unlocked = false;

        public void SetBoard()
        {
            SetPosition(new Vector2(transform.position.x, transform.position.z));
            Board.instance.board[position.x, position.y] = this;
        }

        public void SetPosition(Vector2 position)
        {
            this.position = new Vector2Int(Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y));
            transform.position = new Vector3(position.x, 0, position.y);
        }

        public void ShowAvailable(bool available)
        {
            availableCell.enabled = available;

            if (Board.instance.validPositions.Contains(this) && !available) Board.instance.validPositions.Remove(this);
            if (!Board.instance.validPositions.Contains(this) && available) Board.instance.validPositions.Add(this);
        }
    }
}
