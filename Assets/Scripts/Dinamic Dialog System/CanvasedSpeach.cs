using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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

        /*
        public void Initialize(SpeachStructure_SO _speachData, DialogueManager _dialogueManager)
        {
            dialogueManager = _dialogueManager;
            speachData = _speachData;

            textTitle.text = speachData.GetSpeachTitle();
            textBody.text = speachData.GetSpeachBody();
            speakerName.text = speachData.GetSpeakerName();

            spekerImage.sprite = speachData.GetSpeakerSprite();

            // al pulsar siguiente vamos a la siguiente estructura
            nextButton.onClick.AddListener(dialogueManager.NextStructure);
        }
        */
        public void Initialize(SO_DialogStructure _inputData, DialogueManager _manager, Language _targetLanguage)
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

        private void OnNextButtonClick()
        {
            // Change to the next dialogue
            dialogueManager.NextStructure();
        }
    }

}


