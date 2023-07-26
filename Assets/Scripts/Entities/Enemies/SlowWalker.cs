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

            EventHandler.Instance.onEnemyTurn = Attack;
        }

        //This method will be used to assign a new Action to be invoked during onEnemyTurn
        public override void MakeAction()
        {
            EventHandler.Instance.onEnemyTurn = Attack;
        }

        public override void Attack()
        {
            List<Entity> targetDefs = new List<Entity>() { };

            //ADD ALL UNITS IN BATTLE TO THE TARGETHPS LIST
            for (int i = 0; i < BattleManager.Instance.UnitsInBattle.Count; i++)
            {
                //CHECK IF UNIT IN THE LIST AT THE ARRAY IS CONTROLABLE
                if (BattleManager.Instance.UnitsInBattle[i].IsControlable)
                {
                    //ADD THE UNIT TO MY LIST OF TARGETHPS
                    targetDefs.Add(BattleManager.Instance.UnitsInBattle[i]);
                }
            }

            Entity minDefEntity = targetDefs[0];

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

            //REMOVE THIS ENEMY FROM THE onEnemyTurn event
            EventHandler.Instance.onEnemyTurn -= Attack;
        }

        public void AtkSkill()
        {
            //New code
            //Debuff enemy
        }
    }
}

