using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entities;

public interface IDamageable
{
    void DealDMG(Entity entityDamaged, float dmgTaken)
    {
        entityDamaged.Hp = entityDamaged.Hp - dmgTaken;
        Debug.Log(entityDamaged.Name + " was damaged for " + dmgTaken);
    }
}
