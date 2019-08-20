using Assets.Scripts.Chess.PieceActions;
using Assets.Scripts.Chess.Pieces;
using UnityEngine;

namespace Assets.Scripts.Chess
{
    public class MovementPreview : MonoBehaviour
    {
        [SerializeField] private Piece movementExecutor;
        [SerializeField] private Piece selectedPiece;

        [SerializeField] private GameObject UI_Panel;
        [SerializeField] private ChessCamera mainCamera;

        [SerializeField] private Material dummyMaterial;

        private bool moving;
        private int accumulatedCost;

        public bool previewing;

        private Cell OnMouseCell;

        public Piece SelectedPiece { get => selectedPiece; set => selectedPiece = value; }

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

            if (temp && temp != selectedPiece.boardPosition)
            {
                if (OnMouseCell)
                {
                    OnMouseCell.availableCell.color = Color.white;
                    OnMouseCell.availableCell.enabled = false;
                }

                OnMouseCell = temp;

                Color colorToChange = movementExecutor.MovePositions.Contains(OnMouseCell) ? Color.green : Color.red;

                OnMouseCell.availableCell.enabled = true;
                OnMouseCell.availableCell.color = colorToChange;
            }
            else if (OnMouseCell)
            {
                OnMouseCell.availableCell.enabled = false;
                OnMouseCell.availableCell.color = Color.white;
            }
        }

        private void StartPreview()
        {
            selectedPiece.ActionHandler.Actions.Clear();
            selectedPiece.boardPosition.availableCell.enabled = true;
            selectedPiece.boardPosition.availableCell.color = Color.blue;

            mainCamera.ChangeState(ChessCamera.State.FOLLOWING);

            movementExecutor.GetPossibleMoves(true);

            previewing = true;
            UI_Panel.SetActive(true);
        }

        private void ExecutePreview()
        {
            moving = false;

            if (movementExecutor.boardPosition.type == Cell.CellType.Void) ExitPreview();
            else movementExecutor.GetPossibleMoves(movementExecutor.omnidirectional);
        }

        public void ExitPreview()
        {
            Destroy(movementExecutor.gameObject);

            selectedPiece.boardPosition.availableCell.enabled = false;
            selectedPiece.ActionHandler.ExecuteActions();
            selectedPiece.Moved = true;

            mainCamera.ChangeState(ChessCamera.State.LOCKED);

            if (selectedPiece is Pawn) PieceSelector.instance.EndTurn();

            selectedPiece = null;
            movementExecutor = null;
            moving = false;
            previewing = false;

            if (OnMouseCell) OnMouseCell.availableCell.enabled = false;

            UI_Panel.SetActive(false);
        }

        public void CancelPreview()
        {
            Destroy(movementExecutor.gameObject);
            movementExecutor = null;

            selectedPiece.boardPosition.availableCell.enabled = false;
            selectedPiece.player.movements += accumulatedCost;
            accumulatedCost = 0;
            selectedPiece = null;

            moving = false;
            previewing = false;
        }

        private void MoveDummy(Cell cell)
        {
            if (!selectedPiece || moving) return;
            IPieceAction actionToDo = null;

            bool lastAction = false;

            if (selectedPiece is Bishop) actionToDo = ActionToDo(cell, ref lastAction);
            if (selectedPiece is Pawn)
            {
                actionToDo = ActionToDo(cell);
                lastAction = true;
            }

            if (actionToDo == null) return;

            StartCoroutine(lastAction ? actionToDo.DoAction(ExitPreview) : actionToDo.DoAction(ExecutePreview));

            moving = true;
        }

        private IPieceAction ActionToDo(Cell cell, ref bool lastAction)
        {
            IPieceAction actionToDo = null;

            if (cell.piecePlaced)
            {
                actionToDo = new MovementAction(cell, 0, Mathf.Infinity, movementExecutor);
                selectedPiece.ActionHandler.Actions.Add(new PushAction(cell.piecePlaced, 5, selectedPiece));
                lastAction = true;
            }
            else
            {
                switch (cell.type)
                {
                    case Cell.CellType.Normal:
                        actionToDo = new MovementAction(cell, 0, Mathf.Infinity, movementExecutor);
                        selectedPiece.ActionHandler.Actions.Add(new MovementAction(cell, 0, 5, selectedPiece));
  
                        if (!selectedPiece.omnidirectional) lastAction = true;
                        break;

                    case Cell.CellType.Portal:
                        actionToDo = new TeleportAction(cell, 0, Mathf.Infinity, movementExecutor);
                        selectedPiece.ActionHandler.Actions.Add(new TeleportAction(cell, 0, 5, selectedPiece));
                        break;

                    case Cell.CellType.Jumper:
                        actionToDo = new JumpAction(cell, Mathf.Infinity, movementExecutor);
                        selectedPiece.ActionHandler.Actions.Add(new JumpAction(cell, 5, selectedPiece));
                        break;

                    case Cell.CellType.DestructibleWall:
                        int cost = movementExecutor.CalculateCost(cell);

                        actionToDo = new DestroyAction(cell, cost, Mathf.Infinity, movementExecutor);
                        selectedPiece.ActionHandler.Actions.Add(new DestroyAction(cell, cost, 5, selectedPiece));
                        lastAction = true;
                        break;
                }
            }

            return actionToDo;
        }

        private IPieceAction ActionToDo(Cell cell)
        {
            IPieceAction actionToDo = null;

            if (cell.piecePlaced)
            {
                actionToDo = new MovementAction(cell, 0, Mathf.Infinity, movementExecutor);
                selectedPiece.ActionHandler.Actions.Add(new KillPawn(cell.piecePlaced as Pawn, 5, selectedPiece));
            }
            else
            {
                actionToDo = new MovementAction(cell, 0, Mathf.Infinity, movementExecutor);
                selectedPiece.ActionHandler.Actions.Add(new MovementAction(cell, 0, 5, selectedPiece));
            }

            return actionToDo;
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

        public Piece CreateDummy(Piece original)
        {
            var clone = Instantiate(original.gameObject).GetComponent<Piece>();

            clone.dummy = true;
            clone.MoveToCell(original.boardPosition);
            clone.GetComponentInChildren<Renderer>().material = dummyMaterial;

            return clone;
        }
    }
}
