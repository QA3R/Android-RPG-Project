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
        //Lists for Units to spawn, ally spawn locations, and enemy spawn locations
        [SerializeField] private List<GameObject> EntityObjToSpawn;

        [SerializeField] private List<GameObject> _allySpawnPoints;
        public List<GameObject> AllySpawnPoints => _allySpawnPoints;

        [SerializeField] private List<GameObject> _enemySpawnPoints;
        public List<GameObject> EnemeySpawnPoints => _enemySpawnPoints;

        private int allySpawnPointNum = 0;
        public int AllySpawnPointNum { get => allySpawnPointNum; set => allySpawnPointNum = value; }
        
        private int enemySpawnPointNum = 0;
        public int EnemySpawnPointNum { get => enemySpawnPointNum; set => enemySpawnPointNum = value; }

        public List <Entity> UnitsInBattle;
        public List<Entity> AlliesInParty;
        public List<Entity> EnemiesInBattle;

        public List<Entity> unitHPList;

        //Variable for the currentEntity and its script
        private GameObject currentUnit;
                
        [SerializeField] private TextMeshProUGUI statusTxt;
        [SerializeField] private TextMeshProUGUI DMGTxt;

        //CameraManager
        private CameraManager cameraManager;

        //EventHandler
        private EventHandler eventHandler;

        //Pings when units are spawning into the battle
        public delegate void OnUnitSpawn(GameObject unitToSapwn);
        public OnUnitSpawn onUnitSpawn;

        public GameObject ChildObj;


        //Pings to reset all Vitual Cameras' Priority
        public delegate void OnBetweenTurn (CinemachineVirtualCamera cinemachineVC);
        public OnBetweenTurn ResetCameraPriority;

        [SerializeField] GameObject battlePanel;
        #endregion

        #region Awake & Start
        private void Awake()
        {
            //Get all necessary references for the script to run
            GetReferences();
        }

        // Start is called before the first frame update
        void Start()
        {
            //Spawn all untis involved in the battle
            SpawnUnits();

            //Sorts the list of Entity scripts by the speed value on each one
            UnitsInBattle.Sort((Ent1, Ent2) => Ent1.Spd.CompareTo(Ent2.Spd));

            //Startup the Battle System
            SetBattleStatus();
        }
        #endregion

        #region Methods
        void GetReferences()
        {
            cameraManager = GameObject.FindObjectOfType<CameraManager>();
            eventHandler = GameObject.FindObjectOfType<EventHandler>();
        }

        //Takes the list of EntityObjToSpawn and sets them up in the battlefield
        void SpawnUnits()
        {
            //Spawns the Gameobject from the list of Entities in the Battelfield and Sorts the list of EntityScripts based on the spd value on it
            foreach (GameObject entity in EntityObjToSpawn)
            {
                if (entity != null)
                //Spawn the entity 
                currentUnit = Instantiate(entity);
                //Add the Entity Script from currentEntity to the EntityScripts List
                UnitsInBattle.Add(currentUnit.GetComponent<Entity>());
            }

            //Set the postions of each entity to its correct spawn location
            foreach (Entity unit in UnitsInBattle)
            {
                unit.SetSpawnPoint(this, cameraManager);
            }

            //Create a list of Entities sorted on their HP values
            unitHPList = new List<Entity>(UnitsInBattle);
        }
        
        //Checks which phase of the battle we are currently in (Player turn or Enemy turn)
        public void SetBattleStatus()
        {
            //Sorts the list of allies by their HP value
            unitHPList.Sort((Ent1, Ent2) => Ent1.Hp.CompareTo(Ent2.Hp));

            //Cylcle through the turn-system til we find a player controlled unit
            foreach (Entity entity in UnitsInBattle.ToList())
            {
                //Updates the currentEntity to the entity whose turn it is
                currentUnit = entity.gameObject;

                //Is the current unit controllable?
                if (currentUnit.GetComponent<Entity>().IsControlable && currentUnit.GetComponent<Entity>().IsDead == false)
                {
                    //Pass the CinemachineVirtualCamera to the CameraManager to set the MainCamera to use the correct VirtualCamera
                    ChildObj = UnitsInBattle[0].transform.GetChild(0).gameObject;
                    eventHandler.onUnitTurn();
   
                    //Enable camera controls for the player
                    cameraManager.IsControllable = true;

                    //Toggle the controls panels for the player
                    battlePanel.SetActive(true);

                    //Set the text to the person's turn (Remove this function later)
                    statusTxt.text = "It is " + currentUnit.GetComponent<Entity>().name + " turn";

                    break;
                }
                //Is the current unit an enemy?
                else if (currentUnit.GetComponent<Entity>().IsDead == false)
                {   //Sets up the OnDealDMG to take the IDamageable TakeDmg method when pinged
                    eventHandler.OnDealDMG = entity.iDamageable.DealDMG;
                    //Disable the camera controls for the player
                    cameraManager.IsControllable = false;
                    //Ping the AttackAlly delegate for the enemy to attack (NEEDS TO BE FIXED)
                    eventHandler.onEnemyTurn(entity);
                    //Move to the next turn
                    eventHandler.OnActionMade.Invoke();

                    ClearTurnPos();
                }
            }
        }

        //Clears and adds the current playable party member from the turn order (sets them to the back of the turn order)
        public void ClearTurnPos()
        {   
            UnitsInBattle.Remove(currentUnit.GetComponent<Entity>());
            UnitsInBattle.Add(currentUnit.GetComponent<Entity>());
        }
        #endregion

    }
}

