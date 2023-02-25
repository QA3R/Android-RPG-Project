using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;

namespace Managers
{
    public class CameraManager : MonoBehaviour
    {
        #region Variables
        public bool IsControllable;
        
        [SerializeField] private InputHandler inputHandler;
        private BattleManager battleManager;
        
        public List<GameObject> Targets;
     

        [SerializeField] private Camera cam;
        [SerializeField] private CinemachineVirtualCamera activeVC;
        private int cameraIndex;
        #endregion

        #region OnEnable, OnDisable, and Start Methods
        private void OnEnable()
        {
            inputHandler = GameObject.FindObjectOfType<InputHandler>();
            inputHandler.swipeRight = CameraChnageTargetRight;
            inputHandler.swipeLeft = CameraChnageTargetLeft;

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

            battleManager.onUnitTurn -= ChangeCamera;

            battleManager.onUnitSpawn -= SetupVCList;
            //battleManager.ResetCameraPriority -= ResetCameraPriority;

        }

        //Subscribe to the SwipeRight and SwipeLeft methods
        private void Start()
        {
            cameraIndex = 0;
        }
        #endregion

        #region Camera Control Methods
        [ContextMenu("Move Camera Right")]
        void CameraChnageTargetRight()
        {
            if (IsControllable == true)
            {
                if (cameraIndex < Targets.Count - 1 && activeVC !=null)
                {
                    cameraIndex++;
                    activeVC.LookAt = Targets[cameraIndex].transform;
                    
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
                }
            }
        }

        void ChangeCamera()
        {
            GameObject ChildObj = battleManager.EntityScripts[0].transform.GetChild(0).gameObject;
            CinemachineVirtualCamera currentEntityVC = battleManager.ChildObj.GetComponent<CinemachineVirtualCamera>(); ;
            
            Debug.Log("SetupUnitCam was invoked");
            if (currentEntityVC != null)
            {
                activeVC = currentEntityVC;
                activeVC.Priority = 11;
                activeVC.LookAt = Targets[0].transform;
                cameraIndex = 0;
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

