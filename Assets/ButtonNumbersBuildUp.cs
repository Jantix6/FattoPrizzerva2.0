using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonNumbersBuildUp : MonoBehaviour
{
    public List<Button> firstButtons;
    public List<Button> secondButtons;
    public PawnPlayer player;

    void Start()
    {
        SetUpFunctions();
    }

    private void SetUpFunctions()
    {
        for (int i = 0; i < firstButtons.Count; i++)
        {
            firstButtons[i].onClick.AddListener(delegate () { player.SelectNumber(i); });
        }

        for (int x = 0; x < secondButtons.Count; x++)
        {
            secondButtons[x].onClick.AddListener(delegate () { player.SelectCellNumber(x); });
            secondButtons[x].onClick.AddListener(delegate () { SetInteractable(secondButtons[x]); });
        }
    }

    private void SetInteractable(Button button)
    {
        button.interactable = false;
    }
}
