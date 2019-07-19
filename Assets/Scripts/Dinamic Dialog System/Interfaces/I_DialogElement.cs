using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogues
{
    public interface I_DialogElement 
    {
        void Initialize(SO_DialogStructure _inputData, DialogueManager _manager, Language _targetLanguage);
    }
}


