using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjects;
using TMPro;
using Managers;

namespace Entities
{
    public class Enemy : Entity
    {
        private Entity currentEntity;
        private Entity targetEntity;

        private float TotalDMG;

        BattleManager battleManager;

        public override void Start()
        {
            base.Start();
            battleManager = GameObject.FindObjectOfType<BattleManager>();
            battleManager.AttackAlly += Attack;
        }

        private void OnDisable()
        {
            if (battleManager != null)
            {
                battleManager.AttackAlly -= Attack;
            }
        }

        #region Action Methods
        //Selects the Ally with the lowest current HP and calculates DMG dealt based on the enemy's ATK
        public override void Attack(Entity enemy) 
        {
            
            if (battleManager = null)
            {
                battleManager = GameObject.FindObjectOfType<BattleManager>();
            }

            //Assigns the current 
            currentEntity = enemy;

            //Get the partymember with the least HP to the top of the list
            battleManager.EntityScripts.Sort((Ent1, Ent2) => Ent1._hp.CompareTo(Ent2._hp));

            //Assign the party member with the lowest SPD to be the targetEntity
            if (battleManager.EntityScripts.Count > 0)
            {
                targetEntity = battleManager.EntityScripts[0];

                //Get the dmg that will be dealt to the target assigned to this int
                TotalDMG = TotalDMG - targetEntity._def;

                if (TotalDMG < 0)
                {
                    TotalDMG = 0;
                }

                //Subtract the hp from the dmg dealt to the target
                targetEntity._hp = targetEntity._hp - TotalDMG;

                Debug.Log("Attack");

                //Ping to do DMG to party members when DMG is calculated
            }
            
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
