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
    public BaseState stun;
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
    public bool knockbackPlant = false;
    private float lifes = 1;
    private EnemieBasic.TypeOfDamage lastDamage = EnemieBasic.TypeOfDamage.NADA;

    public void StartExecution()
    {
        mainScript = GetComponent<RioTutteMainScript>();
        mainScript.speed = 2f;
        stateMachine = mainScript.GetStateMachine();
        ChangeState(State.MOVING);


        mainScript.SetDamageMin(damageMinToMove);


    }

    public void Execute()
    {
        if (mainScript.GetPlayer().currentState != PlayerScript.State.INSIDEPLANT || currentState == State.KNOCKBACK || currentState == State.DASH)
        {
            ExecuteState();
        }

        switch (mainScript.GetTypeOfDamage())
        {
            case EnemieBasic.TypeOfDamage.PLAYEREMPUJADO:
                break;
            case EnemieBasic.TypeOfDamage.PLAYERREBOTA:
                mainScript.GetPlayer().StartKnockBack(damageImpact, timeImpact,
                    (mainScript.GetPlayer().transform.position - gameObject.transform.position).normalized);
                lastDamage = mainScript.GetTypeOfDamage();
                mainScript.RerstartTypeOfDamage();
                break;
            case EnemieBasic.TypeOfDamage.NADA:
                break;
            case EnemieBasic.TypeOfDamage.EMPUJAAMBOS:
                break;
            case EnemieBasic.TypeOfDamage.EMPUJAENEMIGO:
                if (currentState != State.DASH && currentState != State.STUNED)
                {
                    ChangeState(State.KNOCKBACK);
                }
                else if(currentState != State.STUNED)
                {
                    CheckLifes(-0.25f);
                }
                lastDamage = mainScript.GetTypeOfDamage();
                mainScript.RerstartTypeOfDamage();
                break;
            case EnemieBasic.TypeOfDamage.EMPUJADOCONCONDICIÓN:
                ChangeState(State.KNOCKBACK);
                lastDamage = mainScript.GetTypeOfDamage();
                mainScript.RerstartTypeOfDamage();
                break;

        }


    }

    private void CheckLifes(float _life)
    {
        lifes += _life;
        if (lifes <= 0)
            mainScript.ChangePhase(3);
    }

    private void ExecuteState()
    {
        stateMachine.ExecuteState();

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
        else if (currentState == State.MOVING)
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
                mainScript.SetDamageMin(damageMinToMove);
                currentTimeMovingToDash = 0;
                stateMachine.ChangeState(moving);
                break;
            case State.KNOCKBACK:
                currentDash = 0;
                currentTimeDash = 0;
                currentTimeMovingToDash = 0;
                stateMachine.ChangeState(knockback);
                break;
            case State.DASH:
                currentDash++;
                stateMachine.ChangeState(dash);

                break;
            case State.STUNED:
                mainScript.SetDamageMin(0);
                currentDash = 0;
                currentTimeDash = 0;
                currentTimeMovingToDash = 0;
                stateMachine.ChangeState(stun);
                break;

        }
        currentState = _newState;
    }

    public float GetTimeStun()
    {
        return timeStun;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (mainScript != null)
        {
            if (mainScript.phase == 2)
            {
                if (currentState == State.KNOCKBACK && lastDamage == EnemieBasic.TypeOfDamage.EMPUJADOCONCONDICIÓN)
                {
                    if (hit.gameObject.tag == "ObstacleRioTutte" && hit.gameObject.GetComponent<ObstaclePhase2>() != null)
                    {
                        if (!hit.gameObject.GetComponent<ObstaclePhase2>().GetRoto())
                        {
                            hit.gameObject.GetComponent<ObstaclePhase2>().Chocado();
                            ChangeState(State.STUNED);
                            CheckLifes(-1);
                        }
                    }
                }
            }
        }
    }
}
