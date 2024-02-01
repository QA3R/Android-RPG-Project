using Unity.VisualScripting;
using System.Collections;
using UnityEngine.Rendering.Universal;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using ScriptableObjects;
using Entities;
using Cinemachine;

namespace Managers
{
    public class TurnManager : MonoBehaviour
    {
        /// <summary>
        /// This script is responsible for managing the battle status. It will keep track of:
        /// - which unit's turn it is
        /// - Which round of the battle the game is in 
        /// - Win and loss conditions
        /// </summary>
        /// 

        #region Variables
        //Instance implementation
        #region Instance Implmentation
        private static TurnManager instance;
        public static TurnManager Instance => instance;
        #endregion

        //The GameState will dictate what events needs to be called (i.e Giving player functionality, Passing to an EnemyTurn, ending the battle, etc..)
        private enum GameState
        {
            BattleStart,
            BetweenTurn,
            PlayerTurn,
            EnemyTurn,
            GameWon,
            GameLoss
        };

        private static GameState currentGameState;

        #region Turn Tracker Variables
        private int unitIndex;
        public int UnitIndex => unitIndex;

        private int roundIndex;
        
        private Entity currentUnit;
        #endregion 

        //Lists for Units to spawn, ally spawn locations, and enemy spawn locations
        #region Lists of Units
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
        public List <Entity> UnitsInBattle;
        #endregion

        //Used by the BattleCameraHandler to change target 
        public GameObject ChildObj;
        #endregion

        #region Awake & Start
        //Singleton implementation
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

        //Subscribe to OnBattleStart when Enabled
        private void OnEnable()
        {
            currentGameState = GameState.BattleStart;
            BattleHandler.Instance.OnStateEnd += CheckState;
            BattleHandler.Instance.OnTimerReady += SetEntityTurn;
        }

        //Unsubscribe to OnBattleStart when disabled
        void OnDisable()
        {
            BattleHandler.Instance.OnStateEnd -= CheckState;
            BattleHandler.Instance.OnTimerReady -= SetEntityTurn;
        }

        // Start is called before the first frame update
        void Start()
        {
            BattleHandler.Instance.OnStateEnd.Invoke();
        }
        #endregion

        #region Methods

        #region State Related Methods
        //This is the main method that will get called when a battle begins
        void CheckState()
        {
            switch (currentGameState)
            {
                //Invoke all methods related to the start of battle (spawning units and passing to the between turn state)
                #region State: Start
                case GameState.BattleStart:
                    Debug.Log(currentGameState);
                    //Setup all Allies and Enemies in scene
                    SpawnUnits();
                    break;
                #endregion

                #region State: BetweenTurn
                case GameState.BetweenTurn:
                    break;
                #endregion

                #region State: PlayerTurn
                case GameState.PlayerTurn:
                    Debug.Log("It is now the player turn.");

                    //Enable camera controls for the player
                    ChildObj = UnitsInBattle[unitIndex].transform.GetChild(0).gameObject;
                    BattleCameraHandler.Instance.IsControllable = true;
                    break;
                #endregion

                #region State: EnemyTurn
                case GameState.EnemyTurn:
                    Debug.Log("It is now the enemy turn.");
                    break;
                #endregion

                #region State: GameLoss
                case GameState.GameLoss:
                    Debug.Log("You lose. The game is now over.");
                    break;
                #endregion

                #region State: GameWon
                case GameState.GameWon:
                    break;
                    #endregion
            }
        }

        bool HasPlayerLost()
        {
            //Check if all allies are dead
            bool anyPlayerUnitNotDead = UnitsInBattle.Any(Entity => Entity.IsControlable && !Entity.IsDead);

            return !anyPlayerUnitNotDead;
        }

        //Forces the EventHandler to invoke the OnStateEnd delegate (TO DO: Move this functionality to the agent script)
        public void InvokeStateEnd()
        {
            BattleHandler.Instance.OnStateEnd.Invoke();            
        }

        public void SetStateBetween()
        {
            currentGameState = GameState.BetweenTurn;
            Debug.Log(currentGameState);
        }
        #endregion

        //Takes the list of EntityObjToSpawn and sets them up in the battlefield
        void SpawnUnits()
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
       
        //Once an Entity's Timer reaches its max value, pause all entity timers and set the GameState to the corresponding state
        void SetEntityTurn(Entity entityTakingTurn)
        {
            //Pause all Entities' timer in UnitsInBattle
            foreach (Entity entity in UnitsInBattle)
            {
                entity.PauseEntityTimer();
            } 

            //Set the currentGameState based on weather the passsed entity is controlable
            if (entityTakingTurn.IsControlable)
            {
                currentGameState = GameState.PlayerTurn;
                Debug.Log(entityTakingTurn.name);
            }
            else
            {
                currentGameState = GameState.EnemyTurn;
            }
        }

        #endregion

    }
}

