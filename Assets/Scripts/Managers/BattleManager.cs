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
        [SerializeField] private List <Entity> EntityScripts;

        [SerializeField] private Entity currentEntityScript;
        [SerializeField] private Entity targetEntity;

        [SerializeField] private GameObject currentEnemyObj;
        [SerializeField] private GameObject currentEntity;

        [SerializeField] private Enemy currentEnemyScript;
       
        [SerializeField]private float TotalDMG;
        
        [SerializeField] private TextMeshProUGUI statusTxt;
        [SerializeField] private TextMeshProUGUI DMGTxt;

        [SerializeField] GameObject battlePanel;
        #endregion

        // Start is called before the first frame update
        void Start()
        {
            //Spawns the Gameobject from the list of Entities in the Battelfield and Sorts the list of EntityScripts based on the spd value on it
            foreach (GameObject entity in EntityObjToSpawn)
            {
                currentEntity = Instantiate(entity);
                EntityScripts.Add(currentEntity.GetComponent<Entity>());
            }

            //Sorts the list of Entity scripts by the speed value on each one
            EntityScripts.Sort((Ent1, Ent2) => Ent1._spd.CompareTo(Ent2._spd));

            //Startup the Battle System
            SetBattleStatus();
        }

        #region Methods
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
                    //CalculateATK();  
                    ClearTurnPos();
                    //SetBattleStatus();
                }
            }
        }

        //Clears the current playable party member from the turn order (sets them to the back of the turn order)
        public void ClearTurnPos()
        {
            EntityScripts.Remove(currentEntityScript);
            EntityScripts.Add(currentEntityScript);
            //currentEntity= null;
            //targetEntity = null;
        }
        #endregion

        /// <summary>
        /// This method should calculate the total ATK... 
        /// -This should be from the currentEntity's equiped artifacts, buffs, debuffs, passives, etc...
        /// -This currently only takes the ATK value from the currentEntity.ATK and multiplies it by all the ATK% increases from the list of currentEntity.EquipedArtifacts
        /// -Does not currently take into account for buffs that could be in effect
        /// </summary>
        /// <param name="attackingEntity"></param>
        //public void CalculateATK()
        //{            
        //    //Local variable used when we need to calculate the Total Atk% modifier
        //    float TotalDMGModifier = 1;

        //    //Check if the currentEntity does not have any Artifacts equipped
        //    if (currentEntity.EquipedArtifacts != null || currentEntity!=null)
        //    {
        //        List<ArtifactScriptableObject> entityEquipedArtifacts   = currentEntity.EquipedArtifacts;
                
        //        //Get the total Percentage ATK modifier from the Atifacts equiped on the Agent
        //        foreach(ArtifactScriptableObject AtkModifier in currentEntity.EquipedArtifacts)
        //        {
        //            //Add the ATK% from the current artifact in the loop to the TotalDMGModifier
        //            TotalDMGModifier = TotalDMGModifier + AtkModifier.AtKPercent/100;
        //        }
        //    }

        //    //Sets the TotalDMG to the current Entity's ATK multiplied by the ATK% modifier from the Artifacts
        //    TotalDMG = currentEntity.Atk * TotalDMGModifier;
        //    //Debug.Log(currentEntity.name + TotalDMG);
        //}
    }
}

