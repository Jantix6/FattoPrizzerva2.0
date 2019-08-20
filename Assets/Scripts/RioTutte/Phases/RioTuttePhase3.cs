using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RioTuttePhase3 : MonoBehaviour
{
    public enum State { MOVING, KNOCKBACK, ULTRADASH, STUNED, MOVINGINVERSE }; //dash provisional
    public State currentState = State.MOVING;
    private RioTutteMainScript mainScript;
    private StateMachine stateMachine;
    public BaseState moving;
    public BaseState knockback;
    public BaseState ultraDash;
    public BaseState stun;
    public float damageImpact = 15;
    public float timeImpact = 1.5f;
    public float damageMinToMove = 40;
    private float timeStun = 3f;
    private float currentTimeState = 0;
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
                if (currentState != State.ULTRADASH && currentState != State.STUNED)
                {
                    ChangeState(State.KNOCKBACK);
                    CheckLifes(-1);
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

        if (currentDash >= numOfDashes)
        {
            currentDash = 0;
            currentTimeDash = 0;
        }
        else if (currentDash != 0 && timeBetweenDash != 0 && currentState == State.MOVING &&
            mainScript.GetPlayer().currentState != PlayerScript.State.PLANNING)
        {

            currentTimeDash += Time.deltaTime;
            if (currentTimeDash >= timeBetweenDash)
            {
                currentTimeDash = 0;
                ChangeState(State.ULTRADASH);
            }
        }
        else if (currentState == State.MOVING && mainScript.GetPlayer().currentState != PlayerScript.State.PLANNING)
        {
            currentTimeMovingToDash += Time.deltaTime;
            if (currentTimeMovingToDash >= 3.5f)
                ChangeState(State.ULTRADASH);

        }

        if (currentState == State.MOVINGINVERSE)
        {
            currentTimeState += Time.deltaTime;
            if (currentTimeState > 1.5f)
            {
                currentTimeState = 0;
                ChangeState(State.MOVING);
            }
        }
        else if (mainScript.GetPlayer().currentState == PlayerScript.State.PLANNING && currentState != State.MOVINGINVERSE)
            ChangeState(State.MOVINGINVERSE);
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
            case State.ULTRADASH:
                currentDash++;
                stateMachine.ChangeState(ultraDash);

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
            if (mainScript.phase == 3)
            {
                {
                    if (currentState == State.KNOCKBACK)
                    {
                        if (hit.gameObject.tag == "ObstacleRioTutte" && hit.gameObject.GetComponent<ObstacleInstaKillRioTutte>() != null)
                        {

                            gameObject.SetActive(false);
                            print("Muerto");
                            CheckLifes(-1);

                        }
                        else if (hit.gameObject.tag == "ObstacleRioTutte" && hit.gameObject.GetComponent<ObstaclePhase2>() != null)
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
}
