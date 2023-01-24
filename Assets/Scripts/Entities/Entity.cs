using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjects;
using Managers;
using UnityEditor.ShaderGraph.Internal;

namespace Entities
{
    public class Entity : MonoBehaviour
    {
        [SerializeField] private EntityScriptableObject _entityType;
        
        public bool _isControlable;
        public string _name;
        public float _atk;
        public float _def;
        public float _spd;
        public float _res;
        public float _hp;

        public virtual void Start()
        {
            _name = _entityType.Name;
            _hp = _entityType.MaxHP;
            _atk = _entityType.Atk;
            _spd = _entityType.Spd;
            _def = _entityType.Def;
            _res = _entityType.Res;

            
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

