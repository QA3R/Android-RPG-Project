using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using ScriptableObjects;
using System.Linq;
using TMPro;
using UnityEngine.Rendering.Universal;

namespace Managers
{
    public class BattleManager : MonoBehaviour
    {
        #region Variables
        [SerializeField] private List<EntityScriptableObject> entitiesInField;
        [SerializeField] private List<EntityScriptableObject> party;
        [SerializeField] private List<EntityScriptableObject> enemyParty;
        
        [SerializeField] private EntityScriptableObject currentEntity;
        [SerializeField] private EntityScriptableObject targetEntity;
       
        [SerializeField]private float TotalDMG;
        
        [SerializeField] private TextMeshProUGUI statusTxt;
        [SerializeField] private TextMeshProUGUI DMGTxt;

        [SerializeField] GameObject battlePanel;
        #endregion

        // Start is called before the first frame update
        void Start()
        {
            //spawn all party members on the battlefield
            foreach (EntityScriptableObject agentSO in party)
            {
                entitiesInField.Add(agentSO);

                if (agentSO.EntityModel != null)
                {
                    agentSO.currentHP = agentSO.MaxHP;
                    Instantiate(agentSO.EntityModel);
                    
                }
            }
        
            //Spawn all the enemys from the enemy party list in the battlefield
            foreach (EntityScriptableObject enemySO in enemyParty)
            {
                entitiesInField.Add(enemySO);  

                if (enemySO.EntityModel != null)
                {
                    enemySO.currentHP = enemySO.MaxHP;
                    Instantiate(enemySO.EntityModel);
                }
            }

            //Sorts list in descending order based on ent2.spd compared to ent1.spd
            entitiesInField.Sort((Ent1, Ent2) => Ent2.Spd.CompareTo(Ent1.Spd));

            SetBattleStatus();
        }

        #region Methods
        public void SetBattleStatus()
        {
            foreach (EntityScriptableObject entitySO in entitiesInField.ToList<EntityScriptableObject>())
            {
                //Updates the currentEntity to the entity whose turn it is
                currentEntity = entitySO;
                
                //If the player is controlable, we give player controls and sends that entity to the bottom of the list
                if (currentEntity.controlable)
                {
                    battlePanel.SetActive(true);
                    statusTxt.text = "It is " + currentEntity.name + " turn";
                    break;
                }
                else
                {
                    CalculateATK();
                    EnemyAttack(currentEntity);
                    ClearTurnPos();
                    SetBattleStatus();
                }
            }
        }

        //Clears the current playable party member from the turn order (sets them to the back of the turn order)
        public void ClearTurnPos()
        {
            entitiesInField.Remove(currentEntity);
            entitiesInField.Add(currentEntity);
            //currentEntity= null;
            //targetEntity = null;
        }
        #endregion

        void EnemyAttack(EntityScriptableObject enemy)
        {
            currentEntity = enemy;

            //Get the partymember with the least HP to the top of the list
            party.Sort((Ent1, Ent2) => Ent1.currentHP.CompareTo(Ent2.currentHP));
            
            //Assign the party member with the lowest SPD to be the targetEntity
            if(party.Count >0)
            {
                targetEntity = party[0];
            
                //Get the dmg that will be dealt to the target assigned to this int
                TotalDMG = TotalDMG - targetEntity.Def;

                if (TotalDMG< 0)
                {
                    TotalDMG = 0;
                }

                //Subtract the hp from the dmg dealt to the target
                targetEntity.currentHP = targetEntity.currentHP - TotalDMG;
                DMGTxt.text = targetEntity.Name + " Took " + TotalDMG + "DMG";

                /*if (targetEntity.currentHP < 1)
                {
                    entitiesInField.Remove(targetEntity);
                    party.Remove(targetEntity);
                }*/
            }
        }

        /// <summary>
        /// This method should calculate the total ATK... 
        /// -This should be from the currentEntity's equiped artifacts, buffs, debuffs, passives, etc...
        /// -This currently only takes the ATK value from the currentEntity.ATK and multiplies it by all the ATK% increases from the list of currentEntity.EquipedArtifacts
        /// -Does not currently take into account for buffs that could be in effect
        /// </summary>
        /// <param name="attackingEntity"></param>
        public void CalculateATK()
        {            
            //Local variable used when we need to calculate the Total Atk% modifier
            float TotalDMGModifier = 1;

            //Check if the currentEntity does not have any Artifacts equipped
            if (currentEntity.EquipedArtifacts != null || currentEntity!=null)
            {
                List<ArtifactScriptableObject> entityEquipedArtifacts   = currentEntity.EquipedArtifacts;
                
                //Get the total Percentage ATK modifier from the Atifacts equiped on the Agent
                foreach(ArtifactScriptableObject AtkModifier in currentEntity.EquipedArtifacts)
                {
                    //Add the ATK% from the current artifact in the loop to the TotalDMGModifier
                    TotalDMGModifier = TotalDMGModifier + AtkModifier.AtKPercent/100;
                }
            }

            //Sets the TotalDMG to the current Entity's ATK multiplied by the ATK% modifier from the Artifacts
            TotalDMG = currentEntity.Atk * TotalDMGModifier;
            //Debug.Log(currentEntity.name + TotalDMG);
        }
    }
}

