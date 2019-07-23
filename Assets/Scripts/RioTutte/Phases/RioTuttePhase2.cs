using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RioTuttePhase2 : MonoBehaviour
{
    public enum State { MOVING, KNOCKBACK, DASH, STUNED }; //dash provisional
    public State currentState = State.MOVING;
    private RioTutteMainScript mainScript;
    private StateMachine stateMachine;
    public BaseState moving;
    public BaseState knockback;
    public BaseState dash;
    public float damageImpact = 10;
    public float timeImpact = 0.2f;
    public float damageMinToMove = 15;
    private float timeStun = 3f;
    public int numOfDashes = 2;
    public int currentDash = 0;
    public float timeBetweenDash = 1f;
    public float currentTimeDash = 0;
    private float currentTimeMovingToDash = 0;
    public float timesStunedToChangePhase = 5;

    public void StartExecution()
    {
        mainScript = GetComponent<RioTutteMainScript>();
        stateMachine = mainScript.GetStateMachine();
        ChangeState(State.MOVING);


        mainScript.SetDamageMin(damageMinToMove);


    }

    public void Execute()
    {
        if (mainScript.GetPlayer().currentState != PlayerScript.State.INSIDEPLANT || currentState == State.KNOCKBACK)
        {
            stateMachine.ExecuteState();
        }

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
                if (currentState != State.DASH)
                {
                    ChangeState(State.KNOCKBACK);
                }
                mainScript.RerstartTypeOfDamage();
                break;

        }
        if (currentDash >= numOfDashes)
        {
            currentDash = 0;
            currentTimeDash = 0;
        }
        else if (currentDash != 0 && timeBetweenDash != 0 && currentState == State.MOVING)
        {

            currentTimeDash += Time.deltaTime;
            if (currentTimeDash >= timeBetweenDash)
            {
                currentTimeDash = 0;
                ChangeState(State.DASH);
            }
        }
        else if(currentState == State.MOVING)
        {
            currentTimeMovingToDash += Time.deltaTime;
            if (currentTimeMovingToDash >= 2)
                ChangeState(State.DASH);

        }

    }

    public void ChangeState(State _newState)
    {
        switch (_newState)
        {
            case State.MOVING:
                stateMachine.ChangeState(moving);
                currentTimeMovingToDash = 0;
                break;
            case State.KNOCKBACK:
                stateMachine.ChangeState(knockback);
                currentDash = 0;
                currentTimeDash = 0;
                currentTimeMovingToDash = 0;
                break;
            case State.DASH:
                currentDash++;
                stateMachine.ChangeState(dash);

                break;
            case State.STUNED:
                currentDash = 0;
                currentTimeDash = 0;
                currentTimeMovingToDash = 0;
                break;

        }
        currentState = _newState;
    }

    public float GetTimeStun()
    {
        return timeStun;
    }
}
