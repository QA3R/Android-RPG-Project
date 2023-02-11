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
        [SerializeField] private List<CinemachineVirtualCamera> allyOneVCs;
        [SerializeField] private List<CinemachineVirtualCamera> allyFourVCs;
        [SerializeField] private List<CinemachineVirtualCamera> currentVirtualCameras;
        [SerializeField] private Camera cam;
        [SerializeField] private InputHandler inputHandler;

        private int cameraIndex;
        #endregion

        #region Start, OnEnable, and OnDisable Methods
        private void Start()
        {
            cameraIndex = 0;

            inputHandler = GameObject.FindObjectOfType<InputHandler>();
            inputHandler.swipeRight = CameraChnageTargetRight;
            inputHandler.swipeLeft = CameraChnageTargetLeft;

            currentVirtualCameras = new List<CinemachineVirtualCamera>();
            currentVirtualCameras = allyFourVCs;
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
            if (cameraIndex < currentVirtualCameras.Count - 1)
            {
                cameraIndex++;

                foreach (var cam in currentVirtualCameras)
                {
                    cam.GetComponent<CinemachineVirtualCamera>().Priority = 0;

                }

                currentVirtualCameras[cameraIndex].GetComponent<CinemachineVirtualCamera>().Priority = 1;
            }
        }

        [ContextMenu("Move Camera Left")]
        void CameraChnageTargetLeft()
        {
            if (cameraIndex > 0)
            {
                cameraIndex--;

                foreach (var cam in currentVirtualCameras)
                {
                    cam.GetComponent<CinemachineVirtualCamera>().Priority = 0;

                }

                currentVirtualCameras[cameraIndex].GetComponent<CinemachineVirtualCamera>().Priority = 1;
            }
        }

        [ContextMenu ("Test Camera chang    e")]
        void VCTestChange(List<CinemachineVirtualCamera> currentVC)
        {
            currentVirtualCameras = currentVC;
        }
        #endregion
    }
}

