using Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjects;
using Managers;

namespace Entities.Enemies
{
    public class SlowWalker : Enemy
    {
        private float dmgToDeal;
        Entity minDefEntity;

        private void OnDisable()
        {
            if (EventHandler.Instance != null)
            {
                EventHandler.Instance.onEnemyTurn -= Attack;
                EventHandler.Instance.OnDealDMG -= iDamageable.DealDMG;
            }
        }

        // Start is called before the first frame update
        public override void Start()
        {
            base.Start();
        }

        //This method will be used to assign a new Action to be invoked during onEnemyTurn
        public override void MakeAction()
        {
            EventHandler.Instance.onEnemyTurn += Attack;
        }

        public override void Attack()
        {
            StartCoroutine(StartAttack());
        }

        IEnumerator StartAttack()
        {
            yield return new WaitForSeconds(2f);
            Debug.Log("Starting Attack");

            List<Entity> targetDefs = new List<Entity>() { };

            //ADD ALL UNITS IN BATTLE TO THE TARGETHPS LIST
            for (int i = 0; i < BattleManager.Instance.UnitsInBattle.Count; i++)
            {
                //CHECK IF UNIT IN THE LIST AT THE ARRAY IS CONTROLABLE
                if (BattleManager.Instance.UnitsInBattle[i].IsControlable && !IsDead)
                {
                    //ADD THE UNIT TO MY LIST OF TARGETHPS
                    targetDefs.Add(BattleManager.Instance.UnitsInBattle[i]);
                }
            }


            if (targetDefs != null)
            {
                minDefEntity = targetDefs[0];

                //FIND THE MIN VALUE ON THE LIST
                for (int i = 0; i < targetDefs.Count; i++)
                {
                    if (targetDefs[i].Def < minDefEntity.Def)
                    {
                        minDefEntity = targetDefs[i];
                    }
                }

                //TAKE THE MIN VALUE UNIT AND CALCULATE DMG
                dmgToDeal = Mathf.Abs(this.Atk - minDefEntity.Def);
                Debug.Log(minDefEntity.name + " should take " + dmgToDeal + " damage");

                //INVOKE THE TARGETHPS UNIT'S IDAMAGABLE'S TAKE DMG METHOD
                minDefEntity.iDamageable.DealDMG(minDefEntity, dmgToDeal);

                //Reset the target for the next turn
                minDefEntity = null;
            }

            EventHandler.Instance.onEnemyTurn -= Attack;

            //Cycle to the next State
            EventHandler.Instance.OnStateEnd.Invoke();
        }
    }
}

