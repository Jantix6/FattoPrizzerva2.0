using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogues
{
    [CreateAssetMenu(fileName = "Dialogs Configuration" , menuName = "Dialogs Configuration")]
    public class SO_DialogConfig : ScriptableObject
    {
        public List<SO_TriggerAndDialogStructure> dialogsWithTriggers;
        

        public SO_DialogStructure GetDialogStructure(DialogTrigger _dialogTrigger)
        {
            SO_DialogStructure returnStructure = null;

            foreach (SO_TriggerAndDialogStructure triggerDialogStructure in dialogsWithTriggers)
            {
                if (triggerDialogStructure.dialogTrigger == _dialogTrigger)
                {
                    returnStructure = triggerDialogStructure.dialogStructure;
                    if (returnStructure)
                    {
                        return returnStructure;

                    } else
                    {
                        Debug.LogError("The trigger you are asking for does not have a structure defined (fix it)");
                        return null;
                    }

                }           
            }

            Debug.LogError("The trigger you want is not defined on the dialogsWithTriggers list (fix it)");
            return null;           
        }

    }
}


