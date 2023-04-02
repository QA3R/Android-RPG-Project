using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entities;

public interface IDamageable
{
    void TakeDmg(Entity entityDamaged, float dmgTaken)
    {
        if (entityDamaged.Hp <= 0)
        {
            entityDamaged.IsDead = true;
        }
        else if (entityDamaged.IsDead == false)
        {
            entityDamaged.Hp = entityDamaged.Hp - dmgTaken;
            Debug.Log(entityDamaged.Name + " was damaged for " + dmgTaken);
        }
    }
}
