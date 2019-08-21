using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RioTutteMainScript : EnemieBasic, ICongelable
{
    public int phase = 0;
    private RioTuttePhase1 phase1;
    private RioTuttePhase2 phase2;
    private RioTuttePhase3 phase3;
    private RioTuttePhase4 phase4;
    private StateMachine stateMachine;
    private CharacterController characterController;
    private PlayerScript player;
    public Vector3 direction = Vector3.back;
    public float speed = 1;
    public HudCambioCarasRioTutte hudCaras;
    private float timeToWait = 0;
    private bool frozen = false;
    private GameObject gameController;
    private Dialogs_GameController dialogsController;
    // Start is called before the first frame update
    void Awake()
    {
        gameController = GameObject.FindGameObjectWithTag("GameManager");
        dialogsController = gameController.GetComponent<Dialogs_GameController>();
        dialogsController.AddToFreazablesList(this);

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        characterController = GetComponent<CharacterController>();
        phase1 = GetComponent<RioTuttePhase1>();
        phase2 = GetComponent<RioTuttePhase2>();
        phase3 = GetComponent<RioTuttePhase3>();
        phase4 = GetComponent<RioTuttePhase4>();
        stateMachine = GetComponent<StateMachine>();

    }

    private void Start()
    {
        phase = 1;
        ChangePhase(1);
    }

    // Update is called once per frame
    void Update()
    {
        if (!frozen)
        {
            if (timeToWait == 0)
            {
                switch (phase)
                {
                    case 1:
                        phase1.Execute();
                        break;
                    case 2:
                        phase2.Execute();
                        break;
                    case 3:
                        phase3.Execute();
                        break;
                    case 4:
                        phase4.Execute();
                        break;
                }
            }
            else
            {
                currentDamage = TypeOfDamage.NADA;
                timeToWait -= Time.deltaTime;
                if (timeToWait < 0)
                    timeToWait = 0;
            }

            Gravity();
        }
    }

    public void ChangePhase(int _newPhase)
    {
        if (phase < _newPhase)
            timeToWait = 2f;
        switch (_newPhase)
        {
            case 1:
                phase = 1;
                phase1.StartExecution();
                break;
            case 2:
                phase = 2;
                phase2.StartExecution();
                break;
            case 3:
                phase = 3;
                phase3.StartExecution();
                break;
            case 4:
                phase = 4;
                phase4.StartExecution();
                break;
        }
    }

    public PlayerScript GetPlayer()
    {
        return player;
    }

    public CharacterController GetCharacterController()
    {
        return characterController;
    }

    public RioTuttePhase1 GetPhase1()
    {
        return phase1;
    }

    public RioTuttePhase2 GetPhase2()
    {
        return phase2;
    }

    public RioTuttePhase3 GetPhase3()
    {
        return phase3;
    }

    public RioTuttePhase4 GetPhase4()
    {
        return phase4;
    }

    public StateMachine GetStateMachine()
    {
        return stateMachine;
    }

    private void Gravity()
    {
        characterController.Move(Vector3.down * player.GetGravity() * Time.deltaTime);
    }

    public void Congelar(bool _anim = false)
    {
        frozen = true;
    }

    public void Descongelar(bool _anim = false)
    {
        frozen = false;
    }
}
