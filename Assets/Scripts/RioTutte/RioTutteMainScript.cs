using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RioTutteMainScript : EnemieBasic
{
    public int phase = 0;
    private RioTuttePhase1 phase1;
    private RioTuttePhase2 phase2;
    private StateMachine stateMachine;
    private CharacterController characterController;
    private PlayerScript player;
    public Vector3 direction = Vector3.back;
    private float timeToWait = 0;
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        characterController = GetComponent<CharacterController>();
        phase1 = GetComponent<RioTuttePhase1>();
        phase2 = GetComponent<RioTuttePhase2>();
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
                    break;
            }
        }
        else
        {
            timeToWait -= Time.deltaTime;
            if (timeToWait < 0)
                timeToWait = 0;
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
                break;
            case 4:
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

    public StateMachine GetStateMachine()
    {
        return stateMachine;
    }
}
