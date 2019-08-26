using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tasks;

public class Test_EventManager : MonoBehaviour
{
    

    public void ApplyReward(Reward _reward)
    {
        Debug.Log("Reward " + _reward.name + " applied");
    }
    public void KillEntity(Entity _entity)
    {
        Debug.Log("Calling killing of " + _entity);
    }

}
