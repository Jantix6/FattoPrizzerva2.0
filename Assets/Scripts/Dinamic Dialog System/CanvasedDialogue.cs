using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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

        public void Initialize(SO_DialogStructure _inputData, DialogueManager _manager, Language _targetlanguage)
        {
            dialogueManager = _manager;
            dialogueData = _inputData as SO_QuestionAnswerStructure;

            question.text = dialogueData.GetQuestion(_targetlanguage);
            spekerImage.sprite = _inputData.GetSpeakerSprite();

            foreach (SO_Answer answer in dialogueData.GetAnswers())
            {
                CanvasedAnswer canvasedAnswer = Instantiate(answerPrefab, answersContainer);
                canvasedAnswer.Initialize(answer, dialogueManager, _targetlanguage);
            }

            EnableVisibility();
        }
    }
}


