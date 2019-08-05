using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Dialogues
{
    public interface I_DialogElement 
    {
        void Initialize(SO_DialogStructure _inputData, DialogueManager _manager, Language _targetLanguage);
        Button GetButton(int _desiredIndex);
        void InitializeDefaultKeyboardNavigation(EventSystem _inputEventSystem , int _preselectedButtonIndex = 0);

    }
}


