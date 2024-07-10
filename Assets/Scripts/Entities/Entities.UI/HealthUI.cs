using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entities;
using TMPro;
using Managers;

namespace Entities.UI
{
    public class HealthUI : MonoBehaviour
    {
        private Entity entity;
        private Camera mainCamera;
        
        [SerializeField] private TextMeshProUGUI healthText;
        [SerializeField] private Canvas healthCanvas;

        // Start is called before the first frame update
        void Start()
        {
            TurnManager.Instance.OnStateEnd += UpdateHealthText;

            //Fetch the MainCamera component
            mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            
            //Fetch the Entity Component
            entity = this.gameObject.GetComponentInParent<Entity>();

            //Set the Render Camera to the MainCamera
            healthCanvas.worldCamera = mainCamera;

            //Set the plane distance
            healthCanvas.planeDistance = 15;

            //If we have the entity component, set the health to the entity hp value
            if (entity != null)
            {
                healthText.text = ("HP: " + entity.HealthPoints);
            }
        }

        private void OnDisable()
        {

            TurnManager.Instance.OnStateEnd -= UpdateHealthText;
        }

        // Update is called once per frame
        void Update()
        {
            //Constantly face text to player
            transform.LookAt(mainCamera.transform);
        }

        void UpdateHealthText()
        {
            if (entity != null)
            {
                healthText.text = ("HP: " + entity.HealthPoints);
            }
        }
    }
}

