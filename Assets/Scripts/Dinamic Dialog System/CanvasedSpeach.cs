using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Dialogues
{
    public class CanvasedSpeach : CanvasedDialogElement , I_DialogElement
    {
        [Header("Speach references")]
        [SerializeField] SO_SpeachStructure speachData;
        [SerializeField] Button nextButton;

        [Header("Speach Atributes")]
        [SerializeField] private Image spekerImage;
        [SerializeField] private TMP_Text speakerName;
        [SerializeField] private TMP_Text textTitle;
        [SerializeField] private TMP_Text textBody;

        // get the button deffined by its index (in this case there is only one button so the index is not used)
        public Button GetButton(int _desiredIndex)
        {
            if (nextButton)
            {
                return nextButton;
            }
            else
            {
                Debug.LogError("No button found on " + this.name);
                return null;
            }

        }

        public void Initialize(SO_DialogStructure _inputData, DialogueManager _manager, DialogEventsManager _eventsManager,  Language _targetLanguage)
        {
            dialogueManager = _manager;
            speachData = _inputData as SO_SpeachStructure;

            textTitle.text = speachData.GetSpeachTitle(_targetLanguage);
            textBody.text = speachData.GetSpeachBody(_targetLanguage);
            speakerName.text = speachData.GetSpeakerName();

            spekerImage.sprite = speachData.GetSpeakerSprite();

            // al pulsar siguiente vamos a la siguiente estructura
            nextButton.onClick.AddListener(OnNextButtonClick);
        }

        public void InitializeDefaultKeyboardNavigation(EventSystem _inputEventSystem, int _preselectedButtonIndex = 0)
        {
            if (!_inputEventSystem)
            {
                Debug.LogError("No Input event system to modify, ABORTING");
                return;
            }

            _inputEventSystem.firstSelectedGameObject = GetButton(_preselectedButtonIndex).gameObject;
            _inputEventSystem.SetSelectedGameObject(GetButton(_preselectedButtonIndex).gameObject);

            if (!_inputEventSystem.firstSelectedGameObject)
            {
                Debug.LogError("No button found, ABORTING");
            }
        }

        private void OnNextButtonClick()
        {
            // Change to the next dialogue
            dialogueManager.GoToNextStructure();
        }
    }

}


