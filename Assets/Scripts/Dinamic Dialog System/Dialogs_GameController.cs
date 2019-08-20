using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialogues;

public class Dialogs_GameController : MonoBehaviour
{
    public List<ICongelable> freazables_lst;    
    bool isGameFrozen;

    // debug
    private KeyCode freeze_Key = KeyCode.Space;

    private void Awake()
    {
        isGameFrozen = false;
    }


    private void Update()
    {
        if (Input.GetKeyUp(freeze_Key))
        {
            if (isGameFrozen)
                UnFreezeGame();
            else
                FreezeGame();
        }


    }
    public void AddToFreazablesList(ICongelable _item)
    {
        if (freazables_lst == null)
            freazables_lst = new List<ICongelable>();

        freazables_lst.Add(_item);
    }
    public void RemoveFromFreazables(ICongelable _itemToRemove)
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
            foreach (ICongelable freazable in freazables_lst)
            {
                freazable.Congelar();
            }
            isGameFrozen = true;
        }
        else
        {
            throw new MissingReferenceException("La lista de congelables esta vacia o es nula");
        }

    }
    public void UnFreezeGame()
    {
        if (freazables_lst != null && freazables_lst.Count != 9)
        {
            foreach (ICongelable freazable in freazables_lst)
            {
                freazable.Descongelar();
            }
            isGameFrozen = false;
        }
        else
        {
            throw new MissingReferenceException("La lista de congelables esta vacia o es nula");
        }
    }


}
