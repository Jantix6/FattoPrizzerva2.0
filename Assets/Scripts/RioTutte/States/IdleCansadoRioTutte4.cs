using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleCansadoRioTutte4 : BaseState
{
    private float currentTimeIdle = 0;
    public float timeToExitIdle = 2;
    private RioTutteMainScript mainScript;


    private void Start()
    {
        mainScript = GetComponent<RioTutteMainScript>();
        player = mainScript.GetPlayer();
    }

    public override void Enter()
    {
        currentTimeIdle = 0;
    }

    public override void Execute()
    {
        currentTimeIdle += Time.deltaTime;

        if(currentTimeIdle >= timeToExitIdle)
        {
            mainScript.GetPhase4().ChangeState(RioTuttePhase4.State.MOVING);
        }
    }

    public override void Exit()
    {
        currentTimeIdle = 0;
    }
}
