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

        #region Delegates
        public delegate void TurnReady();
        public TurnReady OnTurnReady;

        public delegate void PlayerTurnEnded();
        public PlayerTurnEnded OnPlayerTurnEnded;
        #endregion

        //The GameState will dictate what events needs to be called (i.e Giving player functionality, Passing to an EnemyTurn, ending the battle, etc..)
        #region Turn Tracker Variables

        private Entity currentEntity;
        public Entity CurrentEntity { get => currentEntity; set => currentEntity = value; }

        private int unitIndex =0;
        public int UnitIndex => unitIndex;

        private int roundIndex;
        private enum GameState
        {
            BattleStart,
            BetweenTurn,
            PlayerTurn,
            EnemyTurn,
            GameWon,
            GameLoss
        }

        private static GameState currentGameState;
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
            BattleHandler.Instance.OnTimerReady += SetEntityTurn;
            BattleHandler.Instance.OnStateEnd += CheckState;
        }

        //Unsubscribe to OnBattleStart when disabled
        void OnDisable()
        {
            BattleHandler.Instance.OnTimerReady -= SetEntityTurn;
            BattleHandler.Instance.OnStateEnd -= CheckState;
        }

        // Start is called before the first frame update
        void Start()
        {
            BattleHandler.Instance.OnStateEnd.Invoke();
        }
        #endregion

        #region Methods
        #region StateMachine
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
                    //Pause all Entities' timer in UnitsInBattle
                    foreach (Entities.Entity entity in BattleHandler.Instance.UnitsInBattle)
                    {
                        entity.StartEntityTimer();
                    }
                    break;
                #endregion

                #region State: PlayerTurn
                case GameState.PlayerTurn:
                    Debug.Log("It is now " + CurrentEntity.name + " turn.");
                    OnTurnReady.Invoke();
     
                    break;
                #endregion

                #region State: EnemyTurn
                case GameState.EnemyTurn:
                    Debug.Log("It is now " + CurrentEntity.name + " turn.");
                    currentEntity.Attack(currentEntity, currentEntity.Target);
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
        #endregion

        //Once an Entity's Timer reaches its max value, pause all entity timers and set the GameState to the corresponding state
        public void SetEntityTurn(Entities.Entity entityTakingTurn)
        {
            CurrentEntity = entityTakingTurn;

            //Pause all Entities' timer in UnitsInBattle
            foreach (Entities.Entity entity in BattleHandler.Instance.UnitsInBattle)
            {
                entity.PauseEntityTimer();
            } 

            //Set the currentGameState based on weather the passsed entity is controlable
            if (entityTakingTurn.IsControlable)
            {
                currentGameState = GameState.PlayerTurn;
                BattleHandler.Instance.OnStateEnd?.Invoke();
            }
            else
            {
                currentGameState = GameState.EnemyTurn;
                BattleHandler.Instance.OnStateEnd?.Invoke();
            }
        }

        //Forces the State Machine to the between turn state
        public void SetStateBetween()
        {
            currentGameState = GameState.BetweenTurn;
            BattleHandler.Instance.OnStateEnd.Invoke();
            Debug.Log(currentGameState);
        }
        //Forces the EventHandler to invoke the OnStateEnd delegate (TO DO: Move this functionality to the agent script)
        public void InvokeStateEnd()
        {

            BattleHandler.Instance.OnStateEnd.Invoke();            
        }
        #endregion

    }
}

