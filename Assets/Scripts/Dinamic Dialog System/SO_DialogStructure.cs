using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogues
{
    public abstract class SO_DialogStructure : ScriptableObject 
    {
        [Header("Speaker")]
        [SerializeField] protected CharacterData speaker;

        private bool CheckIfSpeaker()
        {
            if (speaker)
                return true;
            else
            {
                Debug.LogError("Speaker not set on " + this.name);
                return false;
            }
                
        }

        public Sprite GetSpeakerSprite()
        {
            if (CheckIfSpeaker())
                return speaker.GetSprite();
            else
                return null;
                
        }
        public string GetSpeakerName()
        {
            if (CheckIfSpeaker())
                return speaker.GetName();
            else
                return null;
        }

    }
}


