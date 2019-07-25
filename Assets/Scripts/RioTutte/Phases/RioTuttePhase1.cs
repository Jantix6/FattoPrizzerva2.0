using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RioTuttePhase1 : MonoBehaviour
{
    public enum State { MOVING, KNOCKBACK }; //dash provisional
    public State currentState = State.MOVING;
    private RioTutteMainScript mainScript;
    private StateMachine stateMachine;
    public BaseState moving;
    public BaseState knockback;
    public float damageImpact = 10;
    public float timeImpact = 0.2f;
    public float damageMinToMove = 15;

    public void StartExecution()
    {
        mainScript = GetComponent<RioTutteMainScript>();
        stateMachine = mainScript.GetStateMachine();
        ChangeState(State.MOVING);


        mainScript.SetDamageMin(damageMinToMove);


    }

    public void Execute()
    {
        stateMachine.ExecuteState();
        switch (mainScript.GetTypeOfDamage())
        {
            case EnemieBasic.TypeOfDamage.PLAYEREMPUJADO:
                break;
            case EnemieBasic.TypeOfDamage.PLAYERREBOTA:
                mainScript.GetPlayer().StartKnockBack(damageImpact, timeImpact,
                    (mainScript.GetPlayer().transform.position - gameObject.transform.position).normalized);
                mainScript.RerstartTypeOfDamage();
                break;
            case EnemieBasic.TypeOfDamage.NADA:
                break;
            case EnemieBasic.TypeOfDamage.EMPUJAAMBOS:
                break;
            case EnemieBasic.TypeOfDamage.EMPUJAENEMIGO:
                ChangeState(State.KNOCKBACK);
                mainScript.RerstartTypeOfDamage();
                break;
        }

    }

    public void ChangeState(State _newState)
    {
        switch (_newState)
        {
            case State.MOVING:
                stateMachine.ChangeState(moving);
                break;
            case State.KNOCKBACK:
                stateMachine.ChangeState(knockback);
                break;

        }
        currentState = _newState;
    }

}
