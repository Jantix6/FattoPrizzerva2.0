using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessPlayer : MonoBehaviour
{
    public int playerNumber = 1;
    public int movements = 1;
    public int maxMovements = 15;

    public int turn = 0;

    private void Update()
    {
        if (turn == 1 || turn == 2) movements = 1;
    }

    public void StartTurn()
    {
        turn++;

        if (turn == 3 || turn == 4) movements += 3;
        if (turn == 5 || turn == 6) movements += 5;
        if (turn == 7 || turn == 8 || turn == 9) movements += 7;
        if (turn == 10 || turn == 11 || turn == 12) movements += 9;
        if (turn >= 13) movements += 11;

        movements = Mathf.Clamp(movements, 0, maxMovements);
    }
}
