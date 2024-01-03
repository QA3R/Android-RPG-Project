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

            //Subscribe to the OnActionMade Event
            EventHandler.Instance.OnDeathCheck += CheckEntityStatus;
        }

        private void OnDisable()
        {
            EventHandler.Instance.OnDeathCheck -= CheckEntityStatus;
        }

        public void DealDMG(Entity entityDamaged, float dmgTaken)
        {
            entityDamaged.Hp = entityDamaged.Hp - dmgTaken;
            Debug.Log(entityDamaged.Name + " was damaged for " + dmgTaken);
        }

        public virtual void SetSpawnPoint()
        {

        }

        public virtual void CheckEntityStatus()
        {
            if (Hp <= 0)
            {
                IsDead = true;
                RemoveEntity();
            }
        }

        public virtual void MakeAction()
        {

        }

        public virtual void Attack()
        {

        }

        //FIGURE OUT WHAT TO DO HERE
        public virtual void RemoveEntity() 
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
    }
}

