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
        public float CurrentTimerVal;
        public bool IsTimerRunning;

        public virtual void Start()
        {
            CurrentTimerVal = 0;
            IsTimerRunning = true;
            Name = entityType.Name;
            Hp = entityType.MaxHP;
            Atk = entityType.Atk;
            Spd = entityType.Spd;
            Def = entityType.Def;
            Res = entityType.Res;

            IsDead = false;

            //Subscribe to the OnActionMade Event
            BattleHandler.Instance.OnDeathCheck += CheckEntityStatus;
        }

        private void OnDisable()
        {
            BattleHandler.Instance.OnDeathCheck -= CheckEntityStatus;
        }

        void Update()
        {
            if (IsTimerRunning)
            {
                CurrentTimerVal += Time.deltaTime * (10 + (Spd/100));
            }

            if (CurrentTimerVal >=10 && IsTimerRunning) 
            {
                BattleHandler.Instance.OnTimerReady.Invoke(GetComponent<Entity>());
            }
        }

        public void PauseEntityTimer()
        {
            IsTimerRunning = false;
        }

        public void ResetEntityTimer()
        {
            CurrentTimerVal= 0;
        }
        public void StartEntityTimer()
        {
            IsTimerRunning = true;
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

