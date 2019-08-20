using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunRioTutte : BaseState
{
    private RioTutteMainScript mainScript;
    private float currentTime = 0;
    private float maxTime = 0;

    private void Start()
    {
        mainScript = GetComponent<RioTutteMainScript>();
        player = mainScript.GetPlayer();
    }
    public override void Enter()
    {
        currentTime = 0;
        switch(mainScript.phase)
        {
            case 1:
                break;
            case 2:
                maxTime = mainScript.GetPhase2().GetTimeStun();
                break;
            case 3:
                maxTime = mainScript.GetPhase3().GetTimeStun();
                break;
            case 4:
                maxTime = mainScript.GetPhase4().GetTimeStun();

                break;
        }
    }

    public override void Execute()
    {
        if (currentTime > maxTime)
        {
            switch (mainScript.phase)
            {
                case 1:
                    break;
                case 2:
                    mainScript.GetPhase2().ChangeState(RioTuttePhase2.State.MOVING);
                    break;
                case 3:
                    mainScript.GetPhase3().ChangeState(RioTuttePhase3.State.MOVING);
                    break;
                case 4:
                    mainScript.GetPhase4().ChangeState(RioTuttePhase4.State.MOVING);

                    break;
            }
        }
        else
            currentTime += Time.deltaTime;
    }

    public override void Exit()
    {
        currentTime = 0;
    }
}
