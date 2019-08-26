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
        [SerializeField] public int objectNamePosition = 1;
        [SerializeField] public int catTitlePosition = 2;
        [SerializeField] public int espTitlePosition = 3;
        [SerializeField] public int engTitlePosition = 4;
        [SerializeField] public int catBodyPosition = 5;
        [SerializeField] public int espBodyPosition = 6;
        [SerializeField] public int engBodyPosition = 7;

        public int CatTitlePosition { get => catTitlePosition;}
        public int ObjectNamePosition { get => objectNamePosition;}
        public int ObjectIdentifierPosition { get => objectIdentifierPosition;}
        public int EspTitlePosition { get => espTitlePosition; }
        public int EngTitlePosition { get => engTitlePosition;  }
        public int CatBodyPosition { get => catBodyPosition; }
        public int EspBodyPosition { get => espBodyPosition; }
        public int EngBodyPosition { get => engBodyPosition;  }
    }

}


