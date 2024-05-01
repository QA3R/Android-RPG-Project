using Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


namespace Managers
{
    public class BattleHandler : MonoBehaviour
    {
        #region Singleton Implementation
        private static BattleHandler instance;
        public static BattleHandler Instance => instance;
        #endregion

        #region BattleManager related delegates
        public delegate void StateEnd();
        public StateEnd OnStateEnd;

        //Pings when units are spawning into the battle
        public delegate void OnUnitSpawn(GameObject unitToSapwn);
        public OnUnitSpawn onUnitSpawn;

        //Pings to set the Main Camera to the correct unit's Camera
        public delegate void PlayerTurn();
        public PlayerTurn onPlayerTurn;

        //The Enemy.cs script will invoke the Attack Method to attack the lowest HP ally unit
        public delegate void EnemyTurn();
        public EnemyTurn onEnemyTurn;

        public delegate void ActionMade();
        public ActionMade OnActionMade;

        //Event for when something takes dmg
        public delegate void DamageReceived(Entity entityTakingDmg, float damageReceived);
        public DamageReceived OnDealDMG;

        //The Entity.cs Script will invoke its CheckEntityStatus method to determine if it is dead or not
        public delegate void DeathCheck();
        public DeathCheck OnDeathCheck;

        public delegate void EntityTimerReady(Entity entityTakingTurn);
        public EntityTimerReady OnTimerReady;
        #endregion

        //Lists for Units to spawn, ally spawn locations, and enemy spawn locations
        #region Lists of Units
        private Entity currentUnit;

        [SerializeField] private List<GameObject> EntityObjToSpawn;

        [SerializeField] private List<GameObject> _allySpawnPoints;
        public List<GameObject> AllySpawnPoints => _allySpawnPoints;

        [SerializeField] private List<GameObject> _enemySpawnPoints;
        public List<GameObject> EnemySpawnPoints => _enemySpawnPoints;

        private int allySpawnPointNum = 0;
        public int AllySpawnPointNum { get => allySpawnPointNum; set => allySpawnPointNum = value; }

        private int enemySpawnPointNum = 0;
        public int EnemySpawnPointNum { get => enemySpawnPointNum; set => enemySpawnPointNum = value; }

        //Lists of Units in battle, only Allies, and only Enemies
        public List<Entity> UnitsInBattle;
        #endregion

        //Singleton Implementation
        private void Awake()
        {
        
            if (instance == null & instance != this)
            {
                instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        //Takes the list of EntityObjToSpawn and sets them up in the battlefield
        public void SpawnUnits()
        {
            //Spawns the Gameobject from the list of Entities in the Battelfield and Sorts the list of EntityScripts based on the spd value on it
            foreach (GameObject unitObj in EntityObjToSpawn)
            {
                if (unitObj != null)
                    //Spawn the entity 
                    currentUnit = Instantiate(unitObj.GetComponent<Entity>());
                //Add the Entity Script from currentEntity to the EntityScripts List
                UnitsInBattle.Add(currentUnit.GetComponent<Entity>());
            }

            //Set the postions of each entity to its correct spawn location
            foreach (Entity unit in UnitsInBattle)
            {
                unit.SetSpawnPoint();
            }
        }

        bool HasPlayerLost()
        {
            //Check if all allies are dead
            bool anyPlayerUnitNotDead = BattleHandler.Instance.UnitsInBattle.Any(Entity => Entity.IsControlable && !Entity.IsDead);

            return !anyPlayerUnitNotDead;
        }
    }
}
