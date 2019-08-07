using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Dialogues
{

    public class TakeDamage_Event : GameEvent
    {
        TestPlayer player;
        int damageAmount;

        public void Initialize(TestPlayer _player, int _damageAmount)
        {
            base.Initialize();
            player = _player;
            damageAmount = _damageAmount;

        }

        public override void Excecute()
        {
            base.Excecute();
            player.TakeDamage(damageAmount);
        }
    }
}


