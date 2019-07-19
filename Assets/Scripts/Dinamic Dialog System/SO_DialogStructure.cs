using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogues
{
    public abstract class SO_DialogStructure : ScriptableObject 
    {


        [Header("Speaker")]
        [SerializeField] protected CharacterData speaker;


        public Sprite GetSpeakerSprite()
        {
            return speaker.GetSprite();
        }
        public string GetSpeakerName()
        {
            return speaker.GetName();
        }


    }
}


