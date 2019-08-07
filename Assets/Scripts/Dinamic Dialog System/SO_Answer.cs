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
        private DialogEventsManager eventsManager;

        //[SerializeField] private List<LanguageBasedString> answers;
        [SerializeField] private DialogEventType eventOnClick;
        [SerializeField] private SO_DialogStructure targetStructure;

        [SerializeField] private SO_langaugeBasedStringContainer answer;
        
        public void SetEventsManager (DialogEventsManager _manager)
        {
            eventsManager = _manager;
            Debug.Log(eventsManager.name + " is now setup and ready to rock!");

        }

        public UnityEngine.Events.UnityAction GetEventActionToPerform()
        {
            return eventsManager.ExcecuteEvent(eventOnClick);
        }

        public string GetAnswerBody(Language _targetLanguage)
        {

            SO_LanguageBasedString desiredLBS = null;
            desiredLBS = answer.GetLanguageBasedString(_targetLanguage, this.name);
            return desiredLBS.text;

        }

        public SO_DialogStructure GetTargetStructure()
        {
            if (targetStructure)
                return targetStructure;
            else
            {
                Debug.LogError("Target strucuture is not set on " + this.name);
                
                return null;
            }
                
        }


    }



}

