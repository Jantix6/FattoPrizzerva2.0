using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingRioTuttePhase1 : BaseState
{
    public float speed = 1;
    private Vector3 direction;
    private RioTutteMainScript mainScript;
    private CharacterController characterController;
    private bool inverse = false;

    void Awake()
    {
        mainScript = GetComponent<RioTutteMainScript>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        characterController = gameObject.GetComponent<CharacterController>();
    }

    public override void Enter()
    {
        speed = mainScript.speed;
        if (mainScript.phase == 3 && mainScript.GetPhase3().currentState == RioTuttePhase3.State.MOVINGINVERSE)
            inverse = true;
        else inverse = false;
    }

    public override void Execute()
    {
        direction = player.gameObject.transform.position - gameObject.transform.position;
        direction = new Vector3(direction.x, 0, direction.z).normalized;
        if (inverse)
            direction = -direction;

        characterController.Move(direction * speed * Time.deltaTime);
    }

    public override void Exit()
    {
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (mainScript.GetStateMachine().currentState == this)
        {
            if (hit.gameObject == mainScript.GetPlayer().gameObject &&
                (mainScript.GetPlayer().currentState == PlayerScript.State.MOVING || mainScript.GetPlayer().currentState == PlayerScript.State.RUNING))
            {
                Vector3 direction = mainScript.GetPlayer().gameObject.transform.position - gameObject.transform.position;
                direction = new Vector3(direction.x, 0, direction.z).normalized;

                switch (mainScript.phase)
                {
                    case 1:
                        mainScript.GetPlayer().StartKnockBack(mainScript.GetPhase1().damageImpact, mainScript.GetPhase1().timeImpact, direction);
                        break;
                    case 2:
                        mainScript.GetPlayer().StartKnockBack(mainScript.GetPhase2().damageImpact, mainScript.GetPhase2().timeImpact, direction);
                        break;
                    case 3:
                        mainScript.GetPlayer().StartKnockBack(mainScript.GetPhase3().damageImpact, mainScript.GetPhase3().timeImpact / 2, direction);
                        break;
                    case 4:
                        mainScript.GetPlayer().StartKnockBack(mainScript.GetPhase4().damageImpact, mainScript.GetPhase4().timeImpact / 2, direction);
                        break;


                }

            }
        }
    }
}
