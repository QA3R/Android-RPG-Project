using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entities;
using Managers;

namespace Entities
{
    public class TimerHandler : MonoBehaviour
    {
        private Entity entity;
        [SerializeField] private float currentTimerVal;
        [SerializeField] private bool isTimerRunning;

        public float CurrentTimerVal { get => currentTimerVal; set => currentTimerVal = value; }
        public bool IsTimerRunning { get => isTimerRunning; set => isTimerRunning = value; }

        // Start is called before the first frame update
        void Start()
        {
            CurrentTimerVal = 0;

            if (gameObject.GetComponent<Entity>() != null)
            {
                entity = this.gameObject.GetComponent<Entity>();
            }

            StartEntityTimer();
        }

        // Update is called once per frame
        void Update()
        {

            if (IsTimerRunning && !entity.IsDead)
            {
                CurrentTimerVal += Time.deltaTime * (entity.Spd);
            }

            if (CurrentTimerVal >= 1 && IsTimerRunning)
            {
                TurnManager.Instance.OnEntityTimerReady?.Invoke(entity);

                ResetEntityTimer();
            }
        }

        #region Entity Timer Methods
        public void StartEntityTimer()
        {
            IsTimerRunning = true;
        }

        public void PauseEntityTimer()
        {
            IsTimerRunning = false;
        }

        public void ResetEntityTimer()
        {
            CurrentTimerVal = 0;
        }
        #endregion
    }
}

