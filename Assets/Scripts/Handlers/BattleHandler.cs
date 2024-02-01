using Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleHandler : MonoBehaviour
{
    #region Singleton Implementation
    private static BattleHandler instance;
    public static BattleHandler Instance => instance;
    #endregion

    #region BattleManager related delegates
    public delegate void StateEnd();
    public StateEnd OnStateEnd;

    //Pings when units are spawning into the battle
    public delegate void OnUnitSpawn(GameObject unitToSapwn);
    public OnUnitSpawn onUnitSpawn;

    //Pings to set the Main Camera to the correct unit's Camera
    public delegate void PlayerTurn();
    public PlayerTurn onPlayerTurn;

    //The Enemy.cs script will invoke the Attack Method to attack the lowest HP ally unit
    public delegate void EnemyTurn();
    public EnemyTurn onEnemyTurn;

    //Event for when something takes dmg
    public delegate void DamageReceived(Entity entityTakingDmg, float damageReceived);
    public DamageReceived OnDealDMG;
    //The Entity.cs Script will invoke its CheckEntityStatus method to determine if it is dead or not
    public delegate void DeathCheck();
    public DeathCheck OnDeathCheck;

    public delegate void EntityTimerReady(Entity entityTakingTurn);
    public EntityTimerReady OnTimerReady;

    #endregion


    //Singleton Implementation
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
