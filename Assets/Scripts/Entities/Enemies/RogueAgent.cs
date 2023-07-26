using Entities;
using Managers;
using ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueAgent : Enemy
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

    public override void Start()
    {
        base.Start();

        EventHandler.Instance.onEnemyTurn = Attack;
    }


    public override void MakeAction()
    {
        EventHandler.Instance.onEnemyTurn = Attack;
    }

    public override void Attack()
    {
        List<Entity> targetHps = new List<Entity>() { };

        //ADD ALL UNITS IN BATTLE TO THE TARGETHPS LIST
        for (int i = 0; i < BattleManager.Instance.UnitsInBattle.Count; i++)
        {
            //CHECK IF UNIT IN THE LIST AT THE ARRAY IS CONTROLABLE
            if (BattleManager.Instance.UnitsInBattle[i].IsControlable && !BattleManager.Instance.UnitsInBattle[i].IsDead)
            {
                //ADD THE UNIT TO MY LIST OF TARGETHPS
                targetHps.Add(BattleManager.Instance.UnitsInBattle[i]);
            }
        }


        Entity minHpEntity = targetHps[0];

        //FIND THE MIN VALUE ON THE LIST
        for (int i=0; i< targetHps.Count; i++)
        {
            if (targetHps[i].Hp < minHpEntity.Hp)
            {
                minHpEntity = targetHps[i];
            }
        }

        //TAKE THE MIN VALUE UNIT AND CALCULATE DMG
        dmgToDeal = Mathf.Abs(this.Atk - minHpEntity.Def);
        Debug.Log(minHpEntity.name + " should take " + dmgToDeal + " damage");

        //INVOKE THE TARGETHPS UNIT'S IDAMAGABLE'S TAKE DMG METHOD
        minHpEntity.iDamageable.DealDMG(minHpEntity, dmgToDeal);

        //REMOVE THIS ENEMY FROM THE onEnemyTurn event
        EventHandler.Instance.onEnemyTurn -= Attack;

    }


}
