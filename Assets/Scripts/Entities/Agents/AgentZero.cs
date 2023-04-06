using Entities;
using ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;



namespace Entities.Agents
{
    public class AgentZero : Agent
    {
        // Start is called before the first frame update
        public override void Start()
        {
            base.Start(); 
        }

        // Update is called once per frame
        void Update()
        {

        }

        #region Action Methods
        public override void Attack(Entity entity, float dmgTaken)
        {
            //base.Attack();
            //reduce def by 30%
        }

        public void AtkSkill()
        {

        }

        public void BuffSKill()
        {

        }

        public void DebuffSkill()
        {

        }

        public void HealSkill()
        {

        }
        #endregion
    }
}

