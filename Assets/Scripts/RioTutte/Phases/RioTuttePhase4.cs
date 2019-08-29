using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RioTuttePhase4 : MonoBehaviour
{
    public enum State { MOVING, KNOCKBACK, STUNED, MOVINGINVERSE, FIRSTTELEPORT, IDLECANSADO }; //dash provisional
    public State currentState = State.MOVING;
    private RioTutteMainScript mainScript;
    private StateMachine stateMachine;
    public BaseState moving;
    public BaseState knockback;
    public BaseState teleport;
    public BaseState stun;
    public BaseState firstTeleport;
    public BaseState idleCansado;
    public float damageImpact = 15;
    public float timeImpact = 0.5f;
    public float damageMinToMove = 40;
    private float timeStun = 3f;
    public float currentTimeState = 0;
    public int numOfDashes = 3;
    public int currentDash = 0;
    public float timeBetweenDash = 0.75f;
    public float currentTimeDash = 0;
    public float timesStunedToChangePhase = 1;
    public bool knockbackPlant = false;
    private float lifes = 5;
    private bool started = true;
    private bool firstTime = false;
    public GameObject wall;
    public LayerMask layerMask;

    [Header("Wall Raycast")]
    [SerializeField] private WallInstantiatedObject objectToInstantateOnRayImpactPrefab;
    [SerializeField] private bool showDistanceLines;


    public void StartExecution()
    {

        //Escena de pararse

        mainScript = GetComponent<RioTutteMainScript>();
        mainScript.speed = 1f;
        lifes = timesStunedToChangePhase;
        started = true;
        stateMachine = mainScript.GetStateMachine();
        ChangeState(State.MOVING);

        RaycastHit rayhit;
        if (Physics.Raycast(transform.position, (mainScript.GetPlayer().transform.position - gameObject.transform.position).normalized,
            out rayhit, 200, layerMask))
        {
            wall = rayhit.collider.gameObject;

            if (objectToInstantateOnRayImpactPrefab)
            {
                WallInstantiatedObject objectToInstantiateOnRayImpact;
                objectToInstantiateOnRayImpact = Instantiate(objectToInstantateOnRayImpactPrefab, rayhit.point, Quaternion.identity);
                objectToInstantiateOnRayImpact.Initialize(mainScript.GetPlayer().transform, teleport,showDistanceLines);
            }
        }
        else
            print("NULL");


        mainScript.SetDamageMin(damageMinToMove);
        mainScript.GetPlayer().StartKnockBack(0, 200, Vector3.zero, false);
        mainScript.GetPlayer().SetCanDamage(false);

    }

    public void Execute()
    {
        if (!started)
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
                    ChangeState(State.KNOCKBACK);
                    mainScript.RerstartTypeOfDamage();
                    break;
                case EnemieBasic.TypeOfDamage.EMPUJADOCONCONDICIÓN:
                    ChangeState(State.KNOCKBACK);
                    mainScript.RerstartTypeOfDamage();
                    break;

            }
        }
        else
        {
            stateMachine.ExecuteState();
            if (firstTime)
            {
                Vector3 dir = (mainScript.GetPlayer().transform.position - gameObject.transform.position).normalized;
                RaycastHit rayhit;
                if (Physics.Raycast(mainScript.GetPlayer().transform.position, dir, out rayhit, 200, layerMask)
                    && (mainScript.GetPlayer().currentState == PlayerScript.State.MOVING || mainScript.GetPlayer().currentState == PlayerScript.State.RUNING))
                {
                    if (rayhit.collider.gameObject != wall)
                    {
                        ChangeState(State.FIRSTTELEPORT);
                        Debug.DrawLine(mainScript.GetPlayer().transform.position, rayhit.point, Color.red);
                    }
                        
                }
            }


            if (!firstTime && (gameObject.transform.position - mainScript.GetPlayer().transform.position).magnitude <= 1f)
            {
                if (mainScript.GetPlayer().currentState == PlayerScript.State.KNOCKBACK)
                {
                    mainScript.GetPlayer().StopKnockBack();
                    firstTime = true;
                    //Escena de pararse
                }
            }

            if (mainScript.GetPlayer().GetImpact())
            {
                started = false;
                mainScript.GetPlayer().SetCanDamage(true);
                ChangeState(State.IDLECANSADO);
                mainScript.GetPlayer().StartAdrenalina(true);
            }

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


    }

    public void ChangeState(State _newState)
    {

        switch (_newState)
        {
            case State.MOVING:
                mainScript.SetDamageMin(damageMinToMove);
                if (!started)
                {
                    currentTimeDash++;
                    mainScript.speed = 3;

                    if (currentState == State.IDLECANSADO)
                        mainScript.speed = 1f;
                    else
                        ChangeState(State.IDLECANSADO);
                }
                if (currentTimeDash < 1 || currentState == State.IDLECANSADO)
                    stateMachine.ChangeState(moving);
                break;
            case State.KNOCKBACK:
                currentDash = 0;
                currentTimeDash = 0;
                stateMachine.ChangeState(knockback);
                break;

                break;
            case State.STUNED:
                mainScript.SetDamageMin(0);
                currentDash = 0;
                currentTimeDash = 0;
                stateMachine.ChangeState(stun);
                break;
            case State.MOVINGINVERSE:
                mainScript.SetDamageMin(damageMinToMove);
                mainScript.speed = 0.75f;

                stateMachine.ChangeState(moving);
                break;

            case State.FIRSTTELEPORT:
                stateMachine.ChangeState(firstTeleport);
                break;

            case State.IDLECANSADO:
                stateMachine.ChangeState(idleCansado);

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
            if (mainScript.phase == 4)
            {
                {
                    if (currentState == State.KNOCKBACK)
                    {
                        if (hit.gameObject.tag == "ObstacleRioTutte" && hit.gameObject.GetComponent<ObstacleInstaKillRioTutte>() != null)
                        {

                            ChangeState(State.STUNED);
                            gameObject.SetActive(false);
                            CheckLifes(-1);

                        }
                    }
                }
            }
        }
    }
}
