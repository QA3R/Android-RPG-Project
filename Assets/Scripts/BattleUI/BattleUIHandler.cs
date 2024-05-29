using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Handlers;

namespace BattleUI
{
    public class BattleUIHandler : MonoBehaviour
    {
        #region Variables
        [SerializeField] private GameObject victoryPanel;
        [SerializeField] private GameObject inputHandler;
        #endregion

        private void Start()
        {
            TurnHandler.Instance.OnBattleVictory += SetVictoryScreen;
        }

        private void OnDisable()
        {
            TurnHandler.Instance.OnBattleVictory -= SetVictoryScreen;
        }

        void SetVictoryScreen()
        {
            victoryPanel.SetActive(true);
            inputHandler.SetActive(false);
        }
    }

}
