using Entities;
using Managers;
using ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueAgent : Enemy
{
    private float dmgToDeal;
    Entity minHpEntity;
    private void OnDisable()
    {
        if (BattleHandler.Instance != null)
        {
            BattleHandler.Instance.onEnemyTurn -= Attack;
            BattleHandler.Instance.OnDealDMG -= DealDMG;
        }
    }

    public override void Start()
    {
        base.Start();
    }


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
