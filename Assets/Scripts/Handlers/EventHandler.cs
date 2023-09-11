using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entities;
using Cinemachine;

public class EventHandler : MonoBehaviour
{
    #region Singleton Implementation
    private static EventHandler instance;
    public static EventHandler Instance => instance;
    #endregion 

    #region InputHandler delegates
    //Pings when swiping left on the device
    public delegate void SwipeLeft();
    public SwipeLeft swipeLeft;
    
    //Pings when swiping right on the device
    public delegate void SwipeRight();
    public SwipeRight swipeRight;
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
    #endregion

    #region CameraManager related delegates
    public delegate void CameraTargetChanged();
    public CameraTargetChanged OnTargetChanged;
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
