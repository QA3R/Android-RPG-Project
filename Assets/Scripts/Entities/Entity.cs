using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjects;
using Managers;

namespace Entities
{
    public class Entity : MonoBehaviour, IDamageable, IUnit
    {
        [SerializeField] private EntityScriptableObject entityType;

        protected BattleManager battleManager;
        protected EventHandler eHandler;
        public IDamageable iDamageable;
        
        public bool IsControlable;
        public bool IsDead;
        public string Name;
        public float Atk;
        public float Def;
        public float Spd;
        public float Res;
        public float Hp;

        public virtual void Start()
        {
            Name = entityType.Name;
            Hp = entityType.MaxHP;
            Atk = entityType.Atk;
            Spd = entityType.Spd;
            Def = entityType.Def;
            Res = entityType.Res;
            IsDead = false;
           
            //Get Reference to BattleManager, EventHandler, and IDamageable
            battleManager = GameObject.FindObjectOfType<BattleManager>();
            eHandler = GameObject.FindObjectOfType<EventHandler>();
            iDamageable = this;
        }

        public virtual void SetSpawnPoint(BattleManager bManager, CameraManager cManager)
        {

        }
    }
}

