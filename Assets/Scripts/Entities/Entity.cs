using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjects;
using Managers;
using Managers.Battle;

namespace Entities
{
    public class Entity : MonoBehaviour, IDamageable
    {
        #region Variables
        private string entName;
        private bool isAlliedEntity;
        private bool isControlable;
        private bool isDead;
        private float healthPoints;
        private float atk;
        private float spd;
        private float def;
        private float res;

        private STAScriptableObject BasicAtk => entityType.BasicAtk;
        private Sprite portrait;
        private Entity target;
        
        [SerializeField]private EntityScriptableObject entityType;
        [SerializeField] private TimerHandler timer;
        #endregion

        #region Properties 
        public string EntName { get => entName; set => entName = value; }
        public bool IsAlliedEntity { get => isAlliedEntity; set => isAlliedEntity = value; }
        public bool IsControlable { get => isControlable; set => isControlable = value; }
        public bool IsDead { get => isDead; set => isDead = value; }
        public float HealthPoints { get => healthPoints; set => healthPoints = value; }
        public float Atk { get => atk; set => atk = value; }
        public float Spd { get => spd; set => spd = value; }
        public float Def { get => def; set => def = value; }
        public float Res { get => res; set => res = value; }

        public Sprite Portrait { get => portrait; set => portrait = value; }
        public EntityScriptableObject EntityType { get => entityType; set => entityType = value; }
        public Entity Target { get => target; set => target = value; }
        public List<STAScriptableObject> Skill => entityType.Skills;
        public TimerHandler Timer { get => timer; set => timer = value; }
        #endregion

        #region Start, OnEnable, OnDisable, Update Methods
        public virtual void Awake()
        {
            Portrait = entityType.EntityPortrait;

            EntName = entityType.Name;
            IsControlable = entityType.IsControlable;
            IsAlliedEntity = entityType.IsAlliedEntity;

            IsDead = false;
            HealthPoints = entityType.MaxHP;
            Atk = entityType.Atk;
            Spd = entityType.Spd;
            Def = entityType.Def;
            Res = entityType.Res;

            if (this.gameObject.GetComponent<TimerHandler>() != null)
            {
                Timer = this.gameObject.GetComponent<TimerHandler>();
            }
        }

        void Update()
        {

        }
        #endregion

        #region Battle Start Methods
        public virtual void SetSpawnPoint()
        {

            if (this.entityType.IsControlable)
            {
                transform.position = BattleManager.Instance.AllySpawnPoints[BattleManager.Instance.AllySpawnID].transform.position;
                BattleManager.Instance.AllySpawnID++;
            }
            else
            {
                transform.position = BattleManager.Instance.EnemySpawnPoints[BattleManager.Instance.EnemyBattleID].transform.position;
                BattleManager.Instance.EnemyBattleID++;
            }
        }
        #endregion

        #region Damage Related Methods
        //Assign a caster and call the BasicAtk.ExecuteSkill()
        public virtual void Attack(Entity caster, Entity receiver)
        {
            caster = this;

            if (Target != null)
            {
                //Calls the caster's BasicAtk.ExecuteSkill method that exists in the STASScriptableObject attached to it
                BasicAtk.ExecuteSkill(caster, Target);
            }

            //Once Attack function has been completed, we call the SetStateBetween 
            TurnManager.Instance.SetStateBetween();
        }

        public virtual void ReceiveDmg(float dmgToDeal)
        {
            HealthPoints -= dmgToDeal;

            if (HealthPoints <= 0)
            {
                if (Timer != null)
                {
                    Timer.ResetEntityTimer();
                }

                MarkAsDead();
            }
        }

        public virtual void MarkAsDead()
        {
            //Mark the Entity as dead
            IsDead = true;

            Timer.IsTimerRunning = false;

            //Disable the Meshrenderer of the cube
            gameObject.SetActive(false);
        }
        #endregion
    }

}

