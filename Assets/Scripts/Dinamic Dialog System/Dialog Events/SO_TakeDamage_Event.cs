using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogues
{

    [CreateAssetMenu(fileName = "Take Damage Event", menuName = "Dialog events/Take Damage")]
    public class SO_TakeDamage_Event : SO_DialogEvent
    {
        [SerializeField] string targetOfTheDamage_Tag;
        private TestPlayer target;
        [SerializeField] float damageOnBaseOne;


        public override void Initialize(Dialogs_GameController _gameController)
        {
            base.Initialize(_gameController);

            target = GameObject.FindGameObjectWithTag(targetOfTheDamage_Tag).GetComponent<TestPlayer>();

            if (!target)
                throw new MissingReferenceException("Target is not found for some reason, do something about it");

        }

        public override void Execute()
        {
            base.Execute();
            if (target)
                target.TakeDamage(damageOnBaseOne);
        }

    }

}