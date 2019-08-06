using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogues
{
    [CreateAssetMenu(fileName = "CharacterData", menuName = "Characters/New Character Profile")]
    public class CharacterData : ScriptableObject
    {
        [SerializeField] private string characterName;
        [SerializeField] private Sprite sprite;
        [SerializeField] [TextArea] private string characterDescription;
        [SerializeField] [TextArea] private string biography;

        public string GetName()
        {
            return characterName;
        }
        public Sprite GetSprite()
        {
            return sprite;
        }
        public string GetBibliography()
        {
            return biography;
        }
        public string GetCharacterDescription() 
        {
            return characterDescription;
        }

    }
}


