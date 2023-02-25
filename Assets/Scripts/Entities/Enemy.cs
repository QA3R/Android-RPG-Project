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
    public class Enemy : Entity, IDamageable
    {
        private float TotalDMG;

        BattleManager battleManager;
        EventHandler eHandler;
        private IDamageable iDamageable;

        List <Entity> _hpList;

        #region OnEnable, OnDisable, Start
        private void OnDisable()
        {
            if (battleManager != null)
            {
                battleManager.onEnemyTurn -= Attack;;
                eHandler.OnDamageReceived -= TakeDmg;
            }
        }

        public override void Start()
        {
            base.Start();
            //Get Reference to the BattleManager
            battleManager = GameObject.FindObjectOfType<BattleManager>();
            battleManager.onEnemyTurn = Attack;

            //Get reference to the EventHandler
            eHandler = GameObject.FindObjectOfType<EventHandler>();

            iDamageable = this;

            //Sorts the list of allies by their HP value
            _hpList = new List<Entity>(battleManager.EntityScripts);
            _hpList.Sort((Ent1, Ent2) => Ent1.Hp.CompareTo(Ent2.Hp));

            //remove all uncontrolable entities
            foreach (Entity entity in _hpList.ToList())
            {
                if (entity.IsControlable == false)
                {
                    _hpList.Remove(entity);
                }
            }   
        }
        #endregion

        public override void SetSpawnPoint(BattleManager bManager, CameraManager cManager)
        {
            //base.SetSpawnPoint(bManager, cManager);
            transform.position = bManager.EnemeySpawnPoints[bManager.EnemySpawnPointNum].transform.position;
            cManager.SetupVCList(gameObject);
            bManager.EnemySpawnPointNum++;
        }

        void TakeDmg(Entity entityDamaged, float dmgTaken)
        {

        }

        #region Action Methods
        //Selects the Ally with the lowest current HP and calculates DMG dealt based on the enemy's ATK
        public override void Attack(Entity enemy) 
        {
            eHandler.OnDamageReceived = iDamageable.TakeDmg;

            if (enemy.Atk - _hpList[0].Def < enemy.Def * .3f)
            {
                //Set the dmg dealt to be 30% of the enemy's ATK
                TotalDMG = enemy.Atk * (0.3f);
                Debug.Log("Total Dmg is " + TotalDMG);

                if(eHandler.OnDamageReceived !=null)
                eHandler.OnDamageReceived.Invoke(_hpList[0], TotalDMG);
            }
            else
            {
                //Set the DMG based on the regular calculations
                TotalDMG = enemy.Atk - _hpList[0].Def;

                //Subtract the hp from the dmg dealt to the target
                _hpList[0].Hp = _hpList[0].Hp - TotalDMG;

                Debug.Log(enemy.Name + " dealt " + TotalDMG + " DMG to " + _hpList[0].Name);
            }

            //Update the list of target HP's
            _hpList.Sort((Ent1, Ent2) => Ent1.Hp.CompareTo(Ent2.Hp));
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
