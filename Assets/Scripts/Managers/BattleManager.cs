using Unity.VisualScripting;
using System.Collections;
using UnityEngine.Rendering.Universal;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using ScriptableObjects;
using Entities;

namespace Managers
{
    public class BattleManager : MonoBehaviour
    {
        #region Variables
        [SerializeField] private List<GameObject> EntityObjToSpawn;
        [SerializeField] private List<GameObject> _allySpawnPoints;
        [SerializeField] private List<GameObject> _enemySpawnPoints;
        [SerializeField] public List <Entity> EntityScripts;

        [SerializeField] private Entity currentEntityScript;

        [SerializeField] private GameObject currentEntity;
                
        [SerializeField] private TextMeshProUGUI statusTxt;
        [SerializeField] private TextMeshProUGUI DMGTxt;

        public delegate void OnEnemyAttack(Entity entity);
        public OnEnemyAttack AttackAlly;

        [SerializeField] GameObject battlePanel;
        #endregion

        // Start is called before the first frame update
        void Start()
        {
            int _allySpawnPointNum = 0;
            int _enemySpawnPointNum = 0;

            //Spawns the Gameobject from the list of Entities in the Battelfield and Sorts the list of EntityScripts based on the spd value on it
            foreach (GameObject entity in EntityObjToSpawn)
            {
                if (entity != null)
                {
                    currentEntity = Instantiate(entity);
                    EntityScripts.Add(currentEntity.GetComponent<Entity>());
                }
            }   

            //Set the postions of each ally entity to the ally spawn positions
            foreach (Entity entity in EntityScripts)
            {
                if (entity._isControlable)
                {
                    entity.gameObject.transform.position = _allySpawnPoints[_allySpawnPointNum].transform.position;
                    _allySpawnPointNum++;
                }
            }

            //Sets the positions of each enemy entity to the enemy spawn positions
            foreach (Entity entity in EntityScripts)
            {
                if (!entity._isControlable)
                {
                    entity.gameObject.transform.position = _enemySpawnPoints[_enemySpawnPointNum].transform.position;
                    _enemySpawnPointNum++;
                }
            }

            //Sorts the list of Entity scripts by the speed value on each one
            EntityScripts.Sort((Ent1, Ent2) => Ent1._spd.CompareTo(Ent2._spd));

            //Startup the Battle System
            SetBattleStatus();
        }

        #region Turn-Order Regulation Methods
        public void SetBattleStatus()
        {
            foreach (Entity entity in EntityScripts.ToList())
            {
                //Updates the currentEntity to the entity whose turn it is
                currentEntityScript = entity;
                
                //If the player is controlable, we give player controls and sends that entity to the bottom of the list
                if (currentEntityScript._isControlable)
                {
                    battlePanel.SetActive(true);
                    statusTxt.text = "It is " + currentEntityScript.name + " turn";
                    break;
                }
                else
                {
                    AttackAlly(entity);
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

