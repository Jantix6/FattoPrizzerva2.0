using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashSimpleRioTutte : BaseState
{
    private RioTutteMainScript mainScript;
    private CharacterController characterController;
    private float currentTime = 0;
    public float speedDash = 30;
    public float timeDelay = 0.15f;
    public float timeDash = 1f;
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
        mainScript.direction = (player.gameObject.transform.position - gameObject.transform.position).normalized;
    }

    public override void Execute()
    {
        if(currentTime > timeDelay)
        {
            if (!dashing)
            {
                dashing = true;
            }
            CollisionFlags collisionFlags = characterController.Move(mainScript.direction * Time.deltaTime * speedDash / 2);
            if(currentTime > timeDash)
            {
                switch (mainScript.phase)
                {
                    case 1:
                        mainScript.GetPhase1().ChangeState(RioTuttePhase1.State.MOVING);
                        break;
                    case 2:
                        mainScript.GetPhase2().currentTimeDash += Time.deltaTime;
                        mainScript.GetPhase2().ChangeState(RioTuttePhase2.State.MOVING);
                        break;
                }
            }
        }
        else
        {
            CollisionFlags collisionFlags = characterController.Move(-1 *mainScript.direction * Time.deltaTime * speedDash / 2f);
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
                if (player.currentState == PlayerScript.State.PUNCHRUNNING || player.currentState == PlayerScript.State.FLYINGKICK)
                {
                    mainScript.GetPhase2().currentDash = 0;
                    mainScript.GetPhase2().currentTimeDash = 0;
                    mainScript.GetPhase2().ChangeState(RioTuttePhase2.State.MOVING);

                    Vector3 _direction = (player.gameObject.transform.position - gameObject.transform.position).normalized;
                    _direction = new Vector3(_direction.x, 0, _direction.z);
                    player.StartKnockBack(speedDash * 0.5f, 0.1f, _direction);
                }
                else { 
                Vector3 _direction = (player.gameObject.transform.position - gameObject.transform.position).normalized;
                Vector2 guia = new Vector2(_direction.x, _direction.z).normalized;
                guia = Vector2.Perpendicular(guia);
                _direction = new Vector3(guia.x, 0, guia.y);
                if (((player.transform.position + _direction) - (gameObject.transform.position)).magnitude < ((player.transform.position - _direction) - (gameObject.transform.position)).magnitude)
                    _direction *= -1;
                player.StartKnockBack(speedDash * 1.05f, 0.25f, _direction);
                if (currentTime < timeDash - timeDash / 10)
                    currentTime = timeDash - timeDash / 10;
            }
            }
        }
    }

}
