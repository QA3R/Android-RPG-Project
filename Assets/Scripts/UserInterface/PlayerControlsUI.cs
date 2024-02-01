using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace UserInterface
{
    public class PlayerControlsUI : MonoBehaviour
    {
        [SerializeField] private GameObject controlsUI;

        //Subscribe to the EventHandler's onPlayerTurn & onEnemyTurn
        private void Start()
        {
            BattleHandler.Instance.onPlayerTurn += EnablePlayerControls;
            BattleHandler.Instance.onEnemyTurn += DisablePlayerControls;
        }

        //Unsubscribe to the EventHandler's onPlayerTurn & onEnemyTurn
        private void OnDisable()
        {
            BattleHandler.Instance.onPlayerTurn -= EnablePlayerControls;
            BattleHandler.Instance.onEnemyTurn -= DisablePlayerControls;
        }

        void DisablePlayerControls()
        {
            if (controlsUI != null)
            {
                Debug.Log("Player Controls Enabled");
                controlsUI.SetActive(false);
            }
        }

        void EnablePlayerControls()
        {
            if(controlsUI != null)
            {
                Debug.Log("Player Controls Disabled");
                controlsUI.SetActive(true);   
            }
        }
    }
}
    
