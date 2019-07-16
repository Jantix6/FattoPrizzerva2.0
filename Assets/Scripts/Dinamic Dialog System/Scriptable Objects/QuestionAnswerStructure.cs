using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Dialogues
{
    [CreateAssetMenu(fileName = "QAStructure", menuName = "QAStructure/New QA Structure")]
    public class QuestionAnswerStructure : ScriptableObject
    {
        [Header("Speaker")]
        [SerializeField] private CharacterData speaker;
        //[SerializeField] private string speakerName;
        //[SerializeField] private Sprite speakerSprite;


        [SerializeField] [TextArea] private string question;
        [SerializeField] private List<Answer> answers_Lst;

        public string GetQuestion()
        {
            return question;
        }
        public List<Answer> GetAnswers()
        {
            return answers_Lst;
        }
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

