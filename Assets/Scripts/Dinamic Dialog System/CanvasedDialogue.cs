using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Dialogues
{
    public class CanvasedDialogue : CanvasedDialogElement , I_DialogElement
    {
        [Header("Dialogue References")]
        [SerializeField] private SO_QuestionAnswerStructure dialogueData;

        [Header("Canvas dialoge references")]
        [SerializeField] private TMPro.TMP_Text question;
        [SerializeField] private Image spekerImage;
        [SerializeField] private CanvasedAnswer answerPrefab;

        [SerializeField] private Transform answersContainer;
        [SerializeField] private List<CanvasedAnswer> canvasedAnswers;

        public Button GetButton(int _desiredIndex)
        {
            Button buttonToReturn = null;

            buttonToReturn = canvasedAnswers[_desiredIndex].GetComponentInChildren<Button>();

            if (buttonToReturn == null)
                Debug.LogError("No button found on index " + _desiredIndex + " or no button object is on the object ");

            return buttonToReturn;
        }

        public void Initialize(SO_DialogStructure _inputData, DialogueManager _dialogManager,Dialogs_GameController _gameControler , Language _targetlanguage)
        {
            dialogueManager = _dialogManager;
            dialogueData = _inputData as SO_QuestionAnswerStructure;

            question.text = dialogueData.GetQuestion(_targetlanguage);
            spekerImage.sprite = _inputData.GetSpeakerSprite();

            foreach (SO_Answer answer in dialogueData.GetAnswers())
            {
                answer.Initialize(_gameControler);

                CanvasedAnswer canvasedAnswer = Instantiate(answerPrefab, answersContainer);
                canvasedAnswer.Initialize(answer, dialogueManager, _targetlanguage);

                if (canvasedAnswers == null)
                    canvasedAnswers = new List<CanvasedAnswer>();

                canvasedAnswers.Add(canvasedAnswer);
            }

            EnableVisibility();
        }

        // Dani busca un modo de no tener este codigo en dos lugares distitnos al mismo tiempo 
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
    }
}


