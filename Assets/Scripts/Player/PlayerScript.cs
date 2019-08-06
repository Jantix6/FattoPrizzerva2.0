using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public enum State
    {
        MOVING, PUNCHING, RUNING, PUNCHRUNNING, KNOCKBACK, FLYINGKICK, HABILITY, INSIDEPLANT,
        JUMPING, PLANNING, AEREOPUNCH, ADRENALINAPUNCH, ADRENALINARUN, ADRENALINAPUNCHRUN, ADRENALINAAEREOPUNCH
    };
    public State currentState = State.MOVING;
    public KeyCode upKey = KeyCode.W;
    public KeyCode downKey = KeyCode.S;
    public KeyCode rightKey = KeyCode.D;
    public KeyCode leftKey = KeyCode.A;
    public KeyCode runKey = KeyCode.LeftShift;
    public KeyCode jumpKey = KeyCode.Space;

    [SerializeField] private LayerMask layerMask;

    [SerializeField] public IEstaminable stamina;
    [SerializeField] public IAdrenalinable adrenalina;

    #region STATES
    public BaseState punch;
    public BaseState moving;
    public BaseState run;
    public BaseState punchRunning;
    public BaseState punchFly;
    public BaseState knockBack;
    public BaseState jumping;
    public BaseState planning;
    public BaseState aereoPunch;
    public BaseState adrenalinaPunch;
    public BaseState adrenalinaRun;
    public BaseState adrenalinaPunchRun;
    public BaseState adrenalinaAereoPunch;

    private PunchScript punchScript;
    private MoveScript moveScript;
    private RunScript runScript;
    private PunchRunning punchRunningScript;
    private PunchFly punchFlyScript;
    private AdrenalinaRun adrenalinaRunScript;
    #endregion
    public float normalSpeed = 7;
    public float adrenalinaSpeed = 10f;
    public float speed;
    public float recoverStamina = 0;
    public float damageBase = 2;
    public float currentTimePunch = 0;
    private float coolDownPunch = 0.75f;
    public float currentTimeState = 0;
    public Vector3 toMove = Vector3.zero;
    private Vector3 lastDirection = Vector3.forward;
    private bool running = false;
    private bool onGround = false;
    public bool adrenalinaOn = false;
    private bool exhaust = false;
    public float gravity = 60;
    private float distanceWithCamera;
    private int layer = 0;

    public float staminaPerColetazo = 3;
    private float currentTimeColetazo = 0;

    private float speedKnockBack = 0;
    private float timeKnockBack = 0;

    private float life = 100;

    private float forceJump = 0;
    private PlantaTierra plantaTierra;
    private bool planningPlant = false;

    private Vector3 directionKnockBack;



    private CharacterController characterController;
    public SpriteRenderer spriteRenderer;
    public Animator anim;
    private Color startColor;
    public GameObject children;
    public StateMachine stateMachine;
    [SerializeField] private CanvasPlayerScript canvasPlayer;
    // Start is called before the first frame update
    void Awake()
    {
        stamina = GetComponent<IEstaminable>();
        adrenalina = GetComponent<IAdrenalinable>();
        characterController = GetComponent<CharacterController>();
        startColor = spriteRenderer.color;
        speed = normalSpeed;

        punch = children.GetComponent<PunchScript>();
        punchFly = children.GetComponent<PunchFly>();
        adrenalinaPunch = children.GetComponent<AdrenalinaPunch>();
        aereoPunch = children.GetComponent<AereoPunchScript>();
        adrenalinaAereoPunch = children.GetComponent<AdrenalinaAereoPunch>();

        punchScript = punch.GetComponent<PunchScript>();
        moveScript = moving.GetComponent<MoveScript>();
        runScript = run.GetComponent<RunScript>();
        punchRunningScript = punchRunning.GetComponent<PunchRunning>();
        punchFlyScript = punchFly.GetComponent<PunchFly>();

        adrenalinaRunScript = adrenalinaRun.GetComponent<AdrenalinaRun>();

        ChangeState(State.MOVING);
    }

    // Update is called once per frame
    void Update()
    {
        if (adrenalinaOn)
            AdrenalinaCheckVariablesUntilMoving();
        else CheckVariablesUntilMoving();

        stateMachine.ExecuteState();
        switch (currentState)
        {
            case State.MOVING:
                if(!adrenalinaOn)
                    stamina.RegenStamina();
                break;
            case State.PUNCHING:
                break;
            case State.RUNING:
                break;
            case State.PUNCHRUNNING:
                break;
            case State.FLYINGKICK:
                break;
            case State.KNOCKBACK:
                break;
            case State.HABILITY:
                break;
            case State.ADRENALINAPUNCH:
                break;
            case State.ADRENALINARUN:
                break;
            case State.ADRENALINAPUNCHRUN:
                break;
            case State.ADRENALINAAEREOPUNCH:
                break;
        }
        CheckStats();
    }

    public void ChangeState(State newState)
    {

        switch (newState)
        {
            case State.MOVING:
                stateMachine.ChangeState(moving);
                break;
            case State.PUNCHING:
                stateMachine.ChangeState(punch);
                break;
            case State.RUNING:
                running = true;
                stateMachine.ChangeState(run);
                break;
            case State.PUNCHRUNNING:
                running = true;
                stateMachine.ChangeState(punchRunning);

                break;
            case State.KNOCKBACK:
                stateMachine.ChangeState(knockBack);
                break;
            case State.FLYINGKICK:
                stateMachine.ChangeState(punchFly);

                break;
            case State.HABILITY:
                break;

            case State.JUMPING:
                stateMachine.ChangeState(jumping);
                break;

            case State.PLANNING:
                stateMachine.ChangeState(planning);
                break;

            case State.AEREOPUNCH:
                stateMachine.ChangeState(aereoPunch);
                break;
            case State.ADRENALINAPUNCH:
                stateMachine.ChangeState(adrenalinaPunch);
                break;
            case State.ADRENALINARUN:
                running = true;
                stateMachine.ChangeState(adrenalinaRun);
                break;
            case State.ADRENALINAPUNCHRUN:
                running = true;
                stateMachine.ChangeState(adrenalinaPunchRun);
                break;
            case State.ADRENALINAAEREOPUNCH:
                stateMachine.ChangeState(adrenalinaAereoPunch);
                break;
        }

        currentState = newState;
    }


    private void CheckVariablesUntilMoving()
    {
        if (Input.GetMouseButton(0) && CanPunch() && currentState == State.MOVING)
        {
            ChangeState(State.PUNCHING);
            return;
        }
        if (Input.GetKey(runKey) && currentState == State.MOVING && stamina.Stamina > 2)
        {
            ChangeState(State.RUNING);
            return;
        }
        else if (Input.GetKeyUp(runKey) && currentState == State.RUNING)
        {
            running = false;
            ChangeState(State.MOVING);
        }

        if (Input.GetKeyDown(jumpKey) && (currentState == State.MOVING || currentState == State.RUNING) && currentTimeColetazo == 0)
        {
            currentTimeColetazo = 0.5f;
            running = false;
            StartJump();
        }

        if (Input.GetKey(runKey) && running && CanPunchRunning() && speed > normalSpeed + 1 && Input.GetMouseButton(0))
            ChangeState(State.PUNCHRUNNING);

        if (Input.GetMouseButton(1) && currentState == State.MOVING)
            OnActionButton();

        if (currentTimeColetazo > 0)
        {
            currentTimeColetazo -= Time.deltaTime;
            if (currentTimeColetazo < 0)
            {
                currentTimeColetazo = 0;
            }
        }

    }

    private void AdrenalinaCheckVariablesUntilMoving()
    {

        if (Input.GetMouseButton(0) && AdrenalinaCanPunch() && currentState == State.MOVING)
        {
            ChangeState(State.ADRENALINAPUNCH);
            return;
        }
        if (Input.GetKey(runKey) && currentState == State.MOVING)
        {
            ChangeState(State.ADRENALINARUN);
            return;
        }
        else if (Input.GetKeyUp(runKey) && currentState == State.ADRENALINARUN)
        {
            running = false;
            ChangeState(State.MOVING);
        }

        if (Input.GetKeyDown(jumpKey) && (currentState == State.MOVING || currentState == State.ADRENALINAPUNCHRUN) && currentTimeColetazo == 0)
        {
            currentTimeColetazo = 0.5f;
            running = false;
            StartJump();
        }

        if (Input.GetKey(runKey) && running && AdrenalinaCanPunchRunning() && Input.GetMouseButton(0))
            ChangeState(State.ADRENALINAPUNCHRUN);

        if (Input.GetMouseButton(1) && currentState == State.MOVING)
            OnActionButton();

        if (currentTimeColetazo > 0)
        {
            currentTimeColetazo -= Time.deltaTime;
            if (currentTimeColetazo < 0)
            {
                currentTimeColetazo = 0;
            }
        }

    }

    private bool AdrenalinaCanPunch()
    {
        return currentTimePunch == 0 && currentState == State.MOVING;
    }

    private bool AdrenalinaCanPunchRunning()
    {
        return currentTimePunch == 0 && currentState == State.ADRENALINARUN;
    }

    private bool CanPunch()
    {
        return currentTimePunch == 0 && stamina.Stamina >= punchScript.costStaminaPerPunch && currentState == State.MOVING;
    }

    private bool CanPunchRunning()
    {
        return currentTimePunch == 0 && stamina.Stamina >= punchRunningScript.costStaminaPerPunch * (speed - normalSpeed)
            && currentState == State.RUNING;
    }

    private void CheckStats()
    {
        if (!adrenalinaOn)
        {
            if (currentTimePunch > 0 && currentState != State.PUNCHING && currentState != State.PUNCHRUNNING &&
                currentState != State.FLYINGKICK && currentState != State.AEREOPUNCH)
            {
                currentTimePunch += Time.deltaTime;
                if (currentTimePunch >= coolDownPunch)
                    currentTimePunch = 0;
            }

            if (!running)
            {
                if (speed > normalSpeed)
                {
                    speed -= Time.deltaTime * runScript.decreseSpeedSecond;
                    if (speed < normalSpeed)
                        speed = normalSpeed;
                }
            }
        }
        else
        {
            adrenalina.ReduceAdrenalina();
            canvasPlayer.ChangeAdrenalina();

            if(adrenalina.Adrenalina <= 0)
            {
                exhaust = true;
                //te quedas en la mierda
            }
            else
            {
                if (currentTimePunch > 0 && currentState != State.ADRENALINAPUNCH && currentState != State.ADRENALINAPUNCHRUN &&
    currentState != State.FLYINGKICK && currentState != State.ADRENALINAAEREOPUNCH)
                {
                    currentTimePunch += Time.deltaTime;
                    if (currentTimePunch >= coolDownPunch)
                        currentTimePunch = 0;
                }

                if (!running)
                {
                    if (speed > adrenalinaSpeed)
                    {
                        speed -= Time.deltaTime * adrenalinaRunScript.decreseSpeedSecond;
                        if (speed < adrenalinaSpeed)
                            speed = adrenalinaSpeed;
                    }
                }
            }
        }
    }

    private void OnActionButton()
    {
        RaycastHit rayHit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(Camera.main.transform.position, ray.direction, out rayHit, 25, layerMask))
        {
            if ((rayHit.collider.transform.position - gameObject.transform.position).magnitude <= 3)
            {
                if (rayHit.collider.gameObject.GetComponent<PlantaCatapulta>() != null)
                {
                    ChangeState(State.INSIDEPLANT);
                    rayHit.collider.gameObject.GetComponent<PlantaCatapulta>().EnterInThePlant(this);
                }

            }


        }
    }

    public void StartAdrenalina(bool _active)
    {
        adrenalinaOn = _active;
        exhaust = false;
        if(adrenalinaOn)
        {
            adrenalina.Adrenalina = adrenalina.MaxAdrenalina;
        }

        canvasPlayer.ShowAdrenalina(_active);

    }

    public float ChangeSpeed(float _speed)
    {
        if(!adrenalinaOn)
            _speed = Mathf.Clamp(_speed, normalSpeed, runScript.maxSpeed);
        else
            _speed = Mathf.Clamp(_speed, adrenalinaSpeed, adrenalinaRunScript.maxSpeed);
        speed = _speed;
        return speed;
    }

    public void StartKnockBack(float _damage, float _time, Vector3 _direction, bool _movement = true)
    {
        if (_movement)
        {
            speedKnockBack = _damage;
            directionKnockBack = _direction;
        }
        else
        {
            speedKnockBack = 0;
            directionKnockBack = Vector3.zero;
        }

        timeKnockBack = _time;
        ChangeState(State.KNOCKBACK);
    }

    public void GetStatsKnockBack(out float _speed, out float _time, out Vector3 _direction)
    {
        _speed = speedKnockBack;
        _time = timeKnockBack;
        _direction = directionKnockBack;
    }

    public void ResetSpeed()
    {
        if (adrenalinaOn)
            speed = adrenalinaSpeed;
        else speed = normalSpeed;
    }

    public float GetSpeed()
    {
        return speed;
    }

    public void SetLayerPlayer(int _layer)
    {
        layer = _layer;
    }

    public int GetLayerPlayer()
    {
        return layer;
    }

    public float ChangeLife(float _life)
    {
        life += _life;
        if (life > 100)
            life = 100;
        else if (life < 0)
            life = 0;
        print(life + "  " + _life);
        return life;
    }

    #region PlantaFunctions

    public void StartJump()
    {
        stamina.ModifiyStamina(-staminaPerColetazo);
        if (plantaTierra != null)
        {
            planningPlant = plantaTierra.planningPlant;
            if (planningPlant)
            {
                if (currentState == State.MOVING)
                {
                    toMove = moveScript.toMove;
                    if (toMove.magnitude == 0)
                        toMove = moveScript.lastDirection;
                }
                else if (currentState == State.RUNING)
                    toMove = runScript.toMove;
                toMove = toMove * speed / 2;
            }
            else
            {
                toMove = plantaTierra.direction;
            }
            ChangeState(State.JUMPING);
        }
    }

    public float GetForceJump()
    {
        return forceJump;
    }

    public bool GetPlanningPlant()
    {
        return planningPlant;
    }

    public float GetGravity()
    {
        return gravity;
    }

    public void SetPlantaTierra(PlantaTierra _planta)
    {
        plantaTierra = _planta;
        if (plantaTierra != null)
        {
            forceJump = plantaTierra.forceJump;
        }
        else
            forceJump = 0;
    }
    #endregion
}
