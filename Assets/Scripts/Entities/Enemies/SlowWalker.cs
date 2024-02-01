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
            if (BattleHandler.Instance != null)
            {
                BattleHandler.Instance.onEnemyTurn -= Attack;
                BattleHandler.Instance.OnDealDMG -= DealDMG;
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
            BattleHandler.Instance.onEnemyTurn += Attack;
        }

        public override void Attack()
        {
            StartCoroutine(StartAttack());
        }

        IEnumerator StartAttack()
        {
            yield return new WaitForSeconds(2f);
            Debug.Log("Starting Attack");


            //Cycle to the next State
            BattleHandler.Instance.OnStateEnd.Invoke();
        }
    }
}

