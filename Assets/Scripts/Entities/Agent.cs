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
            transform.position = TurnManager.Instance.AllySpawnPoints[TurnManager.Instance.AllySpawnPointNum].transform.position;
            TurnManager.Instance.AllySpawnPointNum++;
        }

        private void SetTarget()
        {
            target = BattleCameraHandler.Instance.Targets[BattleCameraHandler.Instance.cameraIndex].GetComponent<Entity>();
        }
    }
}
