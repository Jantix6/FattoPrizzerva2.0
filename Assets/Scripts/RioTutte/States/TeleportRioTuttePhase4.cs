using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportRioTuttePhase4 : BaseState
{

    private RioTutteMainScript mainScript;
    private CharacterController characterController;
    private int teleports;
    private float currentTime = 0;
    public float speedDash = 45;
    public float timeDash = 0.3f;
    private Vector3 position;
    private bool teleported = false;

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
        mainScript.GetPhase4().currentTimeState = 0;
        currentTime = 0;
        teleports++;
        mainScript.direction = (player.gameObject.transform.position - gameObject.transform.position).normalized;
        mainScript.direction = new Vector3(mainScript.direction.x, 0, mainScript.direction.z);
        //comprobar con un rayo si en la dirección dónde se ha movido por ultima vez hay una pared si la hay haced lo q ya hay

        position = (gameObject.transform.position - player.transform.position).normalized;
        if (teleports <= 5)
        {
            characterController.enabled = false;
            position *= 2;
            transform.position = player.transform.position + position;
            characterController.enabled = true;
        }

    }

    public override void Execute()
    {
        if (teleports > 5)
        {
            if (currentTime > 0.5f)
            {
                if (!teleported)
                {
                    mainScript.direction = (player.gameObject.transform.position - gameObject.transform.position).normalized;
                    mainScript.direction = new Vector3(mainScript.direction.x, 0, mainScript.direction.z);
                    position = (gameObject.transform.position - player.transform.position).normalized;
                    characterController.enabled = false;
                    position *= 3.5f;
                    transform.position = player.transform.position + position;
                    characterController.enabled = true;
                    teleported = true;
                }
                CollisionFlags collisionFlags = characterController.Move(mainScript.direction * Time.deltaTime * speedDash / 2);
                if (currentTime > timeDash + 0.5f)
                {
                    switch (mainScript.phase)
                    {
                        case 4:
                            mainScript.GetPhase4().currentTimeDash += Time.deltaTime;
                            mainScript.GetPhase4().ChangeState(RioTuttePhase4.State.MOVING);
                            break;
                    }
                }
            }

            currentTime += Time.deltaTime;
        }
        else
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
    }

    public override void Exit()
    {
        mainScript.GetPhase4().currentTimeState = Time.deltaTime;
        if (teleports == 5)
        {
            player.StartAdrenalina(true);
            mainScript.GetPhase4().timeBetweenDash *= 3;
        }
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

                        player.StartKnockBack(speedDash / 1.5f, mainScript.GetPhase4().timeImpact, (player.transform.position - gameObject.transform.position).normalized);
                        mainScript.GetPhase4().ChangeState(RioTuttePhase4.State.MOVINGINVERSE);

                        break;


                }

            }
        }
    }
}
