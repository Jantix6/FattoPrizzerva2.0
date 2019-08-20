using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Dialogues
{
    [CreateAssetMenu(fileName = "Answer", menuName = "Answers/New Answer")]
    public class SO_Answer : ScriptableObject 
    {
        private Dialogs_GameController gameController;


        // [SerializeField] private SO_DialogEvent eventOnClick;
        [SerializeField] private SO_DialogEventsContainer eventsOnClickContainer;
        [SerializeField] private SO_DialogStructure targetStructure;

        [SerializeField] private SO_langaugeBasedStringContainer answer;


        public void Initialize (Dialogs_GameController _gameController)
        {
            gameController = _gameController;
            Debug.Log(gameController.name + " is now setup and ready to rock!");
        }

        public UnityAction GetEventActionsToPerform()
        {
            if (eventsOnClickContainer)
            {
                eventsOnClickContainer.Initialize(gameController);
                return eventsOnClickContainer.Excecute;
            }
            return null;

        }
        public List<SO_DialogEvent> GetListOfEvents()
        {
            if (eventsOnClickContainer)
            {
                return eventsOnClickContainer.GetEventsList();
            }
            return null;
        }
         
        public string GetAnswerBody(Language _targetLanguage)
        {
            if (answer)
            {
                SO_LanguageBasedString desiredLBS = null;
                desiredLBS = answer.GetLanguageBasedString(_targetLanguage, this.name);
                return desiredLBS.text;
            }
            return null;
            
        }
        public void SetAnswerBody(Language _targetLanguage, string _newText)
        {
            if (answer)
            {
                SO_LanguageBasedString desiredLBS = null;
                desiredLBS = answer.GetLanguageBasedString(_targetLanguage, this.name);
                desiredLBS.text = _newText;
            }

        }
        public void SetAnswerBody(SO_langaugeBasedStringContainer _languageBasedStringContainer)
        {
            if (_languageBasedStringContainer)
                answer = _languageBasedStringContainer;
        }
        public SO_langaugeBasedStringContainer GetLanguageBasedStringContainer()
        {
            return answer;
        }

        public SO_DialogStructure GetTargetStructure()
        {
            if (targetStructure)
                return targetStructure;
            else
            {
                // Debug.LogError("Target strucuture is not set on " + this.name);
                return null;
            }          
        }






    }



}

