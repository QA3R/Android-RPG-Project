using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entities;
using Cinemachine;

public class EventHandler : MonoBehaviour
{
    //Pings when units are spawning into the battle
    public delegate void OnUnitSpawn(GameObject unitToSapwn);
    public OnUnitSpawn onUnitSpawn;

    //Pings to set the Main Camera to the correct unit's Camera
    public delegate void PlayerTurn();
    public PlayerTurn onPlayerTurn;

    public delegate void EnemyTurn(Entity entity);
    public EnemyTurn onEnemyTurn;

    //Event for when something takes dmg
    public delegate void DamageReceived(Entity entityTakingDmg, float damageReceived);
    public DamageReceived OnDealDMG;

    public delegate void CameraTargetChanged();
    public CameraTargetChanged OnTargetChanged;

    public delegate void ActionMade();
    public ActionMade OnActionMade;

}
