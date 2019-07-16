using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Dialogues
{
    public class CanvasedDialogue : MonoBehaviour
    {
        [SerializeField] private DialogueManager dialogueManager;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private QuestionAnswerStructure dialogueData;

        [SerializeField] private TMPro.TMP_Text question;
        [SerializeField] private Image spekerImage;
        [SerializeField] private CanvasedAnswer answerPrefab;

        [SerializeField] private Transform answersContainer;

        public void Initialize(QuestionAnswerStructure _questionAnswerData, DialogueManager _dialoguemanager)
        {
            dialogueManager = _dialoguemanager;
            dialogueData = _questionAnswerData;

            question.text = dialogueData.GetQuestion();
            spekerImage.sprite = _questionAnswerData.GetSpeakerSprite();

            foreach (Answer answer in dialogueData.GetAnswers())
            {
                CanvasedAnswer canvasedAnswer = Instantiate(answerPrefab, answersContainer);
                canvasedAnswer.Initialize(answer ,dialogueManager);
            }

            EnableVisibility();
        }

        private void EnableVisibility()
        {
            canvasGroup.alpha = 1f;
        }

        public void DisableVisibilty()
        {
            canvasGroup.alpha = 0f;
        }

    }
}


