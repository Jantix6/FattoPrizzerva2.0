using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Dialogues
{
    public enum DialogEventType
    {
        NONE,
        SAY_HI,
        TAKE_DAMAGE,
    }


    [RequireComponent(typeof(DialogueManager))]
    // manager que se encargara de gestionar que cosas pueden pasar al seleccionar cada respuesta en los dialogos
    public class DialogEventsManager : MonoBehaviour
    {
        // required references
        [SerializeField] private DialogueManager dialogManager;

        SayHi_Event sayHi;
        TakeDamage_Event takeDamage;

        private void Awake()
        {
            dialogManager = GetComponent<DialogueManager>();

            sayHi = new SayHi_Event();
            takeDamage = new TakeDamage_Event();
        }

        public UnityEngine.Events.UnityAction ExcecuteEvent(DialogEventType _dialogEvent)
        {
            switch (_dialogEvent)
            {
                case DialogEventType.SAY_HI:
                    return SayHi();
                    break;

                case DialogEventType.NONE:
                    return None;
                    break;
                case DialogEventType.TAKE_DAMAGE:
                    // return PlayerTakeDamage();
                    break;
                default:
                    break;
            }

            return null;
        }


        private void None()
        {
            Debug.LogError("No action set for this button");
        }
        private UnityAction SayHi()
        {
            sayHi.Initialize();
            return sayHi.Excecute;
        }

        private UnityAction PlayerTakeDamage(TestPlayer _player, int _damageAmount)
        {
            takeDamage.Initialize(_player, _damageAmount);
            return takeDamage.Excecute;
        }

        public void FrezeGame()
        {

        }
        public void UnFrezeGame()
        {

        }




    }
}

