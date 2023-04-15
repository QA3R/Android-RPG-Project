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
        #region Singleton Implementation
        private static CameraManager instance;
        public static CameraManager Instance => instance;
        #endregion

        #region Variables
        public bool IsControllable;
        
        public List<GameObject> Targets;

        private Entity target;

        [SerializeField] private Camera cam;
        private CinemachineVirtualCamera currentEntityVC;
        public CinemachineVirtualCamera activeVC;
        public int cameraIndex;
        #endregion

        #region OnEnable, OnDisable, and Start Methods
        private void OnEnable()
        {
            //Subscribe the CameraManager's methods to move the camera right and left to the InputHandler's events
            EventHandler.Instance.swipeRight = CameraChnageTargetRight;
            EventHandler.Instance.swipeLeft = CameraChnageTargetLeft;

            //Subscribe to the OnUnitTurn delegate: will change the Main Camera to the correct unit's VirtualCamera 
            EventHandler.Instance.onPlayerTurn = ChangeCamera;

            EventHandler.Instance.onUnitSpawn = SetupVCList;
        }

        private void OnDisable()
        {
            EventHandler.Instance.swipeRight -= CameraChnageTargetRight;
            EventHandler.Instance.swipeLeft -= CameraChnageTargetLeft;
            EventHandler.Instance.OnTargetChanged -= SetTarget;
            EventHandler.Instance.onPlayerTurn -= ChangeCamera;

            EventHandler.Instance.onUnitSpawn -= SetupVCList;
        }

        private void Awake()
        {
            //Singleton Implementation
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        //Subscribe to the SwipeRight and SwipeLeft methods
        private void Start()
        {
            cameraIndex = 0;
            EventHandler.Instance.OnTargetChanged = SetTarget;
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
                    EventHandler.Instance.OnTargetChanged.Invoke();
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
                    EventHandler.Instance.OnTargetChanged.Invoke();
                }
            }
        }

        void ChangeCamera()
        {
            //If there is a currentEntityVC (We have gone pass one PlayerTurn, we will set the previous Player Unit's VC priorty to 0)
            if (currentEntityVC != null)
            {
                currentEntityVC.Priority = 0;
            }

            //Find the current player's VC and attach it to the currentEntityVC
            GameObject ChildObj = BattleManager.Instance.UnitsInBattle[BattleManager.Instance.UnitIndex].transform.GetChild(0).gameObject;
            currentEntityVC = BattleManager.Instance.ChildObj.GetComponent<CinemachineVirtualCamera>(); ;

            ///<Summary> 
            ///If we have a currentEntityVC:
            ///we are going to assign it to the activeVC so that the camera can move to the corerct positon
            ///Set the activeVC priority to the highest value
            ///Set the activeVC's LookAt target to the first enemy in our Targets list
            ///Reset the cameraIndex
            ///</Summary>
            if (currentEntityVC != null)
            {
                activeVC = currentEntityVC;
                activeVC.Priority = 11;
                activeVC.LookAt = Targets[0].transform;
                cameraIndex = 0;
            }
        }
        #endregion

        public void SetupVCList(GameObject unitToSpawn)
        {
            Targets.Add(unitToSpawn);
        }
    }
}

