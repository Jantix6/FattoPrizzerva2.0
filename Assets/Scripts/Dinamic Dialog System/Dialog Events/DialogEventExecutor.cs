using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogues
{
    public class DialogEventExecutor : MonoBehaviour
    {
        [SerializeField] SO_DialogEvent dialogEvent;

        public void SetDialogEvent(SO_DialogEvent dialogEvent)
        {
            this.dialogEvent = dialogEvent;
        } 

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}


