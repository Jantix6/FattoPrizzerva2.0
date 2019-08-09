using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RioTuttePhase4 : MonoBehaviour
{
    public enum State { MOVING, KNOCKBACK, TELEPORT, STUNED, MOVINGINVERSE }; //dash provisional
    public State currentState = State.MOVING;
    private RioTutteMainScript mainScript;
    private StateMachine stateMachine;
    public BaseState moving;
    public BaseState knockback;
    public BaseState teleport;
    public BaseState stun;
    public float damageImpact = 15;
    public float timeImpact = 0.5f;
    public float damageMinToMove = 40;
    private float timeStun = 3f;
    public float currentTimeState = 0;
    public int numOfDashes = 3;
    public int currentDash = 0;
    public float timeBetweenDash = 2f;
    public float currentTimeDash = 0;
    private float currentTimeMovingToDash = 0;
    public float timesStunedToChangePhase = 5;
    public bool knockbackPlant = false;
    private float lifes = 5;

    public void StartExecution()
    {
        mainScript = GetComponent<RioTutteMainScript>();
        mainScript.speed = 3f;
        stateMachine = mainScript.GetStateMachine();
        ChangeState(State.MOVING);


        mainScript.SetDamageMin(damageMinToMove);


    }

    public void Execute()
    {
        if (mainScript.GetPlayer().currentState != PlayerScript.State.INSIDEPLANT || currentState == State.KNOCKBACK)
        {
            ExecuteState();
        }

        switch (mainScript.GetTypeOfDamage())
        {
            case EnemieBasic.TypeOfDamage.PLAYEREMPUJADO:
                break;
            case EnemieBasic.TypeOfDamage.PLAYERREBOTA:
                mainScript.GetPlayer().StartKnockBack(damageImpact, timeImpact / 2,
                    (mainScript.GetPlayer().transform.position - gameObject.transform.position).normalized);
                mainScript.RerstartTypeOfDamage();
                break;
            case EnemieBasic.TypeOfDamage.NADA:
                break;
            case EnemieBasic.TypeOfDamage.EMPUJAAMBOS:
                break;
            case EnemieBasic.TypeOfDamage.EMPUJAENEMIGO:
                if (currentState != State.TELEPORT && currentState != State.STUNED)
                {
                    CheckLifes(-1);
                    ChangeState(State.KNOCKBACK);
                }
                else if (currentState != State.STUNED)
                {
                    CheckLifes(-0.25f);
                }
                mainScript.RerstartTypeOfDamage();
                break;
            case EnemieBasic.TypeOfDamage.EMPUJADOCONCONDICIÓN:
                ChangeState(State.KNOCKBACK);
                mainScript.RerstartTypeOfDamage();
                break;

        }


    }

    private void CheckLifes(float _life)
    {
        lifes += _life;
        if (lifes <= 0)
            mainScript.ChangePhase(4);
    }

    private void ExecuteState()
    {
        stateMachine.ExecuteState();


        if (currentState == State.MOVINGINVERSE)
        {
            currentTimeState += Time.deltaTime;
            if (currentTimeState > 0.2f)
            {
                currentTimeState = 0;
                ChangeState(State.MOVING);
            }
        }
        else if (mainScript.GetPlayer().currentState == PlayerScript.State.PLANNING && currentState != State.MOVINGINVERSE)
            ChangeState(State.MOVINGINVERSE);
        
        if(currentState == State.MOVING )
        {
            currentTimeState += Time.deltaTime;
            if(currentTimeState >= 0.75f)
            {
                ChangeState(State.TELEPORT);
            }
        }


    }

    public void ChangeState(State _newState)
    {
        currentState = _newState;

        switch (_newState)
        {
            case State.MOVING:
                mainScript.SetDamageMin(damageMinToMove);
                mainScript.speed = 3;
                currentTimeMovingToDash = 0;
                stateMachine.ChangeState(moving);
                break;
            case State.KNOCKBACK:
                currentDash = 0;
                currentTimeDash = 0;
                currentTimeMovingToDash = 0;
                stateMachine.ChangeState(knockback);
                break;
            case State.TELEPORT:
                currentDash++;
                stateMachine.ChangeState(teleport);

                break;
            case State.STUNED:
                mainScript.SetDamageMin(0);
                currentDash = 0;
                currentTimeDash = 0;
                currentTimeMovingToDash = 0;
                stateMachine.ChangeState(stun);
                break;
            case State.MOVINGINVERSE:
                mainScript.SetDamageMin(damageMinToMove);
                mainScript.speed = 0.75f;
                currentTimeMovingToDash = 0;
                stateMachine.ChangeState(moving);
                break;

        }
    }

    public float GetTimeStun()
    {
        return timeStun;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (mainScript != null)
        {
            if (mainScript.phase == 4)
            {
                {
                    if (currentState == State.KNOCKBACK)
                    {
                        if (hit.gameObject.tag == "ObstacleRioTutte" && hit.gameObject.GetComponent<ObstacleInstaKillRioTutte>() != null)
                        {

                            ChangeState(State.STUNED);
                            print("Muerto");
                            CheckLifes(-1);

                        }
                    }
                }
            }
        }
    }
}
