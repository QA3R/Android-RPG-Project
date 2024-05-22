using Entities;
using Managers;
using ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTestEntity : EnemyEntity
{
    private float dmgToDeal;
    Entity minHpEntity;
    private void OnDisable()
    {
    }

    public override void Start()
    {
        base.Start();
    }

    public override void Attack(Entity caster, Entity receiver)
    {
        base.Attack(caster, receiver);
    }
}
