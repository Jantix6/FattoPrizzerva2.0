using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportStartStateRioTutte4 : BaseState
{

    private RioTutteMainScript mainScript;
    private CharacterController characterController;
    private int teleports;
    private float currentTime = 0;
    public float speedDash = 45;
    public float timeDash = 0.3f;
    private Vector3 position;
    private Vector3 startPos;
    private bool teleported = false;
    public LayerMask layerMask;
    private GameObject wall;
    private Vector3 dir;

    private void Start()
    {
        mainScript = GetComponent<RioTutteMainScript>();
        teleports = 0;
        player = mainScript.GetPlayer();
        characterController = mainScript.GetCharacterController();
    }

    public override void Enter()
    {
        teleported = false;
        startPos = transform.position;
        wall = mainScript.GetPhase4().wall;
        dir = wall.GetComponent<WallScript>().direction;
        mainScript.GetPhase4().currentTimeState = 0;
        currentTime = 0;
        position = mainScript.GetPlayer().transform.position + dir;

        characterController.enabled = false;
        transform.position = position;
        characterController.enabled = true;

        mainScript.direction = (mainScript.GetPlayer().transform.position - gameObject.transform.position).normalized;


    }

    public override void Execute()
    {
        characterController.Move(mainScript.direction * Time.deltaTime * 4f);

        currentTime += Time.deltaTime;
        if (currentTime > 0.75f)
        {
            mainScript.GetPhase4().ChangeState(RioTuttePhase4.State.MOVING);
        }

    }

    public override void Exit()
    {
        mainScript.GetPhase4().currentTimeState = Time.deltaTime;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (mainScript.GetStateMachine().currentState == this)
        {
            if (hit.gameObject == player.gameObject)
            {

                switch (mainScript.phase)
                {
                    case 4:
                        mainScript.GetPhase4().currentDash = 0;
                        mainScript.GetPhase4().currentTimeDash = 0;

                        player.StartKnockBack(speedDash / 5f, mainScript.GetPhase4().timeImpact, (player.transform.position - gameObject.transform.position).normalized);
                        mainScript.GetPhase4().ChangeState(RioTuttePhase4.State.MOVING);

                        break;


                }

            }
        }
    }
}

