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
        public Entity target;
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
            transform.position = BattleManager.Instance.AllySpawnPoints[BattleManager.Instance.AllySpawnPointNum].transform.position;
            BattleManager.Instance.AllySpawnPointNum++;
        }

        private void SetTarget()
        {
            target = CameraManager.Instance.Targets[CameraManager.Instance.cameraIndex].GetComponent<Entity>();
        }
    }
}
