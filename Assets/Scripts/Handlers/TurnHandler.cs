using Unity.VisualScripting;
using System.Collections;
using UnityEngine.Rendering.Universal;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjects;
using Entities;
using Cinemachine;

namespace Handlers
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

        public delegate void EntityTimerReady(Entity entityTakingTurn);
        public EntityTimerReady OnEntityTimerReady;

        public delegate void EntityTurnSet();
        public EntityTurnSet OnEntityTurnSet;

        public delegate void EntityTurnEnd();
        public EntityTurnEnd OnEntityTurnEnd;

        public delegate void StateEnd();
        public StateEnd OnStateEnd;

        public delegate void TargetSelected(Entity entity);
        public TargetSelected OnTargetSelected;

        public delegate void BattleVictory();
        public BattleVictory OnBattleVictory;

        public delegate void BattleLoss();
        public BattleLoss OnBattleLoss;
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
            TurnHandler.Instance.OnEntityTimerReady += SetEntityTurn;
            TurnHandler.Instance.OnStateEnd += CheckState;

            //Subscribe to the function which sets the Target of the CurrentEntity on the TurnHandler to the targetSelected by the InputHandler
            OnTargetSelected += SetCEntityTarget;

            //Subscribe to the function which sets the currentGameState to GameState.BetweenTurn on the end of an Entity's turn
            OnEntityTurnEnd += SetStateBetween;
        }

        //Unsubscribe to OnBattleStart when disabled
        void OnDisable()
        {
            TurnHandler.Instance.OnEntityTimerReady -= SetEntityTurn;
            TurnHandler.Instance.OnStateEnd -= CheckState;
            OnTargetSelected -= SetCEntityTarget;
            OnEntityTurnEnd -= SetStateBetween;
        }

        // Start is called before the first frame update
        void Start()
        {
            TurnHandler.Instance.OnStateEnd.Invoke();
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

                    if (BattleHandler.Instance.HasPlayerWon())
                    {
                        StopEntityTimers();
                        OnBattleVictory.Invoke();
                        currentGameState = GameState.GameWon;
                    }
                    else if (BattleHandler.Instance.HasPlayerLost())
                    {
                        StopEntityTimers();
                        OnBattleLoss.Invoke();
                        currentGameState = GameState.GameLoss;
                    }
                    else
                    {
                        //Unpause all Entities' timer in UnitsInBattle
                        foreach (Entities.Entity entity in BattleHandler.Instance.PlayableUnitsInBattle)
                        {
                            entity.StartEntityTimer();
                        }
                        //Unpause all Entities' timer in UnitsInBattle
                        foreach (Entities.Entity entity in BattleHandler.Instance.EnemyUnitsInBattle)
                        {
                            entity.StartEntityTimer();
                        }
                    }
                    break;
                #endregion

                #region State: PlayerTurn
                case GameState.PlayerTurn:
                    Debug.Log("It is now " + CurrentEntity.name + " turn.");
                    OnEntityTurnSet.Invoke();
     
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

            StopEntityTimers();

            //Set the currentGameState based on weather the passsed entity is controlable
            if (entityTakingTurn.IsControlable)
            {
                currentGameState = GameState.PlayerTurn;
                TurnHandler.Instance.OnStateEnd?.Invoke();
            }
            else
            {
                currentGameState = GameState.EnemyTurn;
                TurnHandler.Instance.OnStateEnd?.Invoke();
            }
        }

        //Forces the State Machine to the between turn state
        public void SetStateBetween()
        {
            currentGameState = GameState.BetweenTurn;
            TurnHandler.Instance.OnStateEnd.Invoke();
            Debug.Log(currentGameState);
        }
        
        public void SetCEntityTarget(Entity selectedTarget)
        {
            if (currentGameState == GameState.PlayerTurn)
            {
                CurrentEntity.Target = selectedTarget;
            }
        }

        public void StopEntityTimers()
        {
            //Pause all Entities' timer in UnitsInBattle
            foreach (Entities.Entity entity in BattleHandler.Instance.PlayableUnitsInBattle)
            {
                entity.PauseEntityTimer();
            }
            //Pause all Entities' timer in UnitsInBattle
            foreach (Entities.Entity entity in BattleHandler.Instance.EnemyUnitsInBattle)
            {
                entity.PauseEntityTimer();
            }
        }
        #endregion

    }
}

