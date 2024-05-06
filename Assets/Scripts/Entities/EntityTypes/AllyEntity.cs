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
        #region OnEnable, OnDisable, Start

        // Start is called before the first frame update
        public override void Start()
        {
            base.Start();
  
        }
        #endregion

        public override void SetSpawnPoint()
        {
            transform.position = BattleHandler.Instance.AllySpawnPoints[BattleHandler.Instance.AllySpawnID].transform.position;
            BattleHandler.Instance.AllySpawnID++;
        }

        //Attack the first enemy in the spawn order
        public override void Attack(Entity caster, Entity receiver)
        {
            base.Attack(caster, receiver);
        }
    }
}
