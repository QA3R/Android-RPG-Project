using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;
using static UnityEngine.EventSystems.EventTrigger;
using Managers;
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
            transform.position = BattleHandler.Instance.EnemySpawnPoints[BattleHandler.Instance.EnemyBattleID].transform.position;
            BattleHandler.Instance.EnemyBattleID++;
        }
    }

}
