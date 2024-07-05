using Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjects;
using Managers.Battle;

namespace Entities
{
    public class AllyEntity : Entity
    {  
        #region OnEnable, OnDisable, Start

        // Start is called before the first frame update
        public override void Start()
        {
            base.Start();
  
        }
        #endregion

        public override void SetSpawnPoint()
        {
            transform.position = BattleManager.Instance.AllySpawnPoints[BattleManager.Instance.AllySpawnID].transform.position;
            BattleManager.Instance.AllySpawnID++;
        }

        //Attack the first enemy in the spawn order
        public override void Attack(Entity caster, Entity receiver)
        {
            if (Target == null)
            {
                Target = BattleManager.Instance.EnemyUnitsInBattle[0];
            }

            base.Attack(caster, receiver);
        }
    }
}
