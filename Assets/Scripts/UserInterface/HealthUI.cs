using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entities;
using TMPro;

namespace UserInterface
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
            EventHandler.Instance.OnStateEnd += UpdateHealthText;

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
                healthText.text = ("HP: " + entity.Hp);
            }
        }

        private void OnDisable()
        {

            EventHandler.Instance.OnStateEnd -= UpdateHealthText;
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
                healthText.text = ("HP: " + entity.Hp);
            }
        }
    }
}

