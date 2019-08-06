using Assets.Scripts.Chess.Pieces;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Chess
{
    public class PieceSelector : MonoBehaviour
    {
        public ChessPlayer player;

        public MovementPreview previewMovement;

        [SerializeField] private ChessPlayer playerOne;
        [SerializeField] private ChessPlayer playerTwo;

        private Piece[] pieces;

        [SerializeField] private Text playerText;
        [SerializeField] private Text movementText;
        [SerializeField] private Text turnText;

        private void Start()
        {
            SetUpPieces();
            player.StartTurn();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0)) Select();
            if (Input.GetMouseButtonDown(1)) previewMovement.SelectPositionToMove();

            UpdateUI();
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

        private void UpdateUI()
        {
            playerText.text = "Player: " + (player.playerNumber + 1);
            movementText.text = "Moves: " + player.movements;
            turnText.text = "Turn: " + player.turn;
        }

        private void Select()
        {
            Piece piece = GetFromRay<Piece>("Piece");

            if (!piece) return;
            if (piece.teamNumber != player.playerNumber) return;
            if (piece.Moved) return;

            previewMovement.Select(piece);
        }

        private void SetUpPieces()
        {
            pieces = FindObjectsOfType<Piece>();

            foreach (var piece in pieces)
            {
                if (piece.teamNumber == playerOne.playerNumber) piece.player = playerOne;
                if (piece.teamNumber == playerTwo.playerNumber) piece.player = playerTwo;
            }
        }

        public void EndTurn()
        {
            if (previewMovement.previewing) previewMovement.CancelPreview();

            foreach (var piece in pieces)
            {
                if (piece != null) piece.Moved = false;
            }

            if (player.movements == player.maxMovements) player.maxMovements--;
            player = player == playerOne ? playerTwo : playerOne;
            player.StartTurn();
        }
    }
}