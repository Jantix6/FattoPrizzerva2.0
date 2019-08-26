using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tasks;
using Dialogues;

public class TestPlayer : MonoBehaviour , ICongelable
{
    public Dialogs_GameController gameController;

    TasksManager tasksManager;
    public LocalDialogController dialogController;
    public int health;
    public int maxHealth;


    private void Start()
    {
        tasksManager = TasksManager.Instance;

        maxHealth = health = 100;    
    }

    private void Awake()
    {
        // gameController.AddToFreazablesList(this);
    }

    

    public void TakeDamage(int _damage)
    {
        health -= _damage;

        if (health <= 0)
            Debug.LogError("You are dead");
        else
            Debug.LogError("health = " + health);
    }
    public void TakeDamage(float _damagePerCent)
    {
        if (_damagePerCent < 0 || _damagePerCent > 1)
        {
            return;
        }

        int pureDamage;
        pureDamage = (int)(maxHealth * _damagePerCent);

        TakeDamage(pureDamage);

    }

    public void Congelar(bool _anim = false)
    {
        // Do whatever you need to do to be frozen
        Debug.Log(this.gameObject.name + " is now frozen ");
    }

    public void Descongelar(bool _anim = false)
    {
        // Do whatever you need to do to UNFREEZE
        Debug.Log(this.gameObject.name + " is now unfrozen ");
    }
}
