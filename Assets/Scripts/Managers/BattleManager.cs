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
    public class BattleManager : MonoBehaviour
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
        private static BattleManager instance;
        public static BattleManager Instance => instance;
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

        //The currentGameState will be a static variable that all scripts will get access to in order to determine which GameState we are in during a battle
        private static GameState currentGameState;

        private int unitIndex;
        public int UnitIndex => unitIndex;

        private int roundIndex;

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
   
        //List of Allies sorted by HP 
        //(TO DO: Move this functionality to Enemy script)

        #endregion

        //Variable for the currentEntity and its script
        private Entity currentUnit;
                
        //UI Elements to display DMG text and turn status (TO DO: Remove this later)
        [SerializeField] private TextMeshProUGUI statusTxt;
        [SerializeField] private TextMeshProUGUI DMGTxt;

        //TO DO: Make this a get/set
        public GameObject ChildObj;

        //TO DO: Move this to a separate UIController script
        [SerializeField] GameObject battlePanel;
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
            EventHandler.Instance.OnStateEnd += CheckState;
        }

        //Unsubscribe to OnBattleStart when disabled
        void OnDisable()
        {
            //EventHandler.Instance.OnStateEnd += ClearTurnPos;
            EventHandler.Instance.OnStateEnd -= CheckState;

        }

        // Start is called before the first frame update
        void Start()
        {
            EventHandler.Instance.OnStateEnd.Invoke();
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

                    unitIndex = 0;

                    //Sorts the list of Entity scripts by the speed value on each one
                    UnitsInBattle.Sort((Ent1, Ent2) => Ent1.Spd.CompareTo(Ent2.Spd));
                    
                    //Set the State to Between Turns
                    currentGameState = GameState.BetweenTurn;

                    //Invoke the OnCheckState Event
                    EventHandler.Instance.OnStateEnd.Invoke();

                    break;
                #endregion

                ///This State will be responsible for determining which state to move to next
                ///and will handle all logic related to this case (which state comes next? Player:Enemy:GameLoss:GameWon?)
                #region State: BetweenTurn
                case GameState.BetweenTurn:

                    if (HasPlayerLost())
                    {
                        currentGameState = GameState.GameLoss;
                    }

                    //If we are at the end of the list of UnitsInBattle reset the unitIndex counter and increase our roundIndex tracker
                    if (unitIndex >= UnitsInBattle.Count)
                    {
                        unitIndex = 0;
                        roundIndex++;
                    }

                    //Set the currentUnit to the the entity at the unitIndex of the UnitsInBattle list
                    currentUnit = UnitsInBattle[unitIndex];

                    //If the current unit is controllable and alive, set currentGameState to the PlayerTurnState
                    if (currentUnit.IsControlable && !currentUnit.IsDead)
                    {
                        currentGameState = GameState.PlayerTurn;
                        EventHandler.Instance.OnStateEnd?.Invoke();
                    }
                    //if the current unit is not controlable and alive, set currentGameState to the EnemyTurn state
                    else if (!currentUnit.IsControlable && !currentUnit.IsDead)
                    {
                        currentGameState = GameState.EnemyTurn;
                        EventHandler.Instance.OnStateEnd?.Invoke();
                    }
                    //If the current unit is not alive, loop the State Machine by invoking the OnStateEnd delegate
                    else
                    {
                        unitIndex++;
                        EventHandler.Instance.OnStateEnd?.Invoke();
                    }

                    break;
                #endregion

                ///<summary>
                ///
                ///The PlayerTurn state will handle invoking all methods related to 
                ///enabling the Player's ability to control and make their action on their turn
                ///
                ///<summary>
                #region State: PlayerTurn
                case GameState.PlayerTurn:
                    Debug.Log("It is now " + UnitsInBattle[UnitIndex].name + " turn.");

                    ///<summary>
                    ///
                    /// Pass the CinemachineVirtualCamera to the CameraManager 
                    ///to set the MainCamera to use the correct VirtualCamera
                    ///
                    ///<summary>

                    ChildObj = UnitsInBattle[unitIndex].transform.GetChild(0).gameObject;
                    EventHandler.Instance.onPlayerTurn.Invoke();

                    //Enable camera controls for the player
                    CameraManager.Instance.IsControllable = true;

                    //Toggle the controls panels for the player
                    battlePanel.SetActive(true);

                    unitIndex++;
                  
                    break;
                #endregion

                ///<summary>
                ///
                /// The EnemyTurn state will invoke the Enemy whose turn it is to make it's action 
                ///(NEED TO PROPER IMPLEMENT ENEMY AI)
                ///
                ///<summary>
                #region State: EnemyTurn
                case GameState.EnemyTurn:
                    ///<summary>
                    ///
                    ///Disable the camera controls for the player 
                    ///
                    ///TO DO: 
                    ///Move this functionality to the CameraManager
                    ///and have the CameraManager change the IsControlable based on the state we are in
                    ///
                    ///<summary>
                    Debug.Log("It is now " + UnitsInBattle[UnitIndex].name + " turn.");

                    CameraManager.Instance.IsControllable = false;

                    //Setup Enemy Action
                    UnitsInBattle[unitIndex].MakeAction();

                    //Ping the AttackAlly delegate for the enemy to attack (NEEDS TO BE FIXED)
                    EventHandler.Instance.onEnemyTurn?.Invoke();
                    
                    //Check for dead units
                    EventHandler.Instance.OnDeathCheck?.Invoke();

                    currentGameState = GameState.BetweenTurn;

                    unitIndex++;
                    
                    //Cycle to the next State
                    EventHandler.Instance.OnStateEnd.Invoke();
                    
                    
                    break;
                #endregion

                //When this state is called, we will invoke all methods related to ending the battle such as: bringing the Player back the Main Menu, removing all Player control functionality, etc.. (NEEDS TO BE IMPLEMENTED)
                #region State: GameLoss
                case GameState.GameLoss:
                    Debug.Log("You lose. The game is now over.");
                    break;
                #endregion

                //When this state is called, we will invoke all methods related to ending the battle and rewarding the Player for winning (NEEDS TO BE IMPLEMENTED)
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
            EventHandler.Instance.OnStateEnd.Invoke();            
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
       
        #endregion

    }
}

