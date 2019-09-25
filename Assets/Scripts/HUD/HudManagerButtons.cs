using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudManagerButtons : MonoBehaviour
{
    public List<HudButtonAction> huds;

    public void ChangeHud(HudButtonAction.State _newState)
    {
        foreach(HudButtonAction hud in huds)
        {
            hud.ChangeState(_newState);
        }
    }
}
