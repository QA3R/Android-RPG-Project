using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject playerControlsUI;

        //Subscribe to the EventHandler's onPlayerTurn & onEnemyTurn
        private void Start()
        {
            EventHandler.Instance.onPlayerTurn += EnablePlayerControls;
            EventHandler.Instance.onEnemyTurn += DisablePlayerControls;
        }

        //Unsubscribe to the EventHandler's onPlayerTurn & onEnemyTurn
        private void OnDisable()
        {
            EventHandler.Instance.onPlayerTurn -= EnablePlayerControls;
            EventHandler.Instance.onEnemyTurn -= DisablePlayerControls;
        }

        void DisablePlayerControls()
        {
            if (playerControlsUI != null)
            {
                Debug.Log("Player Controls Enabled");
                playerControlsUI.SetActive(false);
            }
        }

        void EnablePlayerControls()
        {
            if(playerControlsUI != null)
            {
                Debug.Log("Player Controls Disabled");
                playerControlsUI.SetActive(true);   
            }
        }
    }
}
    
