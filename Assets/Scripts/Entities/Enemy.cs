using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjects;
using TMPro;
using Managers;
using System.Linq;
using Unity.VisualScripting;
using static UnityEngine.EventSystems.EventTrigger;

namespace Entities
{
    public class Enemy : Entity
    {
        private float TotalDMG;

        BattleManager battleManager;

        List <Entity> _hpList;

        public override void Start()
        {
            base.Start();
            battleManager = GameObject.FindObjectOfType<BattleManager>();
            battleManager.onEnemyAttack = Attack;

            _hpList = new List<Entity>(battleManager.EntityScripts);
            _hpList.Sort((Ent1, Ent2) => Ent1._hp.CompareTo(Ent2._hp));

            //remove all uncontrolable entities
            foreach (Entity entity in _hpList.ToList())
            {
                if (entity._isControlable == false)
                {
                    _hpList.Remove(entity);
                }
            }   
        }

        private void OnDisable()
        {
            if (battleManager != null)
            {
                battleManager.onEnemyAttack -= Attack;
            }
        }

        #region Action Methods
        //Selects the Ally with the lowest current HP and calculates DMG dealt based on the enemy's ATK
        public override void Attack(Entity enemy) 
        {
            if (enemy._atk - _hpList[0]._def < enemy._atk * .3f)
            {
                //Set the dmg dealt to be 30% of the enemy's ATK
                TotalDMG = enemy._atk * (0.3f);

                //Subtract the hp from the dmg dealt to the target
                _hpList[0]._hp = _hpList[0]._hp - TotalDMG;

                Debug.Log(enemy._name + " dealt " + TotalDMG + " DMG to " + _hpList[0]._name);
            }
            else
            {
                //Set the DMG based on the regular calculations
                TotalDMG = enemy._atk - _hpList[0]._def;

                //Subtract the hp from the dmg dealt to the target
                _hpList[0]._hp = _hpList[0]._hp - TotalDMG;

                Debug.Log(enemy._name + " dealt " + TotalDMG + " DMG to " + _hpList[0]._name);
            }

            //Update the list of target HP's
            _hpList.Sort((Ent1, Ent2) => Ent1._hp.CompareTo(Ent2._hp));
        }

        public override void AtkSkill()
        {
            base.AtkSkill();
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
