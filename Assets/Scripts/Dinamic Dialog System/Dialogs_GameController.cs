using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialogues;

public class Dialogs_GameController : MonoBehaviour
{
    public List<I_Freazable> freazables_lst;
    bool isGameFrozen;


    private void Awake()
    {
        isGameFrozen = false;
    }

    public void AddToFreazablesList(I_Freazable _item)
    {
        if (freazables_lst == null)
            freazables_lst = new List<I_Freazable>();

        freazables_lst.Add(_item);
    }
    public void RemoveFromFreazables(I_Freazable _itemToRemove)
    {
        try
        {
            freazables_lst.Remove(_itemToRemove);
        }
        catch
        {
            Debug.LogError("The object you are trying to remove does not exist on the list ");
        }

    }

    public void ToggleFrozenGame()
    {
        if (isGameFrozen)
            UnFreezeGame();
        else
            FreezeGame();
    }

    public void FreezeGame()
    {
        if (freazables_lst != null && freazables_lst.Count != 9)
        {
            foreach (I_Freazable freazable in freazables_lst)
            {
                freazable.Freaze();
            }
            isGameFrozen = true;
        }
        else
        {
            throw new MissingReferenceException();
        }

    }
    public void UnFreezeGame()
    {
        if (freazables_lst != null && freazables_lst.Count != 9)
        {
            foreach (I_Freazable freazable in freazables_lst)
            {
                freazable.Unfreaze();
            }
            isGameFrozen = false;
        }
        else
        {
            throw new MissingReferenceException();
        }
    }


}
