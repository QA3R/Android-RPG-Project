using Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjects;
using Managers;

namespace Entities.Enemies
{
    public class EnemyTestEntity2 : EnemyEntity
    {
        private float dmgToDeal;
        Entity minDefEntity;

        private void OnDisable()
        {
        }

        // Start is called before the first frame update
        public override void Start()
        {
            base.Start();
        }

        public override void Attack(Entity caster, Entity receiver)
        {
            base.Attack(caster, receiver);
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

