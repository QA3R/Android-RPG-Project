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
        [SerializeField] private List<GameObject> _enemySpawnPoints;
        [SerializeField] public List <Entity> EntityScripts;

        //Variable for the currentEntity and its script
        private Entity currentEntityScript;
        private GameObject currentEntity;
                
        [SerializeField] private TextMeshProUGUI statusTxt;
        [SerializeField] private TextMeshProUGUI DMGTxt;

        //CameraManager
        private CameraManager cameraManager;

        //Pings when it is an enemies turn to attack the target that is passed
        public delegate void OnEnemyAttack(Entity target);
        public OnEnemyAttack onEnemyAttack;
       
        //Pings to set the Main Camera to the correct unit's Camera
        public delegate void OnUnitTurn(CinemachineVirtualCamera unitVC);
        public OnUnitTurn onUnitTurn;
        GameObject childObj;

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

            //Spawn all untis involved in the battle
            SpawnUnits();
        }

        // Start is called before the first frame update
        void Start()
        {
            //Sorts the list of Entity scripts by the speed value on each one
            EntityScripts.Sort((Ent1, Ent2) => Ent1._spd.CompareTo(Ent2._spd));

            //Startup the Battle System
            SetBattleStatus();
        }
        #endregion

        #region Methods
        void GetReferences()
        {
            cameraManager = GameObject.FindObjectOfType<CameraManager>();
        }

        //Takes the list of EntityObjToSpawn and sets them up in the battlefield
        void SpawnUnits()
        {
            int allySpawnPointNum = 0;
            int enemySpawnPointNum = 0;

            //Spawns the Gameobject from the list of Entities in the Battelfield and Sorts the list of EntityScripts based on the spd value on it
            foreach (GameObject entity in EntityObjToSpawn)
            {
                if (entity != null)
                //Spawn the entity 
                currentEntity = Instantiate(entity);
                //Add the Entity Script from currentEntity to the EntityScripts List
                EntityScripts.Add(currentEntity.GetComponent<Entity>());
            }

            //Set the postions of each entity to its correct spawn location
            foreach (Entity entity in EntityScripts)
            {
                if (entity._isControlable)
                {   
                    //Set the ally to the next available ally spawn point
                    entity.gameObject.transform.position = _allySpawnPoints[allySpawnPointNum].transform.position;
                    allySpawnPointNum++;
                    //cameraManager.AllyVirtualCameras.Add(entity.gameObject.GetComponentInChildren<CinemachineVirtualCamera>());
                }
                else if (!entity._isControlable)
                {
                    //Set the enemy to the next available enemy spawn point
                    entity.gameObject.transform.position = _enemySpawnPoints[enemySpawnPointNum].transform.position;
                    enemySpawnPointNum++;
                    //cameraManager.EnemyVirtualCameras.Add(entity.gameObject.GetComponentInChildren<CinemachineVirtualCamera>());
                }
            }
        }
        
        //Checks which phase of the battle we are currently in (Player turn or Enemy turn)
        public void SetBattleStatus()
        {
            //Cylcle through the turn-system til we find a player controlled unit
            foreach (Entity entity in EntityScripts.ToList())
            {
                //Updates the currentEntity to the entity whose turn it is
                currentEntityScript = entity;
                currentEntity = entity.gameObject;

                //Pass the CinemachineVirtualCamera to the CameraManager to set the MainCamera to use the correct VirtualCamera
                childObj = EntityScripts[0].transform.GetChild(0).gameObject;
                CinemachineVirtualCamera currentEntityVC = childObj.GetComponent<CinemachineVirtualCamera>();
                //onUnitTurn(currentEntityVC);

                //Is the current unit controllable?
                if (currentEntityScript._isControlable)
                {
   
                    //Enable camera controls for the player
                    cameraManager.IsControllable = true;
                    //Toggle the controls panels for the player
                    battlePanel.SetActive(true);
                    //Set the text to the person's turn (Remove this function later)
                    statusTxt.text = "It is " + currentEntityScript.name + " turn";
                    break;
                }
                //Is the current unit an enemy?
                else
                {
                    //Disable the camera controls for the player
                    cameraManager.IsControllable = false;
                    //Ping the AttackAlly delegate for the enemy to attack
                    onEnemyAttack(entity);
                    //Move to the next turn
                    ClearTurnPos();
                }
            }
        }

        //Clears and adds the current playable party member from the turn order (sets them to the back of the turn order)
        public void ClearTurnPos()
        {   
            EntityScripts.Remove(currentEntityScript);
            EntityScripts.Add(currentEntityScript);
        }
        #endregion

    }
}

