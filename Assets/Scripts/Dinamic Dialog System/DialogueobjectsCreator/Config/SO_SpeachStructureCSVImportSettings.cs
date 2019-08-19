using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogues
{
    [CreateAssetMenu(fileName = "Config Speach CSV Object Config", menuName = "Config Speach CSV Object Config")]
    public sealed class SO_SpeachStructureCSVImportSettings : CSVObject_ImportSettings
    {
        [Header("Speach Object configuration")]
        [SerializeField] public int objectIdentifierPosition = 0;
        [SerializeField] public int catTitlePosition = 1;
        [SerializeField] public int espTitlePosition = 2;
        [SerializeField] public int engTitlePosition = 3;
        [SerializeField] public int catBodyPosition = 4;
        [SerializeField] public int espBodyPosition = 5;
        [SerializeField] public int engBodyPosition = 6;

        public int CatTitlePosition { get => catTitlePosition; set => catTitlePosition = value; }
        public int ObjectIdentifierPosition { get => objectIdentifierPosition; set => objectIdentifierPosition = value; }
        public int EspTitlePosition { get => espTitlePosition; set => espTitlePosition = value; }
        public int EngTitlePosition { get => engTitlePosition; set => engTitlePosition = value; }
        public int CatBodyPosition { get => catBodyPosition; set => catBodyPosition = value; }
        public int EspBodyPosition { get => espBodyPosition; set => espBodyPosition = value; }
        public int EngBodyPosition { get => engBodyPosition; set => engBodyPosition = value; }
    }

}


