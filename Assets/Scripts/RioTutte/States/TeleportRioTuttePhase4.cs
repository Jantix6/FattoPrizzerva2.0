using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportRioTuttePhase4 : BaseState
{

    private RioTutteMainScript mainScript;
    private CharacterController characterController;
    private float currentTime = 0;
    public float speedDash = 45;
    public float timeDash = 0.2f;

    private void Start()
    {
        mainScript = GetComponent<RioTutteMainScript>();
        player = mainScript.GetPlayer();
        characterController = mainScript.GetCharacterController();
    }

    public override void Enter()
    {
        mainScript.GetPhase4().currentTimeState = 0;
        currentTime = 0;
        mainScript.direction = (player.gameObject.transform.position - gameObject.transform.position).normalized;
        mainScript.direction = new Vector3(mainScript.direction.x, 0, mainScript.direction.z);
        characterController.enabled = false;
        //comprobar con un rayo si en la dirección dónde se ha movido por ultima vez hay una pared si la hay haced lo q ya hay

        Vector3 position = (gameObject.transform.position - player.transform.position).normalized;
        position *= 2;
        transform.position = player.transform.position + position;
        characterController.enabled = true;
    }

    public override void Execute()
    {
        CollisionFlags collisionFlags = characterController.Move(mainScript.direction * Time.deltaTime * speedDash / 2);
        if (currentTime > timeDash)
        {
            switch (mainScript.phase)
            {
                case 4:
                    mainScript.GetPhase4().currentTimeDash += Time.deltaTime;
                    mainScript.GetPhase4().ChangeState(RioTuttePhase4.State.MOVING);
                    break;
            }
        }


        currentTime += Time.deltaTime;
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

                        player.StartKnockBack(speedDash  / 1.5f, mainScript.GetPhase4().timeImpact, (player.transform.position - gameObject.transform.position).normalized);
                        mainScript.GetPhase4().ChangeState(RioTuttePhase4.State.MOVINGINVERSE);

                        break;


                }
            }
        }
    }
}
