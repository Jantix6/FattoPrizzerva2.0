using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AereoPunchScript : BaseState
{
    public float speedPunch = 35;
    private CharacterController characterController;
    private float actualY = 0;
    private bool ground = false;
    private float timeGround = 0;
    // Start is called before the first frame update
    void Start()
    {
        characterController = player.GetComponent<CharacterController>();
    }

    public override void Enter()
    {
        player.currentTimeState = 0;
        ground = false;
        timeGround = 0;
        actualY = gameObject.transform.position.y;
    }

    public override void Execute()
    {
        CollisionFlags collisionFlags;

        if (player.currentTimeState < 0.2f)
        {
            collisionFlags = characterController.Move(Vector3.up * Time.deltaTime);
        }
        else if(!ground)
        {
            collisionFlags = characterController.Move(Vector3.down * speedPunch * Time.deltaTime);
            if ((collisionFlags & CollisionFlags.Below) != 0)
            {
                timeGround = player.currentTimeState;
                ground = true;
            }
        }
        else if(player.currentTimeState - timeGround >= 0.2f)
        {
            player.ChangeState(PlayerScript.State.MOVING);
        }




        player.currentTimeState += Time.deltaTime;
    }

    public override void Exit()
    {
        player.currentTimePunch += Time.deltaTime;
    }
}
