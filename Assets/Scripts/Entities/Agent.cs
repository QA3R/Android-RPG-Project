using Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjects;
using Managers;

namespace Entities
{

    public class Agent : Entity
    {
        // Start is called before the first frame update
        public override void Start()
        {
            base.Start();
        }

        public override void SetSpawnPoint(BattleManager bManager, CameraManager cManager)
        {
            base.SetSpawnPoint(bManager, cManager);
            transform.position = bManager.AllySpawnPoints[bManager.AllySpawnPointNum].transform.position;
            bManager.AllySpawnPointNum++;
        }

        #region Action Methods
        public override void Attack(Entity entity)
        {
            //Attack code to attack enemies
        }

        public override void AtkSkill()
        {

        }

        public override void BuffSKill()
        {

        }

        public override void DebuffSkill()
        {

        }

        public override void HealSkill()
        {

        }
        #endregion
    }
}
