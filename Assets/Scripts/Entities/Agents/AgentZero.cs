using Entities;
using ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Entities.Agents
{
    public class AgentZero : Agent
    {
        [SerializeField] private EntityScriptableObject entityType;

        public AgentZero() 
        {
            Name = "";
            Hp = 1f;
            Atk = 1f;
            Spd = 1f;
            Def = 1f;
            Res = 1f;
        }

        // Start is called before the first frame update
        void Start()
        {
            Name = entityType.Name;
            Hp = entityType.MaxHP;
            Atk = entityType.Atk;
            Spd = entityType.Spd;
            Def = entityType.Def;
            Res = entityType.Res;
        }

        // Update is called once per frame
        void Update()
        {

        }

        #region Action Methods
        public override void Attack(EntityScriptableObject entity)
        {

        }

        public override void AtkSkill()
        {

        }

        public override void BuffSKill()
        {

        }

        public override void DebuffSkill()
        {

        }

        public override void HealSkill()
        {

        }
        #endregion
    }
}

