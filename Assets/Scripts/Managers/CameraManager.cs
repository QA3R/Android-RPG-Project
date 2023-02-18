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
        
        [SerializeField] private List<Transform> Targets;
     

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
                    activeVC.LookAt = Targets[cameraIndex];
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
                    activeVC.LookAt = Targets[cameraIndex];
                }
            }
        }

        void ChangeCamera()
        {
            CinemachineVirtualCamera currentEntityVC = battleManager.ChildObj.GetComponent<CinemachineVirtualCamera>(); ;
            
            Debug.Log("SetupUnitCam was invoked");
            if (currentEntityVC != null)
            {
                activeVC = currentEntityVC;
                activeVC.Priority = 11;
                activeVC.LookAt = Targets[0];
                cameraIndex = 0;
            }
        }

        public void ResetCameraPriority()
        {
            activeVC.Priority = 0;
        }
        #endregion

        void SetupVCList(GameObject unitToSpawn)
        {
            try 
            {
                Targets.Add(unitToSpawn.transform);
            }
            catch
            {
                
            }
            
        }
    }
}

