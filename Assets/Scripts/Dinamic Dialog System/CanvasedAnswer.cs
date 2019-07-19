using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Dialogues
{
    public class CanvasedAnswer : MonoBehaviour 
    {
        [SerializeField] private DialogueManager dialogueManager;
        [SerializeField] private TMP_Text answerText;
        [SerializeField] private SO_QuestionAnswerStructure targetQuestionAnswerStructure;
        [SerializeField] private Button answerButton;


        public void Initialize(SO_Answer _answer, DialogueManager _dialogueManager, Language _targetLanguage)
        {
            answerText.text = _answer.GetAnswerBody(_targetLanguage);
            dialogueManager = _dialogueManager;
            targetQuestionAnswerStructure = _answer.GetTargetStructure();

            answerButton.onClick.AddListener(OpenTargetQAStructure);
        }


        private void OpenTargetQAStructure()
        {
            if (targetQuestionAnswerStructure)
                dialogueManager.ShowDialogueStructure(targetQuestionAnswerStructure);
            else 
                Debug.LogWarning("The target Structure is not set ");
        }
    }
}


