using Assets.Scripts.Chess;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessCamera : MonoBehaviour
{
    public enum State { FREE, LOCKED, FOLLOWING };
    public State currentState = State.LOCKED;

    MovementPreview pieceInMovement;
    Vector3 initPos;
    Quaternion initRotation;

    GameObject pieceToFollow;
    private Vector3 offset;

    float movementSpeed = 10f;
    float freeLookSensitivity = 3f;

    public void Awake()
    {
        initPos = gameObject.transform.position;
        initRotation = gameObject.transform.rotation;
        pieceInMovement = GetComponent<MovementPreview>();
    }

    void Start()
    {
        ChangeState(State.LOCKED);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            ChangeState(State.LOCKED);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            ChangeState(State.FREE);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            ChangeState(State.FOLLOWING);

        switch (currentState)
        {
            case State.FREE:
                CheckCameraMovementKeys();
                break;
            case State.LOCKED:
                break;
            case State.FOLLOWING:
                FollowingPiece();
                break;
        }
    }

    private void CheckCameraMovementKeys()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            transform.position = transform.position + (-transform.right * movementSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            transform.position = transform.position + (transform.right * movementSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            transform.position = transform.position + (transform.forward * movementSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            transform.position = transform.position + (-transform.forward * movementSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.Q))
            transform.position = transform.position + (transform.up * movementSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.E))
            transform.position = transform.position + (-transform.up * movementSpeed * Time.deltaTime);


        if (Input.GetKey(KeyCode.R) || Input.GetKey(KeyCode.PageUp))
            transform.position = transform.position + (Vector3.up * movementSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.F) || Input.GetKey(KeyCode.PageDown))
            transform.position = transform.position + (-Vector3.up * movementSpeed * Time.deltaTime);

        
        float newRotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * freeLookSensitivity;
        float newRotationY = transform.localEulerAngles.x - Input.GetAxis("Mouse Y") * freeLookSensitivity;
        transform.localEulerAngles = new Vector3(newRotationY, newRotationX, 0f);        
    }

    private void FollowingPiece()
    {
        transform.position = pieceToFollow.transform.position + offset/3.0f;
    }
    public void ChangeState(State _newState)
    {
        switch (currentState)
        {
            case State.FREE:
                break;
            case State.LOCKED:
                break;
            case State.FOLLOWING:
                pieceToFollow = null;
                break;
        }

        switch (_newState)
        {
            case State.FREE:
                break;
            case State.LOCKED:
                gameObject.transform.position = initPos;
                gameObject.transform.rotation = initRotation;
                break;
            case State.FOLLOWING:
                pieceToFollow = pieceInMovement.SelectedPiece.gameObject;
                offset = transform.position - pieceToFollow.transform.position;
                break;
        }
        currentState = _newState;
    }
}
