using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePhase2 : MonoBehaviour
{
    private bool roto = false;

    public void Chocado()
    {
        if (!roto)
        {
            roto = true;
            gameObject.SetActive(false); //provisional
        }
    }

    public bool GetRoto()
    {
        return roto;
    }
}
