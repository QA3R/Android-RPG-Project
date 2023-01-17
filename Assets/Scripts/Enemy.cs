using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjects;
using TMPro;
using Managers;

namespace Entities
{
    public class Enemy : Entity
    {

        [SerializeField] private EntityScriptableObject currentEntity;
        [SerializeField] private EntityScriptableObject targetEntity;

        [SerializeField] private float TotalDMG;

        [SerializeField] private TextMeshProUGUI statusTxt;
        [SerializeField] private TextMeshProUGUI DMGTxt;

        [SerializeField] GameObject battlePanel;
        [SerializeField] BattleManager battleManager;

        public delegate void OnAttack(EntityScriptableObject targetEntity);
        OnAttack nextAction;

        //Selects the Ally with the lowest current HP and calculates DMG dealt based on the enemy's ATK
        public void Attack(EntityScriptableObject enemy)
        {
            //Assigns the current 
            currentEntity = enemy;

            //Get the partymember with the least HP to the top of the list
            battleManager.party.Sort((Ent1, Ent2) => Ent1.currentHP.CompareTo(Ent2.currentHP));

            //Assign the party member with the lowest SPD to be the targetEntity
            if (battleManager.party.Count > 0)
            {
                targetEntity = battleManager.party[0];

                //Get the dmg that will be dealt to the target assigned to this int
                TotalDMG = TotalDMG - targetEntity.Def;

                if (TotalDMG < 0)
                {
                    TotalDMG = 0;
                }

                //Subtract the hp from the dmg dealt to the target
                targetEntity.currentHP = targetEntity.currentHP - TotalDMG;
                DMGTxt.text = targetEntity.Name + " Took " + TotalDMG + "DMG";

                //Ping to do DMG to party members when DMG is calculated
            }
        }
    }

}