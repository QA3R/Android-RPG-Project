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
        void OnDisable()
        {
            EventHandler.Instance.OnDealDMG -= Attack;
        }

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

        #region Action Methods
        public virtual void Attack(Entity entity, float dmgTaken)
        {
            if (this.Atk - target.Def < 0.3f)
            {
                TotalDmg = BattleManager.Instance.UnitsInBattle[0].Atk * 0.3f;
                EventHandler.Instance.OnDealDMG.Invoke(target, TotalDmg);
            }
            else
            {
                TotalDmg = this.Atk - target.Def;
                EventHandler.Instance.OnDealDMG.Invoke(target, TotalDmg);
            }
            //Attack code to attack enemies
        }
        #endregion
    }
}
