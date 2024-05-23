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


        //Pings when units are spawning into the battle
        public delegate void OnUnitSpawn(GameObject unitToSapwn);
        public OnUnitSpawn onUnitSpawn;
        #endregion

        //Lists for Units to spawn, ally spawn locations, and enemy spawn locations
        #region Lists of Units
        private Entity currentUnit;

        [SerializeField] private List<GameObject> EntityObjToSpawn;

        [SerializeField] private List<GameObject> allySpawnPoints;
        public List<GameObject> AllySpawnPoints => allySpawnPoints;

        [SerializeField] private List<GameObject> enemySpawnPoints;
        public List<GameObject> EnemySpawnPoints => enemySpawnPoints;

        private int allySpawnID = 0;
        public int AllySpawnID { get => allySpawnID; set => allySpawnID = value; }

        private int enemySpawnID = 0;
        public int EnemyBattleID { get => enemySpawnID; set => enemySpawnID = value; }

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
