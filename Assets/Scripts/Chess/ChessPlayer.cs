using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessPlayer : MonoBehaviour
{
    public int playerNumber = 1;
    public int movements=50;

    public int turn = 0;

    void Awake()
    {
        var pieces = FindObjectsOfType<Piece>();

        foreach (var piece in pieces)
        {
            if (piece.teamNumber == playerNumber) piece.player = this;
        }
    }

    private void Update()
    {
       //if (turn == 0) movements = 1;
    }

}
