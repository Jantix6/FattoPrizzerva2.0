using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Dialogues
{
    public class CanvasedAnswer : MonoBehaviour
    {
        [SerializeField] private DialogueManager dialogueManager;
        [SerializeField] private TMP_Text answerText;
        [SerializeField] private QuestionAnswerStructure targetQuestionAnswerStructure;
        [SerializeField] private Button answerButton;

        public void Initialize(Answer _answer, DialogueManager _dialogueManager)
        {
            answerText.text = _answer.GetAnswerBody();
            dialogueManager = _dialogueManager;
            targetQuestionAnswerStructure = _answer.GetTargetStructure();

            answerButton.onClick.AddListener(OpenTargetQAStructure);
        }

        private void OpenTargetQAStructure()
        {
            dialogueManager.ShowDialogueStructure(targetQuestionAnswerStructure);
        }
    }
}


