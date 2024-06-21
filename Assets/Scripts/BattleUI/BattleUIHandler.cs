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
        [SerializeField] private GameObject gameLossPanel;
        [SerializeField] private GameObject inputHandler;
        #endregion

        private void Start()
        {
            TurnHandler.Instance.OnBattleVictory += SetVictoryScreen;
            TurnHandler.Instance.OnBattleLoss += SetGameLossScreen;
        }

        private void OnDisable()
        {
            TurnHandler.Instance.OnBattleVictory -= SetVictoryScreen;
            TurnHandler.Instance.OnBattleLoss -= SetGameLossScreen;
        }

        void SetVictoryScreen()
        {
            victoryPanel.SetActive(true);
            inputHandler.SetActive(false);
        }

        void SetGameLossScreen()
        {
            gameLossPanel.SetActive(true);
            inputHandler.SetActive(false);
        }
    }

}
