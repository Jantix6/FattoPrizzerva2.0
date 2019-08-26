using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogues
{
    [CreateAssetMenu(fileName = "Config QA CSV Object Config", menuName = "Config QA CSV Object Config")]
    public sealed class SO_QuestionAnswerStructureCSVImportSettings : CSVObject_ImportSettings
    {
        [Header("QA Object configuration")]
        [SerializeField] private int questionIDPosition = 0;
        [SerializeField] private int objectName = 1;
        [SerializeField] private int catQuestionPosition = 2;
        [SerializeField] private int espQuestionPosition = 3;
        [SerializeField] private int engQuestionPosition = 4;
        [SerializeField] private int answerIdPosition = 5;
        [SerializeField] private int catAnswerBodyPosition = 6;
        [SerializeField] private int espAnswerBodyPosition = 7;
        [SerializeField] private int engAnswerBodyPosition = 8;

        public int QuestionIDPosition { get => questionIDPosition; set => questionIDPosition = value; }
        public int ObjectName { get => objectName; }
        public int CatQuestionPosition { get => catQuestionPosition; set => catQuestionPosition = value; }
        public int EspQuestionPosition { get => espQuestionPosition; set => espQuestionPosition = value; }
        public int EngQuestionPosition { get => engQuestionPosition; set => engQuestionPosition = value; }
        public int AnswerIdPosition { get => answerIdPosition; set => answerIdPosition = value; }
        public int CatAnswerBodyPosition { get => catAnswerBodyPosition; set => catAnswerBodyPosition = value; }
        public int EspAnswerBodyPosition { get => espAnswerBodyPosition; set => espAnswerBodyPosition = value; }
        public int EngAnswerBodyPosition { get => engAnswerBodyPosition; set => engAnswerBodyPosition = value; }
    }
}
