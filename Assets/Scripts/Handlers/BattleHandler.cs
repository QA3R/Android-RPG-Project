using Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


namespace Handlers
{
    public class BattleHandler : MonoBehaviour
    {
        #region Singleton Implementation
        private static BattleHandler instance;
        public static BattleHandler Instance => instance;
        #endregion

        #region BattleManager related delegates
        //Pings when units are spawning into the battle
        public delegate void OnUnitSpawn(GameObject unitToSapwn);
        public OnUnitSpawn onUnitSpawn;
        #endregion

        //Lists for Units to spawn, ally spawn locations, and enemy spawn locations
        #region Lists of Units
        private Entity currentUnit;
        private int allySpawnID = 0;
        private int enemySpawnID = 0;

        [SerializeField] private List<GameObject> EntityObjToSpawn;
        [SerializeField] private List<GameObject> allySpawnPoints;
        [SerializeField] private List<GameObject> enemySpawnPoints;

        public List<Entity> UnitsInBattle;

        public List<Entity> PlayableUnitsInBattle;

        public List<Entity> EnemyUnitsInBattle;

        public List<GameObject> AllySpawnPoints => allySpawnPoints;
        public List<GameObject> EnemySpawnPoints => enemySpawnPoints;
        public int AllySpawnID { get => allySpawnID; set => allySpawnID = value; }
        public int EnemyBattleID { get => enemySpawnID; set => enemySpawnID = value; }



        #endregion

        //Singleton Implementation
        private void Awake()
        {
            if (instance == null)
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
                {
                    //Spawn the entity 
                    currentUnit = Instantiate(unitObj.GetComponent<Entity>());
                }

                if (currentUnit.IsControlable)
                {
                    PlayableUnitsInBattle.Add(currentUnit.GetComponent<Entity>());
                }
                else
                {
                    EnemyUnitsInBattle.Add(currentUnit.GetComponent<Entity>());
                }
            }

            //Set the postions of each entity to its correct spawn location
            foreach (Entity unit in PlayableUnitsInBattle)
            {
                unit.SetSpawnPoint();
            }
            foreach (Entity unit in EnemyUnitsInBattle)
            {
                unit.SetSpawnPoint();
            }
        }

        public bool HasPlayerLost()
        {
            //Check if all allies are dead
            bool anyPlayerUnitNotDead = BattleHandler.Instance.PlayableUnitsInBattle.Any(Entity => Entity.IsControlable && !Entity.IsDead);

            return !anyPlayerUnitNotDead;
        }

        public bool HasPlayerWon()
        { 
            //Check if all allies are dead
            bool anyEnemyUnitNotDead = BattleHandler.Instance.EnemyUnitsInBattle.Any(Entity => !Entity.IsControlable && !Entity.IsDead);

            return !anyEnemyUnitNotDead;
        }
    }
}
