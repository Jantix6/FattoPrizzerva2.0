using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Chess;
using UnityEngine;

public class PawnSpawnCell : MonoBehaviour
{
    public Cell cell;
    public int team;
    public bool spawnable;

    public void Start()
    {
        cell = GetComponent<Cell>();
    }
}
