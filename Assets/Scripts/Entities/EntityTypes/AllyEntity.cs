using Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjects;
using Managers;

namespace Entities
{
    public class AllyEntity : Entity
    {
        private Entity target;
        float TotalDmg;

        #region OnEnable, OnDisable, Start

        // Start is called before the first frame update
        public override void Start()
        {
            base.Start();
  
        }
        #endregion
        public override void SetSpawnPoint()
        {
            transform.position = BattleHandler.Instance.AllySpawnPoints[BattleHandler.Instance.AllySpawnPointNum].transform.position;
            BattleHandler.Instance.AllySpawnPointNum++;
        }
    }
}
