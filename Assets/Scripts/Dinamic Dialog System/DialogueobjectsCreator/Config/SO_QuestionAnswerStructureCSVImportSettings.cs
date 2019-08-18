using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogues
{
    [CreateAssetMenu(fileName = "Config QA CSV Object Config", menuName = "Config QA CSV Object Config")]
    public sealed class SO_QuestionAnswerStructureCSVImportSettings : ScriptableObject
    {
        [Header("QA Object configuratioin")]
        [SerializeField] public int questionIDPosition = 0;
        [SerializeField] public int catQuestionPosition = 1;
        [SerializeField] public int espQuestionPosition = 2;
        [SerializeField] public int engQuestionPosition = 3;
        [SerializeField] public int numbersOfQuestionsPosition = 4;
        [SerializeField] public int catAnswerBodyPosition = 5;
        [SerializeField] public int espAnswerBodyPosition = 6;
        [SerializeField] public int engAnswerBodyPosition = 7;
    }
}
