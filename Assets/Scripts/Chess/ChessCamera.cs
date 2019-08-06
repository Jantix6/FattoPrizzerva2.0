using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessCamera : MonoBehaviour
{
    public enum State { FREE, LOCKED, FOLLOWING };
    public State currentState = State.LOCKED;
    private MovementPreview pieceInMovement;

    public void Awake()
    {
        pieceInMovement = GetComponent<MovementPreview>();
        ChangeState(State.LOCKED);
    }

    public void Update()
    {
        
    }

    public void ChangeState(State _newState)
    {
        switch (_newState)
        {
            case State.FREE:
                break;
            case State.LOCKED:
                break;
            case State.FOLLOWING:
                break;

        }
        currentState = _newState;
    }
}
