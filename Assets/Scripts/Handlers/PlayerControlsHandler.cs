using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Managers;
using Entities;

namespace UserInterface
{
    public class PlayerControlsHandler : MonoBehaviour
    {
        [SerializeField] private GameObject controlsUI;
        [SerializeField] private Button basicAtkBtn;

        //Subscribe to the EventHandler's onPlayerTurn & onEnemyTurn
        private void Start()
        {
            TurnHandler.Instance.OnTurnReady += ChangeBasicAtkBtn;
        }

        //Unsubscribe to the EventHandler's onPlayerTurn & onEnemyTurn
        private void OnDisable()
        {
            TurnHandler.Instance.OnTurnReady -= ChangeBasicAtkBtn;
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
            TurnHandler.Instance.CurrentEntity.Attack(TurnHandler.Instance.CurrentEntity, TurnHandler.Instance.CurrentEntity.Target);
        }
    }
}
    
