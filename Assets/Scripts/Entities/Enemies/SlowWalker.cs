using Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjects;
using Managers;

namespace Entities.Enemies
{
    public class SlowWalker : Enemy
    {   
        // Start is called before the first frame update
        public override void Start()
        {
            base.Start();
        }

        public override void SetSpawnPoint(BattleManager bManager, CameraManager cManager)
        {
            base.SetSpawnPoint(bManager, cManager);
        }

        public override void Attack(Entity enemy)
        {
            base.Attack(enemy);
        }

        public override void AtkSkill()
        {
            //New code
            //Debuff enemy
        }
    }
}

