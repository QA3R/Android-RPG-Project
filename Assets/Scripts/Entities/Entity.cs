using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjects;
using Managers;

namespace Entities
{
    public class Entity : MonoBehaviour, IDamageable
    {
        [SerializeField] private EntityScriptableObject entityType;
        
        public bool IsControlable;
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
        }
        
        public virtual void SetSpawnPoint(BattleManager bManager, CameraManager cManager)
        {

        }

        #region Action Methods
        public virtual void Attack(Entity entity)
        {

        }

        public virtual void AtkSkill()
        {

        }

        public virtual void BuffSKill()
        {

        }

        public virtual void DebuffSkill()
        {

        }

        public virtual void HealSkill()
        {

        }
        #endregion
    }
}

