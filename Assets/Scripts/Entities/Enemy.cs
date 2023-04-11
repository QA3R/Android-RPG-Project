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
            if (battleManager != null)
            {
                eHandler.onEnemyTurn -= Attack;
                eHandler.OnDealDMG -= iDamageable.DealDMG;
            }
        }

        public override void Start()
        {
            base.Start();

            eHandler.onEnemyTurn = Attack;
            //remove all uncontrolable entities
            foreach (Entity entity in battleManager.unitHPList.ToList())
            {
                if (entity.IsControlable == false)
                {
                    battleManager.unitHPList.Remove(entity);
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

 

        #region Action Methods
        //Selects the Ally with the lowest current HP and calculates DMG dealt based on the enemy's ATK
        public virtual void Attack(Entity enemy) 
        {
            if (enemy.Atk - battleManager.unitHPList[0].Def < enemy.Def * .3f)
            {
                //Set the dmg dealt to be 30% of the enemy's ATK
                TotalDMG = enemy.Atk * (0.3f);
                Debug.Log("Total Dmg is " + TotalDMG);

                //Invoke the IDamageable TakeDmg method
                if(eHandler.OnDealDMG !=null)
                eHandler.OnDealDMG.Invoke(battleManager.unitHPList[0], TotalDMG);
            }
            else
            {
                //Set the DMG based on the regular calculations
                TotalDMG = enemy.Atk - battleManager.unitHPList[0].Def;

                //Invoke the IDamageable TakeDmg method
                if (eHandler.OnDealDMG != null)
                    eHandler.OnDealDMG.Invoke(battleManager.unitHPList[0], TotalDMG);

                Debug.Log(enemy.Name + " dealt " + TotalDMG + " DMG to " + battleManager.unitHPList[0].Name);
            }

            //Update the list of target HP's
            battleManager.unitHPList.Sort((Ent1, Ent2) => Ent1.Hp.CompareTo(Ent2.Hp));
        }

        #endregion
    }

}
