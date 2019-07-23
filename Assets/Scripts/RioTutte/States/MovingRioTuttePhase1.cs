using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingRioTuttePhase1 : BaseState
{
    public float speed = 1;
    private Vector3 direction;
    private RioTutteMainScript mainScript;
    private CharacterController characterController;

    private void Start()
    {
        mainScript = GetComponent<RioTutteMainScript>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        characterController = gameObject.GetComponent<CharacterController>();
    }

    public override void Enter()
    {
    }

    public override void Execute()
    {
        direction = player.gameObject.transform.position - gameObject.transform.position;
        direction = new Vector3(direction.x, 0, direction.z).normalized;

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

                }

            }
        }
    }
}
