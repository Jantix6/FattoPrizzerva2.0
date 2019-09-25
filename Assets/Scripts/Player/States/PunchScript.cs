using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchScript : BaseState
{
    public float costStaminaPerPunch = 5;
    [SerializeField] private float speedPunch = 3;
    [SerializeField] private float damageBase = 2;
    [SerializeField] private float timerDamage = 0.25f;

    public GameObject golpePuñoPrefab;
    private GameObject golpePuñoInstanciado;
    private SpriteRenderer spriteRenderer;

    private BaseState moving;

    [SerializeField] private SphereCollider colliderPunch;
    // Start is called before the first frame update
    void Start()
    {
        moving = player.moving;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }


    public override void Enter()
    {
        player.anim.SetTrigger("Fist");
        player.stamina.ModifiyStamina(-costStaminaPerPunch);
        player.currentTimeState = 0;
        player.ChangeSpeed(speedPunch);
        moving.Enter();
    }

    public override void Execute()
    {
        player.currentTimeState += Time.deltaTime;
        if (player.currentTimeState >= timerDamage && player.currentTimeState < 0.3f)
        {
            if (golpePuñoInstanciado == null)
            {
                if (spriteRenderer.flipX)
                {
                    golpePuñoInstanciado = Instantiate(golpePuñoPrefab, gameObject.transform.position - new Vector3(0.7f, 0, 0), golpePuñoPrefab.transform.rotation);
                }
                else
                {
                    golpePuñoInstanciado = Instantiate(golpePuñoPrefab, gameObject.transform.position + new Vector3(0.7f, 0, 0), golpePuñoPrefab.transform.rotation);
                }
                golpePuñoInstanciado.transform.parent = gameObject.transform;
                golpePuñoInstanciado.GetComponent<SpriteRenderer>().flipX = spriteRenderer.flipX;

            }
            colliderPunch.enabled = true;
        }
        if (player.currentTimeState >= 0.3f)
            player.ChangeState(PlayerScript.State.MOVING); //player.stateMachine.ChangeState(player.moving);
        moving.Execute();

    }

    public override void Exit()
    {
        Destroy(golpePuñoInstanciado);
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
