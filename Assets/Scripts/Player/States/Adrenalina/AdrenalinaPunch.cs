using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdrenalinaPunch : BaseState
{

    [SerializeField] private float speedPunch = 5;
    [SerializeField] private float damageBase = 25;
    private float normalDamage;
    [SerializeField] private float timerDamage = 0.25f;
    private float currentTime = 0;

    private BaseState moving;

    [SerializeField] private SphereCollider colliderPunch;
    // Start is called before the first frame update
    void Start()
    {
        moving = player.moving;
        normalDamage = damageBase;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Enter()
    {
        player.currentTimeState = 0;
        player.ChangeSpeed(speedPunch);
        moving.Enter();
        currentTime = 0;
        damageBase = normalDamage;
    }

    public override void Execute()
    {
        if (Input.GetMouseButton(0))
        {
            currentTime += Time.deltaTime;
        }
        else {
            if(player.currentTimeState == 0)
                player.anim.SetTrigger("Fist");

            currentTime = Mathf.Lerp(1, 2.5f, currentTime);
            damageBase *= currentTime;
            player.currentTimeState += Time.deltaTime;
            if (player.currentTimeState >= timerDamage && player.currentTimeState < 0.3f)
                colliderPunch.enabled = true;
            if (player.currentTimeState >= 0.3f)
                player.ChangeState(PlayerScript.State.MOVING); //player.stateMachine.ChangeState(player.moving);
            moving.Execute();
        }
    }

    public override void Exit()
    {
        player.ResetSpeed();
        player.currentTimePunch += Time.deltaTime;
        colliderPunch.enabled = false;
        moving.Exit();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemie")
        {
            if (player.stateMachine.currentState == this)
            {
                other.gameObject.GetComponent<EnemieBasic>().MoveDirectionHit((other.gameObject.transform.position - gameObject.transform.position).normalized, damageBase * speedPunch);
                player.ChangeState(PlayerScript.State.MOVING);
            }
        }
    }
}
