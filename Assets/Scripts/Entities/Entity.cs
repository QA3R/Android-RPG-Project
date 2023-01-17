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
        protected string Name;
        protected float Atk;
        protected float Def;
        protected float Spd;
        protected float Res;
        protected float Hp;

        #region Action Methods
        public virtual void Attack(EntityScriptableObject entity)
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

