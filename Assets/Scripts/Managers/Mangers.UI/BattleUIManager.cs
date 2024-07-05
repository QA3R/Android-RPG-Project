using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Managers.Battle
{
    public class BattleUIManager : MonoBehaviour
    {
        #region Variables
        [SerializeField] private GameObject victoryPanel;
        [SerializeField] private GameObject gameLossPanel;
        [SerializeField] private GameObject inputHandler;
        #endregion

        private void Start()
        {
            TurnManager.Instance.OnBattleVictory += SetVictoryScreen;
            TurnManager.Instance.OnBattleLoss += SetGameLossScreen;
        }

        private void OnDisable()
        {
            TurnManager.Instance.OnBattleVictory -= SetVictoryScreen;
            TurnManager.Instance.OnBattleLoss -= SetGameLossScreen;
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
