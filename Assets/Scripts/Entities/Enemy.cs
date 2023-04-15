using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;
using static UnityEngine.EventSystems.EventTrigger;
using Managers;
using TMPro;
using ScriptableObjects;

namespace Entities
{
    public class Enemy : Entity
    {
        private float TotalDMG;

        #region OnEnable, OnDisable, Start
        private void OnDisable()
        {
            if (EventHandler.Instance != null)
            {
                EventHandler.Instance.onEnemyTurn -= Attack;
                EventHandler.Instance.OnDealDMG -= iDamageable.DealDMG;
            }
        }

        public override void Start()
        {
            base.Start();

            EventHandler.Instance.onEnemyTurn = Attack;
            //remove all uncontrolable entities
            foreach (Entity entity in BattleManager.Instance.unitHPList.ToList())
            {
                if (entity.IsControlable == false)
                {
                    BattleManager.Instance.unitHPList.Remove(entity);
                }
            }   
        }
        #endregion

        public override void SetSpawnPoint()
        {
            //base.SetSpawnPoint(bManager, cManager);
            transform.position = BattleManager.Instance.EnemySpawnPoints[BattleManager.Instance.EnemySpawnPointNum].transform.position;
            CameraManager.Instance.SetupVCList(this.gameObject);
            BattleManager.Instance.EnemySpawnPointNum++;
        }

 

        #region Action Methods
        //Selects the Ally with the lowest current HP and calculates DMG dealt based on the enemy's ATK
        public virtual void Attack(Entity enemy) 
        {
            if (enemy.Atk - BattleManager.Instance.unitHPList[0].Def < enemy.Def * .3f)
            {
                //Set the dmg dealt to be 30% of the enemy's ATK
                TotalDMG = enemy.Atk * (0.3f);
                Debug.Log("Total Dmg is " + TotalDMG);

                //Invoke the IDamageable TakeDmg method
                if(EventHandler.Instance.OnDealDMG !=null)
                    EventHandler.Instance.OnDealDMG.Invoke(BattleManager.Instance.unitHPList[0], TotalDMG);
            }
            else
            {
                //Set the DMG based on the regular calculations
                TotalDMG = enemy.Atk - BattleManager.Instance.unitHPList[0].Def;

                //Invoke the IDamageable TakeDmg method
                if (EventHandler.Instance.OnDealDMG != null)
                    EventHandler.Instance.OnDealDMG.Invoke(BattleManager.Instance.unitHPList[0], TotalDMG);

                Debug.Log(enemy.Name + " dealt " + TotalDMG + " DMG to " + BattleManager.Instance.unitHPList[0].Name);
            }

            //Update the list of target HP's
            BattleManager.Instance.unitHPList.Sort((Ent1, Ent2) => Ent1.Hp.CompareTo(Ent2.Hp));
        }

        #endregion
    }

}
