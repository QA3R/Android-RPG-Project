using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;
using static UnityEngine.EventSystems.EventTrigger;
using Managers.Battle;
using TMPro;
using ScriptableObjects;

namespace Entities
{
    public class EnemyEntity : Entity
    {
        private float TotalDMG;

        #region OnEnable, OnDisable, Start

        public override void Start()
        {
            base.Start();
        }
        #endregion

        public override void SetSpawnPoint()
        {
            //base.SetSpawnPoint(bManager, cManager);
            transform.position = BattleManager.Instance.EnemySpawnPoints[BattleManager.Instance.EnemyBattleID].transform.position;
            BattleManager.Instance.EnemyBattleID++;
        }

        public override void Attack(Entity caster, Entity receiver)
        {
            if (Target == null)
            {
                Target = BattleManager.Instance.PlayableUnitsInBattle[0];
            }

            base.Attack(caster, receiver);
        }
    }

}
