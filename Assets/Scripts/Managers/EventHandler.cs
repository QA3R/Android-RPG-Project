using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entities;
public class EventHandler : MonoBehaviour
{
    //Event for when something takes dmg
    public delegate void DamageReceived(Entity entityTakingDmg, float damageReceived);
    public DamageReceived OnDealDMG;

    public delegate void CameraTargetChanged();
    public CameraTargetChanged OnTargetChanged;

    public delegate void ActionMade();
    public ActionMade OnActionMade;
}
