using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjects;
using Managers;

namespace Entities
{
    public class Entity : MonoBehaviour, IDamageable
    {
        #region Variables
        [SerializeField] private EntityScriptableObject entityType;

        public STAScriptableObject BasicAtk => entityType.BasicAtk;
        public List<STAScriptableObject> Skill => entityType.Skills;

        public bool IsAlliedEntity;
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

        private Entity target;
        public Entity Target { get => target; set => target = value; }
        #endregion

        #region Start, OnEnable, OnDisable, Update Methods
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
                CurrentTimerVal += Time.deltaTime * (Spd/10);
            }

            if (CurrentTimerVal >=1 && IsTimerRunning) 
            {
                BattleHandler.Instance.OnTimerReady?.Invoke(this);
                
                ResetEntityTimer();
            }
        }
        #endregion

        #region Battle Start Methods
        public virtual void SetSpawnPoint()
        {

        }
        #endregion

        #region Entity Timer Methods
        public void StartEntityTimer()
        {
            IsTimerRunning = true;
        }
        public void PauseEntityTimer()
        {
            IsTimerRunning = false;
        }

        public void ResetEntityTimer()
        {
            CurrentTimerVal= 0;
        }
        #endregion

        #region Damage Related Methods
        //Assign a caster and call the BasicAtk.ExecuteSkill()
        public virtual void Attack(Entity caster, Entity receiver)
        {
            caster = this;

            //Remove this when refactoring Targeting system
            if (Target == null)
            {
                Target = BattleHandler.Instance.UnitsInBattle[0];
            }

            //Calls the caster's BasicAtk.ExecuteSkill method that exists in the STASScriptableObject attached to it
            BasicAtk.ExecuteSkill(this, Target);

            //Once Attack function has been completed, we call the SetStateBetween 
            TurnHandler.Instance.SetStateBetween();
        }

        public void DealDMG(Entity entityDamaged, float dmgTaken)
        {
            entityDamaged.Hp = entityDamaged.Hp - dmgTaken;
            Debug.Log(entityDamaged.Name + " was damaged for " + dmgTaken);
        }

        public virtual void CheckEntityStatus()
        {
            if (Hp <= 0)
            {
                IsDead = true;
                RemoveEntity();
            }
        }
     
        //Disables MeshRenderer
        public virtual void RemoveEntity() 
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
        #endregion
    }

}

