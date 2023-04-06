using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;
using Entities;

namespace Managers
{
    public class CameraManager : MonoBehaviour
    {
        #region Variables
        public bool IsControllable;
        
        [SerializeField] private InputHandler inputHandler;
        private BattleManager battleManager;
        private EventHandler eventHandler;
        public List<GameObject> Targets;

        private Entity target;

        [SerializeField] private Camera cam;
        public CinemachineVirtualCamera activeVC;
        public int cameraIndex;
        #endregion

        #region OnEnable, OnDisable, and Start Methods
        private void OnEnable()
        {
            inputHandler = GameObject.FindObjectOfType<InputHandler>();
            inputHandler.swipeRight = CameraChnageTargetRight;
            inputHandler.swipeLeft = CameraChnageTargetLeft;

            eventHandler = GameObject.FindObjectOfType<EventHandler>();

            //Subscribe to the OnUnitTurn delegate: will change the Main Camera to the correct unit's VirtualCamera 
            battleManager = GameObject.FindObjectOfType<BattleManager>();
            battleManager.onUnitTurn = ChangeCamera;

            battleManager.onUnitSpawn = SetupVCList;
            //battleManager.ResetCameraPriority += ResetCameraPriority;
        }

        private void OnDisable()
        {
            inputHandler.swipeRight -= CameraChnageTargetRight;
            inputHandler.swipeLeft -= CameraChnageTargetLeft;
            eventHandler.OnTargetChanged -= SetTarget;
            battleManager.onUnitTurn -= ChangeCamera;

            battleManager.onUnitSpawn -= SetupVCList;
            //battleManager.ResetCameraPriority -= ResetCameraPriority;

        }

        //Subscribe to the SwipeRight and SwipeLeft methods
        private void Start()
        {
            cameraIndex = 0;
            eventHandler.OnTargetChanged = SetTarget;
        }
        #endregion

        #region Camera Control Methods

        private void SetTarget()
        {
            target = Targets[cameraIndex].GetComponent<Entity>();
        }

        [ContextMenu("Move Camera Right")]
        void CameraChnageTargetRight()
        {
            if (IsControllable == true)
            {
                if (cameraIndex < Targets.Count - 1 && activeVC !=null)
                {
                    cameraIndex++;
                    activeVC.LookAt = Targets[cameraIndex].transform;
                    eventHandler.OnTargetChanged.Invoke();
                }
            }
        }

        [ContextMenu("Move Camera Left")]
        void CameraChnageTargetLeft()
        {
            if (IsControllable == true)
            {
                if (cameraIndex > 0)
                {
                    cameraIndex--;
                    activeVC.LookAt = Targets[cameraIndex].transform;
                    eventHandler.OnTargetChanged.Invoke();
                }
            }
        }

        void ChangeCamera()
        {
            GameObject ChildObj = battleManager.UnitsInBattle[0].transform.GetChild(0).gameObject;
            CinemachineVirtualCamera currentEntityVC = battleManager.ChildObj.GetComponent<CinemachineVirtualCamera>(); ;
            
            Debug.Log("SetupUnitCam was invoked");
            if (currentEntityVC != null)
            {
                activeVC = currentEntityVC;
                activeVC.Priority = 11;
                activeVC.LookAt = Targets[0].transform;
                cameraIndex = 0;
                //eventHandler.OnTargetChanged.Invoke();
            }
        }

        public void ResetCameraPriority()
        {
            activeVC.Priority = 0;
        }
        #endregion

        public void SetupVCList(GameObject unitToSpawn)
        {
            Targets.Add(unitToSpawn);
        }
    }
}

