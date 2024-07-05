using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Managers;
using Entities;

namespace BattleUI
{
    public class PlayerControlsHandler : MonoBehaviour
    {
        [SerializeField] private GameObject controlsUI;
        [SerializeField] private Button basicAtkBtn;

        //Subscribe to the EventHandler's onPlayerTurn & onEnemyTurn
        private void Start()
        {
            TurnManager.Instance.OnEntityTurnSet += ChangeBasicAtkBtn;
        }

        //Unsubscribe to the EventHandler's onPlayerTurn & onEnemyTurn
        private void OnDisable()
        {
            TurnManager.Instance.OnEntityTurnSet -= ChangeBasicAtkBtn;
        }

        void DisablePlayerControls()
        {
            if (controlsUI != null)
            {
                Debug.Log("Player Controls Enabled");
                controlsUI.SetActive(false);
            }
        }

        public void EnablePlayerControls()
        {
            if(controlsUI != null)
            {
                Debug.Log("Player Controls Disabled");
                controlsUI.SetActive(true);   
            }
        }

        public void ChangeBasicAtkBtn()
        {
            basicAtkBtn.onClick.RemoveAllListeners();
            basicAtkBtn.onClick.AddListener(OnClicked);
        }

        public void OnClicked()
        {
            TurnManager.Instance.CurrentEntity.Attack(TurnManager.Instance.CurrentEntity, TurnManager.Instance.CurrentEntity.Target);
        }
    }
}
    
