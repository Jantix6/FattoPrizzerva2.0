using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogues
{
    [CreateAssetMenu(fileName = "CharacterData", menuName = "Characters/New Character Profile")]
    public class CharacterData : ScriptableObject
    {
        [SerializeField] private string name;
        [SerializeField] private Sprite sprite;
        [SerializeField] [TextArea] private string bibliography;

        public string GetName()
        {
            return name;
        }
        public Sprite GetSprite()
        {
            return sprite;
        }
        public string GetBibliography()
        {
            return bibliography;
        }

    }
}


