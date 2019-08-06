using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdrenalinaAereoPunch : BaseState
{
    public float speedPunch = 60;
    public float damageBase = 100f;
    private CharacterController characterController;
    private SphereCollider sphereCollider;
    private float actualY = 0;
    private bool ground = false;
    private float timeGround = 0;
    // Start is called before the first frame update
    void Start()
    {
        sphereCollider = gameObject.GetComponent<SphereCollider>();
        characterController = player.GetComponent<CharacterController>();
    }

    public override void Enter()
    {
        player.currentTimeState = 0;
        ground = false;
        timeGround = 0;
        sphereCollider.enabled = true;
        actualY = gameObject.transform.position.y;
    }

    public override void Execute()
    {
        CollisionFlags collisionFlags;

        if (player.currentTimeState < 0.2f)
        {
            collisionFlags = characterController.Move(Vector3.up * Time.deltaTime * speedPunch / 10f);
        }
        else if (!ground)
        {
            collisionFlags = characterController.Move(Vector3.down * speedPunch * Time.deltaTime);
            if ((collisionFlags & CollisionFlags.Below) != 0)
            {
                timeGround = player.currentTimeState;
                ground = true;
            }
        }
        else if (player.currentTimeState - timeGround >= 0.2f)
        {
            player.ChangeState(PlayerScript.State.MOVING);
        }




        player.currentTimeState += Time.deltaTime;
    }

    public override void Exit()
    {
        sphereCollider.enabled = false;
        player.currentTimePunch += Time.deltaTime;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (player.stateMachine.currentState == this)
        {
            if (hit.gameObject.tag == "Enemie")
            {
                if (player.stateMachine.currentState == this)
                {
                    Vector3 direction = hit.gameObject.transform.position - player.gameObject.transform.position;
                    direction = YEquals0vector.ConvertVectorToNullY(direction);

                    if (direction.magnitude == 0)
                        direction = Vector3.right;

                    hit.gameObject.GetComponent<EnemieBasic>().MoveDirectionHit((hit.gameObject.transform.position - gameObject.transform.position).normalized, Mathf.Abs(damageBase * (actualY - gameObject.transform.position.y)));
                    player.ChangeState(PlayerScript.State.MOVING);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider hit)
    {
        if (player.stateMachine.currentState == this)
        {
            if (hit.gameObject.tag == "Enemie")
            {
                if (player.stateMachine.currentState == this)
                {
                    Vector3 direction = hit.gameObject.transform.position - player.gameObject.transform.position;
                    direction = YEquals0vector.ConvertVectorToNullY(direction);

                    if (direction.magnitude == 0)
                        direction = Vector3.right;

                    hit.gameObject.GetComponent<EnemieBasic>().MoveDirectionHit((hit.gameObject.transform.position - gameObject.transform.position).normalized, Mathf.Abs(damageBase * (actualY - gameObject.transform.position.y)));
                    player.ChangeState(PlayerScript.State.MOVING);
                }
            }
        }
    }
}
