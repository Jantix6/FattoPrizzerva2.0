using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Tasks
{


    // Clas with contains what the player is rewarded 
    [CreateAssetMenu(fileName = "Reward_Object", menuName = "Reward")]
    public class Reward : ScriptableObject
    {
        [SerializeField] private string rewardName;
        [SerializeField] [TextArea] private string rewardDescription;

        [Header("Functions to call on achive")]
        [SerializeField] private UnityEvent OnCompletion;               // Methods to call once the reward is applied

        public virtual void Apply()
        {
            Debug.LogWarning("DANOLE UN REWARD AL host");
            if (OnCompletion != null)
            {
                OnCompletion.Invoke();
            }
                
        }

        // Se pueden aplicar al jugador o al enemigo

        // Pude activar el desbloqueo de algo GDD  pacifico en caballos
        // skin
        // accion <--------<
    }
}


