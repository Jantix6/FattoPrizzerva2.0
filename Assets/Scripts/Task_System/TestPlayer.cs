using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tasks;
using Dialogues;

public class TestPlayer : MonoBehaviour
{
    
    TasksManager tasksManager;
    public LocalDialogController dialogController;
    int health;


    private void Start()
    {
        tasksManager = TasksManager.GetInstance();

        health = 100;


        
    }



    public void TakeDamage(int _damage)
    {
        health -= _damage;

        if (health <= 0)
            Debug.LogError("You are dead");
        else
            Debug.LogError("health = " + health);

    }
    



}
