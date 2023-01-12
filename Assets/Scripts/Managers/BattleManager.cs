using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using ScriptableObjects;
using System.Linq;
using TMPro;

namespace Managers
{
    public class BattleManager : MonoBehaviour
    {
        #region Variables
        [SerializeField] private List<EntityScriptaleObject> entitiesInField;
        [SerializeField] private List<int> entityspd;
        [SerializeField] private List<EntityScriptaleObject> party;
        [SerializeField] private List<EntityScriptaleObject> enemyParty;
        [SerializeField] private TextMeshProUGUI statusTxt;
        [SerializeField] private EntityScriptaleObject currentEntity;
        [SerializeField] GameObject battlePanel;
        #endregion

        // Start is called before the first frame update
        void Start()
        {
            //spawn all party members on the battlefield
            foreach (EntityScriptaleObject agentSO in party)
            {
                entitiesInField.Add(agentSO);

                if (agentSO.EntityModel != null)
                {
                    agentSO.currentHP = agentSO.MaxHP;
                    Instantiate(agentSO.EntityModel);
                    Debug.Log("Player Model: " + agentSO.name + "has been spawned.");
                }
            }
        
            //Spawn all the enemys from the enemy party list in the battlefield
            foreach (EntityScriptaleObject enemySO in enemyParty)
            {
                entitiesInField.Add(enemySO);  

                if (enemySO.EntityModel != null)
                {
                    enemySO.currentHP = enemySO.MaxHP;
                    Instantiate(enemySO.EntityModel);
                    Debug.Log("Enemy Model " + enemySO.name + "has been spawned");
                }
            }

            //Sorts list in descending order based on ent2.spd compared to ent1.spd
            entitiesInField.Sort((Ent1, Ent2) => Ent2.Spd.CompareTo(Ent1.Spd));

            SetBattleStatus();
        }

        #region Methods
        public void SetBattleStatus()
        {
            foreach (EntityScriptaleObject entitySO in entitiesInField.ToList<EntityScriptaleObject>())
            {
                //Updates the currentEntity to the entity whose turn it is
                currentEntity = entitySO;
                
                //If the player is controlable, we give player controls and sends that entity to the bottom of the list
                if (entitySO.controlable)
                {
                    battlePanel.SetActive(true);
                    statusTxt.text = "It is " + entitySO.name + " turn";
                    break;
                }
                else
                {
                    EnemyAttack(entitySO);
                }
            }
        }

        //Clears the current playable party member from the turn order (sets them to the back of the turn order)
        public void ClearTurnPos()
        {
            entitiesInField.Remove(currentEntity);
            entitiesInField.Add(currentEntity);
        }
        #endregion

        void EnemyAttack(EntityScriptaleObject enemy)
        {
            currentEntity = enemy;

            //Debug.Log("It is Enemy: " + enemy.name + " turn.");
            //Get the partymember with the least HP to the top of the list
            party.Sort((Ent1, Ent2) => Ent1.currentHP.CompareTo(Ent2.currentHP));

            //Get the dmg that will be dealt to the target assigned to this int
            int dmgDealtToTarget = enemy.Atk - party[0].Def;

            if (dmgDealtToTarget < 0)
            {
                dmgDealtToTarget = 0;
            }

            //Subtract the hp from the dmg dealt to the target
            Debug.Log(dmgDealtToTarget + " damage was dealt to " + party[0].name);
            party[0].currentHP = party[0].currentHP - dmgDealtToTarget;

            ClearTurnPos();
            SetBattleStatus();
        }
    }
}

