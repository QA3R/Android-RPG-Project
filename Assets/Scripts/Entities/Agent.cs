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
        private CameraManager cManager;
       
        public Entity target;
        float TotalDmg;

        #region OnEnable, OnDisable, Start
        void OnDisable()
        {
            eHandler.OnDealDMG -= Attack;
        }

        private void OnEnable()
        {

        }
        // Start is called before the first frame update
        public override void Start()
        {
            base.Start();
  
        }
        #endregion
        public override void SetSpawnPoint(BattleManager bManager, CameraManager cManager)
        {
            transform.position = bManager.AllySpawnPoints[bManager.AllySpawnPointNum].transform.position;
            bManager.AllySpawnPointNum++;
        }

        private void SetTarget()
        {
            target = cManager.Targets[cManager.cameraIndex].GetComponent<Entity>();
        }

        #region Action Methods
        public virtual void Attack(Entity entity, float dmgTaken)
        {
            if (this.Atk - target.Def < 0.3f)
            {
                TotalDmg = battleManager.UnitsInBattle[0].Atk * 0.3f;
                eHandler.OnDealDMG.Invoke(target, TotalDmg);
            }
            else
            {
                TotalDmg = this.Atk - target.Def;
                eHandler.OnDealDMG.Invoke(target, TotalDmg);
            }
            //Attack code to attack enemies
        }
        #endregion
    }
}
