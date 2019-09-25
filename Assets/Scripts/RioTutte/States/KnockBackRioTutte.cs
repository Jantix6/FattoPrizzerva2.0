using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBackRioTutte : BaseState
{
    public float speed = 5;
    public float timeKnockback = 0.3f;
    public Vector3 direction;
    private float currentTime = 0;
    private CharacterController characterController;
    private RioTutteMainScript mainScript;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        characterController = gameObject.GetComponent<CharacterController>();
        mainScript = GetComponent<RioTutteMainScript>();
    }

    public override void Enter()
    {
        mainScript.hudCaras.ChangeCara(HudCambioCarasRioTutte.Caras.SERIEDAD);
        currentTime = 0;
        speed = mainScript.GetSpeedKnockback();
        timeKnockback = mainScript.GetTimeKnockBack();
        direction = mainScript.GetDirectionKnockback();
    }

    public override void Execute()
    {
        if (currentTime >= timeKnockback)
        {
            switch (mainScript.phase)
            {
                case 1:
                    mainScript.GetPhase1().ChangeState(RioTuttePhase1.State.MOVING);
                    break;
                case 2:
                    mainScript.GetPhase2().ChangeState(RioTuttePhase2.State.MOVING);
                    break;
                case 3:
                    mainScript.GetPhase3().ChangeState(RioTuttePhase3.State.MOVING);
                    break;
                case 4:
                    mainScript.GetPhase4().ChangeState(RioTuttePhase4.State.IDLECANSADO);

                    break;
            }
        }
        else
        {
            characterController.Move(direction * speed * Time.deltaTime);
            currentTime += Time.deltaTime;
        }
    }

    public override void Exit()
    {
        switch (mainScript.phase)
        {
            case 1:
            case 2:
                mainScript.hudCaras.ChangeCara(HudCambioCarasRioTutte.Caras.NORMAL);
                break;
            case 3:
                mainScript.hudCaras.ChangeCara(HudCambioCarasRioTutte.Caras.HERIDO);
                    break;
            case 4:
                mainScript.GetPhase4().ChangeState(RioTuttePhase4.State.IDLECANSADO);

                break;
        }
        
    }
}
