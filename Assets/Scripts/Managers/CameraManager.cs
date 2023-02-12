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
        [SerializeField] private List<Transform> allyOneTargets;
        //[SerializeField] private List<CinemachineVirtualCamera> allyFourVC;
        //[SerializeField] private List<CinemachineVirtualCamera> currentVirtualCameras;
        [SerializeField] private Camera cam;
        [SerializeField] private InputHandler inputHandler;

        [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;

        private int cameraIndex;
        private BattleManager battleManager;

        public bool IsControllable;
   

        #endregion

        #region Start, OnEnable, and OnDisable Methods
        //Subscribe to the SwipeRight and SwipeLeft methods


        private void Start()
        {
            cameraIndex = 0;
            cinemachineVirtualCamera.LookAt = allyOneTargets[0];

            inputHandler = GameObject.FindObjectOfType<InputHandler>();
            inputHandler.swipeRight = CameraChnageTargetRight;
            inputHandler.swipeLeft = CameraChnageTargetLeft;

            battleManager = GameObject.FindObjectOfType<BattleManager>();

            //currentVirtualCameras = new List<CinemachineVirtualCamera>();
            //currentVirtualCameras = allyFourVC;

            //cinemachineVirtualCamera = GameObject.FindObjectOfType<CinemachineVirtualCamera>();
        }

        private void OnEnable()
        {
            inputHandler = GameObject.FindObjectOfType<InputHandler>();
            inputHandler.swipeRight = CameraChnageTargetRight;
            inputHandler.swipeLeft = CameraChnageTargetLeft;
        }

        private void OnDisable()
        {
            inputHandler.swipeRight -= CameraChnageTargetRight;
            inputHandler.swipeLeft -= CameraChnageTargetLeft;
        }
        #endregion

        #region Camera Control Methods
        [ContextMenu("Move Camera Right")]
        void CameraChnageTargetRight()
        {
            if (IsControllable == true)
            {
                if (cameraIndex < allyOneTargets.Count - 1)
                {
                    cameraIndex++;
                    cinemachineVirtualCamera.LookAt = allyOneTargets[cameraIndex];

                    #region Old Code
                    /*
                    foreach (var cam in currentVirtualCameras)
                    {
                        cam.GetComponent<CinemachineVirtualCamera>().Priority = 0;
                    }

                    currentVirtualCameras[cameraIndex].GetComponent<CinemachineVirtualCamera>().Priority = 1;
                    */
                    #endregion
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
                    cinemachineVirtualCamera.LookAt = allyOneTargets[cameraIndex];

                    #region Old Code
                    /*
                    foreach (var cam in currentVirtualCameras)
                    {
                        cam.GetComponent<CinemachineVirtualCamera>().Priority = 0;
                    }

                    currentVirtualCameras[cameraIndex].GetComponent<CinemachineVirtualCamera>().Priority = 1;
                    */
                    #endregion
                }
            }
        }

        [ContextMenu ("Test Camera chang." +
            "e")]
        void VCTestChange(List<CinemachineVirtualCamera> currentVC)
        {
            //currentVirtualCameras = currentVC;
        }
        #endregion
    }
}

