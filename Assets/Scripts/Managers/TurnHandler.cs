using Unity.VisualScripting;
using System.Collections;
using UnityEngine.Rendering.Universal;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjects;
using Entities;
using Cinemachine;

namespace Managers
{
    public class TurnHandler : MonoBehaviour
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
        private static TurnHandler instance;
        public static TurnHandler Instance => instance;
        #endregion

        //The GameState will dictate what events needs to be called (i.e Giving player functionality, Passing to an EnemyTurn, ending the battle, etc..)
        #region Turn Tracker Variables
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

        private int unitIndex =0;
        public int UnitIndex => unitIndex;

        private int roundIndex;
        
        private Entities.Entity currentUnit;
        #endregion 
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
        //This is the Statemachine that checks which State the battle is in and sets the approiate actions
        void CheckState()
        {
            switch (currentGameState)
            {
                //Invoke all methods related to the start of battle (spawning units and passing to the between turn state)
                #region State: Start
                case GameState.BattleStart:
                    Debug.Log(currentGameState);
                    //Setup all Allies and Enemies in scene
                    BattleHandler.Instance.SpawnUnits();
                    break;
                #endregion

                #region State: BetweenTurn
                case GameState.BetweenTurn:
                    break;
                #endregion

                #region State: PlayerTurn
                case GameState.PlayerTurn:
                    Debug.Log("It is now the player turn.");
     
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

        //Once an Entity's Timer reaches its max value, pause all entity timers and set the GameState to the corresponding state
        void SetEntityTurn(Entities.Entity entityTakingTurn)
        {
            //Pause all Entities' timer in UnitsInBattle
            foreach (Entities.Entity entity in BattleHandler.Instance.UnitsInBattle)
            {
                entity.PauseEntityTimer();
            } 

            //Set the currentGameState based on weather the passsed entity is controlable
            if (entityTakingTurn.IsControlable)
            {
                currentGameState = GameState.PlayerTurn;
                Debug.Log(entityTakingTurn.name);
                BattleHandler.Instance.OnStateEnd?.Invoke();
            }
            else
            {
                currentGameState = GameState.EnemyTurn;
            }
        }

        //Forces the State Machine to the between turn state
        public void SetStateBetween()
        {
            currentGameState = GameState.BetweenTurn;
            Debug.Log(currentGameState);
        }
        //Forces the EventHandler to invoke the OnStateEnd delegate (TO DO: Move this functionality to the agent script)
        public void InvokeStateEnd()
        {
            BattleHandler.Instance.OnStateEnd.Invoke();            
        }
        #endregion
        #endregion

    }
}

