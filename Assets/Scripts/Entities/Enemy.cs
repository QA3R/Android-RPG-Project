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
    public class Enemy : Entity
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
            transform.position = TurnManager.Instance.EnemySpawnPoints[TurnManager.Instance.EnemySpawnPointNum].transform.position;
            BattleCameraHandler.Instance.SetupVCList(this.gameObject);
            TurnManager.Instance.EnemySpawnPointNum++;
        }
        

        #region Action Methods
        //Selects the Ally with the lowest current HP and calculates DMG dealt based on the enemy's ATK
        public override void Attack() 
        {

        }

        #endregion
    }

}
