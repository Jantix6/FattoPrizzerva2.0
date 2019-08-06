using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltraDashRioTutte : BaseState
{
    private RioTutteMainScript mainScript;
    private CharacterController characterController;
    private float currentTime = 0;
    public float speedDash = 45;
    public float timeDelay = 0.2f;
    public float timeDash = 0.7f;
    private bool dashing = false;

    private void Start()
    {
        mainScript = GetComponent<RioTutteMainScript>();
        player = mainScript.GetPlayer();
        characterController = mainScript.GetCharacterController();
    }

    public override void Enter()
    {
        currentTime = 0;
        dashing = false;
        mainScript.direction = (player.gameObject.transform.position - gameObject.transform.position).normalized;
        mainScript.direction = new Vector3(mainScript.direction.x, 0, mainScript.direction.z);
    }

    public override void Execute()
    {
        if (currentTime > timeDelay)
        {
            if (!dashing)
            {
                dashing = true;
            }
            CollisionFlags collisionFlags = characterController.Move(mainScript.direction * Time.deltaTime * speedDash / 2);
            if (currentTime > timeDash)
            {
                switch (mainScript.phase)
                {
                    case 3:
                        mainScript.GetPhase3().currentTimeDash += Time.deltaTime;
                        mainScript.GetPhase3().ChangeState(RioTuttePhase3.State.MOVING);
                        break;
                    case 4:
                        mainScript.GetPhase4().currentTimeDash += Time.deltaTime;
                        mainScript.GetPhase4().ChangeState(RioTuttePhase4.State.MOVING);
                        break;
                }
            }
        }
        else
        {
            CollisionFlags collisionFlags = characterController.Move(-1f * mainScript.direction * Time.deltaTime * speedDash / 2.5f);
        }

        currentTime += Time.deltaTime;
    }

    public override void Exit()
    {
        dashing = false;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (mainScript.GetStateMachine().currentState == this)
        {
            if (hit.gameObject == player.gameObject && dashing)
            {

                switch (mainScript.phase)
                {
                    case 3:
                        mainScript.GetPhase3().currentDash = 0;
                        mainScript.GetPhase3().currentTimeDash = 0;
                        mainScript.GetPhase3().ChangeState(RioTuttePhase3.State.MOVINGINVERSE);

                        player.StartKnockBack(0, mainScript.GetPhase3().timeImpact, Vector3.zero, false);
                        player.ChangeLife(-mainScript.GetPhase3().damageImpact);
                        break;
                    case 4:
                        mainScript.GetPhase4().currentDash = 0;
                        mainScript.GetPhase4().currentTimeDash = 0;
                        mainScript.GetPhase4().ChangeState(RioTuttePhase4.State.MOVINGINVERSE);

                        player.StartKnockBack(0, mainScript.GetPhase4().timeImpact, Vector3.zero, false);
                        player.ChangeLife(-mainScript.GetPhase4().damageImpact);
                        break;


                }
            }
        }
    }
}
